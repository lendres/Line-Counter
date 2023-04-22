using DigitalProduction.Forms;
using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DigitalProduction.LineCounter
{
	/// <summary>
	/// A text-file line counting application.  Used to count lines of code, et cetera.
	/// </summary>
	public partial class LineCounter : DPMForm
	{
        #region Delegates

        /// <summary>
        /// Cancels the processing thread via a call back function from another thread.
        /// </summary>
        public delegate void UICallBack();

        #endregion

        #region Members

        private UICallBack						_updateUiCallback;

		private readonly LCWinRegAccess			_winRegistryAccess;
		private ArrayList						_files						= new ArrayList();
		private FileTypes						_fileTypes;
		private Thread[]						_threads;
		private ManualResetEvent[]				_threadDoneEvents;

		private ReaderWriterLock				_fileNameLock				= new ReaderWriterLock();
		private ReaderWriterLock				_lineCountLock				= new ReaderWriterLock();
		private Semaphore						_jointhreadsemaphore		= new Semaphore(1, 1);

		private int								_nProcessors;
		private bool							_startedThreadJoin;

		private CommentMode						_commentMode;
		private int								_fileCount;
		private int								_currentFile;
		private LineCounts						_lineCounts;

		StreamWriter							_log;
		string									_logFile;

		string									_commentingStyleDirectory;

		#endregion

		#region Construction / Timer

		public LineCounter() : base("Line Counter")
		{
			InitializeComponent();

			_winRegistryAccess				= new LCWinRegAccess(this);
			_commentMode				= _winRegistryAccess.CommentMode;
			_updateUiCallback			= new UICallBack(UpdateUICounts);
			_logFile					= DigitalProduction.Reflection.Assembly.LibraryPath + "\\log.txt";
			_commentingStyleDirectory	= DigitalProduction.Reflection.Assembly.LibraryPath + "\\Commenting Styles";


            // Before we can do any reading and writing from the registry we have
            // to make sure that the installations has been done so that we done try
            // to read entries that are not there.  This is mostly used for debugging
            // purposes.
            // Allows for the installation event to occur.  Largely useful for debugging or resetting the software if the
            // registry gets messed up.
            _winRegistryAccess.RaiseInstallEvent();

            _nProcessors		= Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
			_threads			= new Thread[_nProcessors];
			_threadDoneEvents	= new ManualResetEvent[_nProcessors];

			// Load a list of commenting style configuration files from the hard drive into
			// the drop down box for commenting styles.  The combo box selection change event
			// will file when this happens so we have to save and restore the last selected filter
			// index (which is reset during the change event) to create the desire behavior of saving
			// the filter string across instances.
			int lastFilterString = _winRegistryAccess.LastSelectedFilterString;
			LoadCommentingStyles();
			_winRegistryAccess.LastSelectedFilterString = lastFilterString;

			// Set the comment mode.
			switch (_winRegistryAccess.CommentMode)
			{
				case CommentMode.FileExtension:
				{
					this.radbtnFileExtension.Checked = true;
					break;
				}

				case CommentMode.Specified:
				{
					this.radbtnSpecify.Checked = true;
					break;
				}

				default:
				{
					throw new Exception("Unrecognized comment mode.");
				}
			}
			SetCommentMode(_winRegistryAccess.CommentMode);


			#region Status Bar

			this.statusBar						= new DigitalProduction.Forms.StatusBarWithProgress();
			this.statusBarPanel1				= new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2				= new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel3				= new System.Windows.Forms.StatusBarPanel();

			this.statusBar.Location				= new System.Drawing.Point(0, 488);
			this.statusBar.Name					= "Status Bar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {this.statusBarPanel1,
																					  this.statusBarPanel3,
																					  this.statusBarPanel2});
			this.statusBar.SetProgressBarPanel	= 1;
			this.statusBar.ShowPanels			= true;
			this.statusBar.Size					= new System.Drawing.Size(492, 22);
			this.statusBar.TabStop				= false;
			
			this.statusBarPanel1.AutoSize		= System.Windows.Forms.StatusBarPanelAutoSize.None;
			this.statusBarPanel1.MinWidth		= 80;
			this.statusBarPanel1.Text			= "Ready.";
			this.statusBarPanel1.Width			= 100;

			this.statusBarPanel3.AutoSize		= System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel3.Style			= System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.statusBarPanel3.Width			= 188;

			this.statusBar.ProgressBar.Visible	= false;
			this.statusBar.ProgressBar.Step		= 1;
			this.statusBar.ProgressBar.Minimum	= 0;

			this.statusBarPanel2.Text			= System.DateTime.Now.ToLongTimeString();
			this.tmrClock.Interval				= 1000;
			this.tmrClock.Enabled				= true;
			this.tmrClock.Tick					+= new EventHandler(tmrClock_Tick);

			this.Controls.Add(this.statusBar);

			#endregion
		}

		/// <summary>
		/// Updates the clock on the status bar.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void tmrClock_Tick(object sender, EventArgs e)
		{
			this.statusBarPanel2.Text = System.DateTime.Now.ToLongTimeString();
		}

		#endregion

		#region Get Files

		#region Event Handler

		/// <summary>
		/// Get files to count the lines of.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void btnGetFiles_Click(object sender, System.EventArgs e)
		{
			switch (_commentMode)
			{
				case CommentMode.FileExtension:
				{
					if (!GetFilesByDirectory())
					{
						return;
					}
					break;
				}

				case CommentMode.Specified:
				{
					if (!GetFilesBySelecting())
					{
						return;
					}
					break;
				}
			}

			this.statusBar.ProgressBar.Value	= 0;
			this.lnkReport.Visible				= false;

			// Update the files selected display.
			this.txtbxNumberFiles.Text = "0";

			// Update the files processed display.
			this.txtbxNumberFilesProcessed.Text = "0";

			// Update the blank lines display.
			this.txtbxBlankLines.Text = "0";

			// Update the comment lines display.
			this.txtbxComments.Text = "0";

			// Update the code lines display.
			this.txtbxCodeLines.Text = "0";

			// Update the total line display.
			this.txtbxNumberLines.Text = "0";
		}

		#endregion

		#region Get Files by Directory

		/// <summary>
		/// Get the files by selecting a directory.
		/// </summary>
		/// <returns>True if files were selected, false otherwise.</returns>
		private bool GetFilesByDirectory()
		{
			SearchOption searchoption = SearchOption.AllDirectories;

			FolderBrowserDialog dialog	= new FolderBrowserDialog();
			dialog.Description			= this.Text + ": Select the directory of the file to count.";
			dialog.ShowNewFolderButton	= false;

			// Start in the previous directory (if it exists).
			if (Directory.Exists(_winRegistryAccess.LastPathUsed))
			{
				dialog.SelectedPath = _winRegistryAccess.LastPathUsed;
			}

			DialogResult result = dialog.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				_files.Clear();
				this.txtbxFileLocation.Text = dialog.SelectedPath;
				_winRegistryAccess.LastPathUsed	= dialog.SelectedPath;

				string[] files = Directory.GetFiles(dialog.SelectedPath, "*", searchoption);
				_files.AddRange(files);
			}
			else
			{
				return false;
			}

			return true;
		}

		#endregion

		#region Get Files by Selecting

		/// <summary>
		/// Get the files by selecting specific ones.
		/// </summary>
		/// <returns>True if files were selected, false otherwise.</returns>
		private bool GetFilesBySelecting()
		{
			// Get some files.
			OpenFileDialog dialog		= new OpenFileDialog();
			dialog.Title				= this.Text + ": Select Files for Counting";
			dialog.CheckFileExists		= true;
			dialog.ValidateNames		= true;
			dialog.Multiselect			= true;
			dialog.RestoreDirectory		= false;
			dialog.InitialDirectory		= _winRegistryAccess.LastPathUsed;

			string filterstring			= _fileTypes.GetFilterString() + "|All Files (*.*)|*.*";
			dialog.Filter				= filterstring;

			// Restore the last setting if possible.  If we changed the commenting style this won't make sense,
			// so this value is reset when the combo box for the commenting style is changed.
			dialog.FilterIndex = _winRegistryAccess.LastSelectedFilterString;
			if (this.cmbbxCommentStyle.SelectedIndex < 0)
			{
				// The last value wasn't found, so default to the first one in the list.
				this.cmbbxCommentStyle.SelectedIndex = 0;
			}

			try
			{
				// Get the files.
				DialogResult result = dialog.ShowDialog(this);

				// If the dialog is canceled, then just get out of here.
				if (result != DialogResult.OK)
				{
					return false;
				}
			}
			catch
			{
				// Show an error and get out of here.
				MessageBox.Show(this, "An error occurred during file selection.  Too many files may have\nbeen selected.  There is an approximately 200 file limit.\n\nTry again selecting fewer files.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			_winRegistryAccess.LastSelectedFilterString	= dialog.FilterIndex;
			_winRegistryAccess.LastCommentingStyle		= this.cmbbxCommentStyle.SelectedItem.ToString();
			_winRegistryAccess.LastPathUsed				= DigitalProduction.IO.Path.GetDirectory(dialog.FileNames[0]);

			// Add the files to our list of files.
			_files.Clear();
			foreach (string file in dialog.FileNames)
			{
				_files.Add(file);
			}

			this.txtbxFileLocation.Text = DigitalProduction.IO.Path.GetDirectory(dialog.FileName);

			return true;
		}

		#endregion

		#endregion

		#region Menu Event Handlers

		/// <summary>
		/// Close the application.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void mnuExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Show help.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void mnuHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(this, "Help\\Line Counter.chm");
		}

		#endregion

		#region Count

		#region Button Click

		/// <summary>
		/// Count the lines.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void btnCount_Click(object sender, System.EventArgs e)
		{
			if (_files.Count == 0)
			{
				MessageBox.Show(this, "At least 1 file must be specified.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Give a wait cursor.
			this.Cursor = Cursors.WaitCursor;

			// Reset status bar.
			this.statusBarPanel1.Text			= "Counting...";
			this.statusBar.ProgressBar.Value	= 0;
			this.statusBar.ProgressBar.Maximum	= _files.Count;
			this.statusBar.ProgressBar.Visible	= true;

			// Disable controls so no one tries any monkey business.
            this.SetControls(false);

			// Reset counters.
			_lineCounts		= new LineCounts();
			_fileCount		= 0;
			_currentFile	= 0;

			// Store the filter index in case a wise guy decides to change it before the counting
			// has started.  The count threads retrieve this value to determine the commenting style
			// to use.
			_winRegistryAccess.LastCommentingStyle		= this.cmbbxCommentStyle.SelectedItem.ToString();

			// Store the comment style that was used.
			_winRegistryAccess.CommentMode		= _commentMode;

			// Log file.
			_log = new StreamWriter(_logFile, false);

			// Initiate threads.
			_startedThreadJoin = false;
			for (int i = 0; i < _nProcessors; i++)
			{
				// Create the thread.
				_threads[i]				= new Thread(new ThreadStart(CountLines));

				// We use the thread number as the name.  That way we can uniquely trigger one of the 
				// reset events once a thread is finished processing.
				_threads[i].Name		= i.ToString();

				// A manual reset event is used because it won't set back to the initial state like the auto
				// reset event.  We are waiting for all events to set before continuing.
				_threadDoneEvents[i]	= new ManualResetEvent(false);

				// Start the thread.
				_threads[i].Start();
			}
		}

		#endregion

		#region Count the Lines

		/// <summary>
		/// Worker function for line counting threads.
		/// </summary>
		private void CountLines()
		{
			// Loop through file names.  When all of the file names have been processed
			// a break is called to exit the loop.
			while (true)
			{
				string filename;

				// Acquire the next file name, insuring we don't miss a name by using a
				// lock to limit access to one thread at a time.
				_fileNameLock.AcquireWriterLock(-1);
				if (_currentFile >= _files.Count)
				{
					// No more files, release the lock and break out of while loop.
					_fileNameLock.ReleaseWriterLock();

					// Figure out which thread we are and signal the event that says we are done with
					// our work.  We have to wait here until all threads are done because each thread
					// updates the GUI with it's results after a file has been processed and the next
					// step is to 
					int name = System.Convert.ToInt32(Thread.CurrentThread.Name);
					_threadDoneEvents[name].Set();
					WaitHandle.WaitAll(_threadDoneEvents);

					// We are locking this part with a semaphore to make sure only one gets access at a
					// time.  That way we don't have a race condition to calling the thread joining and
					// final GUI update.  Note, because of this semaphore it might not be strickly necessary
					// to join the threads (they are done with their work by that time), but it doesn't matter
					// anyway.
					_jointhreadsemaphore.WaitOne();

					// Make sure we don't try to rejoin threads and reactive the controls once they 
					// have already been joined.
					if (!_startedThreadJoin)
					{
						_startedThreadJoin = true;
						this.BeginInvoke(new UICallBack(JoinThreads));
					}
					_jointhreadsemaphore.Release();
					break;
				}

				filename = _files[_currentFile++].ToString();
				_fileNameLock.ReleaseWriterLock();

				// Use local counters so that we can process an entire file and then update the
				// master counters and gui.  This limits the number of read/write locks required
				// and the number of gui updates to a reasonable amount so that the counting isn't
				// done too slowly.
				LineCounts linecounts = _fileTypes.Count(filename);

				// Update the line counters and display.
				_lineCountLock.AcquireWriterLock(-1);
				_lineCounts += linecounts;

				// Update the file counter and display.
				_fileCount++;
				_lineCountLock.ReleaseWriterLock();

				// Do callback to update the user interface.
				this.Invoke(new UICallBack(UpdateUICounts));
			}
		}

		#endregion

		#region GUI Update / Thread Joining / Report Writing

		/// <summary>
		/// Udate the counts and progress bar on the user interface.
		/// </summary>
		private void UpdateUICounts()
		{
			// Update the progress bar.
			this.statusBar.ProgressBar.PerformStep();
			this.statusBar.ProgressBar.Refresh();

			// Update the number of files selected.
			this.txtbxNumberFiles.Text = _fileCount.ToString();
			this.txtbxNumberFiles.Refresh();

			// Update the number of files processed.
			this.txtbxNumberFilesProcessed.Text = _lineCounts.Files.ToString();
			this.txtbxNumberFilesProcessed.Refresh();

			// Update the blank lines display.
			this.txtbxBlankLines.Text = _lineCounts.BlankLines.ToString();
			this.txtbxBlankLines.Refresh();

			// Update the comment lines display.
			this.txtbxComments.Text = _lineCounts.CommentLines.ToString();
			this.txtbxComments.Refresh();

			// Update the code lines display.
			this.txtbxCodeLines.Text = _lineCounts.CodeLines.ToString();
			this.txtbxCodeLines.Refresh();

			// Update the total lines display.
			this.txtbxNumberLines.Text = _lineCounts.Lines.ToString();
			this.txtbxNumberLines.Refresh();
		}

		/// <summary>
		/// Join the threads and restore the controls.
		/// </summary>
		private void JoinThreads()
		{
			// Capture threads.
			for (int i = 0; i < _nProcessors; i++)
			{
				_threads[i].Join();
				_threads[i] = null;
			}

			WriteReport();

			// Close log.
			_log.Close();

			// Clean up and restore user interface.
			this.SetControls(true);
			this.lnkReport.Visible		= true;
			this.statusBarPanel1.Text	= "Ready.";
			this.Cursor					= Cursors.Default;
		}

		/// <summary>
		/// Write a report of the counts.
		/// </summary>
		private void WriteReport()
		{
			_log.WriteLine("Number of Files Selected: " + _fileCount);
			_log.WriteLine();
			_lineCounts.WriteReport(_log);
		}

		#endregion

		#endregion

		#region Helper Functions

		/// <summary>
		/// Enables or disables the controls based on if counting is occuring.
		/// </summary>
		/// <param name="enabled">If the controls should be enabled or not.</param>
		private void SetControls(bool enabled)
		{
			this.mnuExit.Enabled				= enabled;
			this.btnGetFiles.Enabled			= enabled;
			this.btnCount.Enabled				= enabled;
			this.Refresh();
		}

		#endregion

		#region Commenting Styles

		/// <summary>
		/// Load the commenting style file.
		/// </summary>
		private void LoadCommentingStyleFile()
		{
			_fileTypes	= FileTypes.ReadFileTypesFile(_commentingStyleDirectory + "\\" + this.cmbbxCommentStyle.SelectedItem + ".lcs");
			this.rtxtbxCommentStyle.Text = _fileTypes.Description;
		}

		/// <summary>
		/// Populate the commenting style drop down box with the files from the hard drive.
		/// </summary>
		private void LoadCommentingStyles()
		{
			string[] files = Directory.GetFiles(_commentingStyleDirectory, "*.lcs", SearchOption.AllDirectories);

			// Make sure that there are appropriate Output Templates located in the directory.
			if (files.Length == 0)
			{
				MessageBox.Show(this, "No commenting style files could be found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}

			// Trim the path off and leave just the file names.
			for (int i = 0; i < files.Length; i++)
			{
				// Template files in the dialog do not include the Template Directory part of the path so we remove
				// it here.
				string file = files[i].Replace(_commentingStyleDirectory + "\\", "");

				file = file.Replace(System.IO.Path.GetExtension(file), "");

				// Add the file to the drop down list.
				this.cmbbxCommentStyle.Items.Add(file);
			}

			// Restore the template file if it can be found.
			this.cmbbxCommentStyle.SelectedItem = _winRegistryAccess.LastCommentingStyle;
			if (this.cmbbxCommentStyle.SelectedIndex < 0)
			{
				// The last template file was not found, so default to the first one in the list.
				this.cmbbxCommentStyle.SelectedIndex = 0;
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Handle the changing of the comment style in the drop down box.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void cmbbxCommentStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadCommentingStyleFile();

			// Since we changed the commenting style file, and the filter string is generated from the list of 
			// commenting styles in the commenting style file, the last selected filter string no-long makes
			// sense so we will set it to a value that doesn't exist so the default filter string is selected.
			_winRegistryAccess.LastSelectedFilterString = -1;
		}

		/// <summary>
		/// By File Extension radio button checked changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void radbtnFileExtension_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radbtnFileExtension.Checked)
			{
				SetCommentMode(CommentMode.FileExtension);
			}
		}

		/// <summary>
		/// Specify radio button checked changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void radbtnSpecify_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radbtnSpecify.Checked)
			{
				SetCommentMode(CommentMode.Specified);
			}
		}

		/// <summary>
		/// Sets the comment mode.
		/// </summary>
		/// <param name="commentMode">CommentMode.</param>
		private void SetCommentMode(CommentMode commentMode)
		{
			_commentMode = commentMode;
		}


		/// <summary>
		/// Open the report when the link is clicked.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private void lnkReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(_logFile);
		}

		#endregion

	} // End class.
} // End namespace.