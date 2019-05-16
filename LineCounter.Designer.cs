using System;

namespace DigitalProduction.LineCounter
{
	/// <summary>
	/// Summary not provided for the class LineCounter.
	/// </summary>
	partial class LineCounter
	{
		#region Members / Variables.

		private DigitalProduction.Forms.StatusBarWithProgress		statusBar;
		private System.Windows.Forms.StatusBarPanel					statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel					statusBarPanel2;
		private System.Windows.Forms.StatusBarPanel					statusBarPanel3;
		private System.Windows.Forms.Timer							tmrClock;

		private System.Windows.Forms.Button btnCount;

		private System.Windows.Forms.GroupBox						grpbxResults;
		private System.Windows.Forms.Label							lblComments;
		private System.Windows.Forms.TextBox						txtbxComments;
		private System.Windows.Forms.Label							lblBlankLines;
		private System.Windows.Forms.TextBox						txtbxBlankLines;
		private System.Windows.Forms.Label							lblNumberLines;
		private System.Windows.Forms.Label							lblNumberFiles;
		private System.Windows.Forms.TextBox						txtbxNumberFiles;
		private System.Windows.Forms.TextBox						txtbxNumberLines;
		private System.Windows.Forms.Button							btnGetFiles;
		private System.Windows.Forms.TextBox						txtbxFileLocation;
		private System.Windows.Forms.Label							lblFileLocation;
		private System.Windows.Forms.Label							lblCodeLines;
		private System.Windows.Forms.TextBox						txtbxCodeLines;

		private System.Windows.Forms.MenuStrip						mnuMain;
		private System.Windows.Forms.ToolStripMenuItem				mnuFile;
		private System.Windows.Forms.ToolStripMenuItem				mnuExit;
		private System.Windows.Forms.ToolStripSeparator				mnusepFile1;
		private System.Windows.Forms.ToolStripMenuItem				mnuHelp;

		private System.ComponentModel.IContainer					components;

		#endregion

		#region Disposing.

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Windows Form Designer generated code.

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineCounter));
			this.btnCount = new System.Windows.Forms.Button();
			this.tmrClock = new System.Windows.Forms.Timer(this.components);
			this.grpbxResults = new System.Windows.Forms.GroupBox();
			this.lblNumberFilesProcessed = new System.Windows.Forms.Label();
			this.txtbxNumberFilesProcessed = new System.Windows.Forms.TextBox();
			this.lblCodeLines = new System.Windows.Forms.Label();
			this.txtbxCodeLines = new System.Windows.Forms.TextBox();
			this.lblComments = new System.Windows.Forms.Label();
			this.txtbxComments = new System.Windows.Forms.TextBox();
			this.lblBlankLines = new System.Windows.Forms.Label();
			this.txtbxBlankLines = new System.Windows.Forms.TextBox();
			this.lblNumberFiles = new System.Windows.Forms.Label();
			this.txtbxNumberFiles = new System.Windows.Forms.TextBox();
			this.txtbxNumberLines = new System.Windows.Forms.TextBox();
			this.lblNumberLines = new System.Windows.Forms.Label();
			this.btnGetFiles = new System.Windows.Forms.Button();
			this.txtbxFileLocation = new System.Windows.Forms.TextBox();
			this.lblFileLocation = new System.Windows.Forms.Label();
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnusepFile1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.grpbxFiles = new System.Windows.Forms.GroupBox();
			this.radbtnSpecify = new System.Windows.Forms.RadioButton();
			this.radbtnFileExtension = new System.Windows.Forms.RadioButton();
			this.cmbbxCommentStyle = new System.Windows.Forms.ComboBox();
			this.lnkReport = new System.Windows.Forms.LinkLabel();
			this.lblCommentStyle = new System.Windows.Forms.Label();
			this.grpbxCommentingStyle = new System.Windows.Forms.GroupBox();
			this.rtxtbxCommentStyle = new System.Windows.Forms.RichTextBox();
			this.grpbxResults.SuspendLayout();
			this.mnuMain.SuspendLayout();
			this.grpbxFiles.SuspendLayout();
			this.grpbxCommentingStyle.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCount
			// 
			this.btnCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCount.Location = new System.Drawing.Point(457, 450);
			this.btnCount.Name = "btnCount";
			this.btnCount.Size = new System.Drawing.Size(75, 23);
			this.btnCount.TabIndex = 6;
			this.btnCount.Text = "&Count";
			this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
			// 
			// grpbxResults
			// 
			this.grpbxResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpbxResults.Controls.Add(this.lblNumberFilesProcessed);
			this.grpbxResults.Controls.Add(this.txtbxNumberFilesProcessed);
			this.grpbxResults.Controls.Add(this.lblCodeLines);
			this.grpbxResults.Controls.Add(this.txtbxCodeLines);
			this.grpbxResults.Controls.Add(this.lblComments);
			this.grpbxResults.Controls.Add(this.txtbxComments);
			this.grpbxResults.Controls.Add(this.lblBlankLines);
			this.grpbxResults.Controls.Add(this.txtbxBlankLines);
			this.grpbxResults.Controls.Add(this.lblNumberFiles);
			this.grpbxResults.Controls.Add(this.txtbxNumberFiles);
			this.grpbxResults.Controls.Add(this.txtbxNumberLines);
			this.grpbxResults.Controls.Add(this.lblNumberLines);
			this.grpbxResults.Location = new System.Drawing.Point(8, 274);
			this.grpbxResults.Name = "grpbxResults";
			this.grpbxResults.Size = new System.Drawing.Size(524, 168);
			this.grpbxResults.TabIndex = 5;
			this.grpbxResults.TabStop = false;
			this.grpbxResults.Text = "Results";
			// 
			// lblNumberFilesProcessed
			// 
			this.lblNumberFilesProcessed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNumberFilesProcessed.Location = new System.Drawing.Point(10, 45);
			this.lblNumberFilesProcessed.Name = "lblNumberFilesProcessed";
			this.lblNumberFilesProcessed.Size = new System.Drawing.Size(140, 23);
			this.lblNumberFilesProcessed.TabIndex = 10;
			this.lblNumberFilesProcessed.Text = "Number of Files Processed:";
			this.lblNumberFilesProcessed.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// txtbxNumberFilesProcessed
			// 
			this.txtbxNumberFilesProcessed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxNumberFilesProcessed.Location = new System.Drawing.Point(404, 42);
			this.txtbxNumberFilesProcessed.Name = "txtbxNumberFilesProcessed";
			this.txtbxNumberFilesProcessed.ReadOnly = true;
			this.txtbxNumberFilesProcessed.Size = new System.Drawing.Size(110, 20);
			this.txtbxNumberFilesProcessed.TabIndex = 11;
			this.txtbxNumberFilesProcessed.TabStop = false;
			this.txtbxNumberFilesProcessed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblCodeLines
			// 
			this.lblCodeLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCodeLines.Location = new System.Drawing.Point(10, 69);
			this.lblCodeLines.Name = "lblCodeLines";
			this.lblCodeLines.Size = new System.Drawing.Size(140, 24);
			this.lblCodeLines.TabIndex = 2;
			this.lblCodeLines.Text = "Number of Lines of Code:";
			this.lblCodeLines.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// txtbxCodeLines
			// 
			this.txtbxCodeLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxCodeLines.Location = new System.Drawing.Point(404, 66);
			this.txtbxCodeLines.Name = "txtbxCodeLines";
			this.txtbxCodeLines.ReadOnly = true;
			this.txtbxCodeLines.Size = new System.Drawing.Size(110, 20);
			this.txtbxCodeLines.TabIndex = 3;
			this.txtbxCodeLines.TabStop = false;
			this.txtbxCodeLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblComments
			// 
			this.lblComments.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblComments.Location = new System.Drawing.Point(10, 117);
			this.lblComments.Name = "lblComments";
			this.lblComments.Size = new System.Drawing.Size(140, 23);
			this.lblComments.TabIndex = 6;
			this.lblComments.Text = "Number of Comment Lines:";
			this.lblComments.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// txtbxComments
			// 
			this.txtbxComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxComments.Location = new System.Drawing.Point(404, 114);
			this.txtbxComments.Name = "txtbxComments";
			this.txtbxComments.ReadOnly = true;
			this.txtbxComments.Size = new System.Drawing.Size(110, 20);
			this.txtbxComments.TabIndex = 7;
			this.txtbxComments.TabStop = false;
			this.txtbxComments.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblBlankLines
			// 
			this.lblBlankLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblBlankLines.Location = new System.Drawing.Point(10, 93);
			this.lblBlankLines.Name = "lblBlankLines";
			this.lblBlankLines.Size = new System.Drawing.Size(140, 23);
			this.lblBlankLines.TabIndex = 4;
			this.lblBlankLines.Text = "Number of Blank Lines:";
			this.lblBlankLines.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// txtbxBlankLines
			// 
			this.txtbxBlankLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxBlankLines.Location = new System.Drawing.Point(404, 90);
			this.txtbxBlankLines.Name = "txtbxBlankLines";
			this.txtbxBlankLines.ReadOnly = true;
			this.txtbxBlankLines.Size = new System.Drawing.Size(110, 20);
			this.txtbxBlankLines.TabIndex = 5;
			this.txtbxBlankLines.TabStop = false;
			this.txtbxBlankLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblNumberFiles
			// 
			this.lblNumberFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNumberFiles.Location = new System.Drawing.Point(10, 21);
			this.lblNumberFiles.Name = "lblNumberFiles";
			this.lblNumberFiles.Size = new System.Drawing.Size(133, 23);
			this.lblNumberFiles.TabIndex = 0;
			this.lblNumberFiles.Text = "Number of Files Selected:";
			this.lblNumberFiles.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// txtbxNumberFiles
			// 
			this.txtbxNumberFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxNumberFiles.Location = new System.Drawing.Point(404, 18);
			this.txtbxNumberFiles.Name = "txtbxNumberFiles";
			this.txtbxNumberFiles.ReadOnly = true;
			this.txtbxNumberFiles.Size = new System.Drawing.Size(110, 20);
			this.txtbxNumberFiles.TabIndex = 1;
			this.txtbxNumberFiles.TabStop = false;
			this.txtbxNumberFiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtbxNumberLines
			// 
			this.txtbxNumberLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxNumberLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbxNumberLines.Location = new System.Drawing.Point(404, 138);
			this.txtbxNumberLines.Name = "txtbxNumberLines";
			this.txtbxNumberLines.ReadOnly = true;
			this.txtbxNumberLines.Size = new System.Drawing.Size(110, 20);
			this.txtbxNumberLines.TabIndex = 9;
			this.txtbxNumberLines.TabStop = false;
			this.txtbxNumberLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblNumberLines
			// 
			this.lblNumberLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNumberLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNumberLines.Location = new System.Drawing.Point(10, 141);
			this.lblNumberLines.Name = "lblNumberLines";
			this.lblNumberLines.Size = new System.Drawing.Size(156, 20);
			this.lblNumberLines.TabIndex = 8;
			this.lblNumberLines.Text = "Total Number of Lines:";
			this.lblNumberLines.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// btnGetFiles
			// 
			this.btnGetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGetFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnGetFiles.Location = new System.Drawing.Point(439, 85);
			this.btnGetFiles.Name = "btnGetFiles";
			this.btnGetFiles.Size = new System.Drawing.Size(75, 23);
			this.btnGetFiles.TabIndex = 4;
			this.btnGetFiles.Text = "&Get Files";
			this.btnGetFiles.Click += new System.EventHandler(this.btnGetFiles_Click);
			// 
			// txtbxFileLocation
			// 
			this.txtbxFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxFileLocation.Location = new System.Drawing.Point(10, 87);
			this.txtbxFileLocation.Name = "txtbxFileLocation";
			this.txtbxFileLocation.ReadOnly = true;
			this.txtbxFileLocation.Size = new System.Drawing.Size(423, 20);
			this.txtbxFileLocation.TabIndex = 3;
			this.txtbxFileLocation.TabStop = false;
			// 
			// lblFileLocation
			// 
			this.lblFileLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFileLocation.Location = new System.Drawing.Point(11, 71);
			this.lblFileLocation.Name = "lblFileLocation";
			this.lblFileLocation.Size = new System.Drawing.Size(100, 23);
			this.lblFileLocation.TabIndex = 2;
			this.lblFileLocation.Text = "Location of Files:";
			this.lblFileLocation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// mnuMain
			// 
			this.mnuMain.BackColor = System.Drawing.SystemColors.Control;
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(540, 24);
			this.mnuMain.TabIndex = 0;
			this.mnuMain.Text = "Menu Bar";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp,
            this.mnusepFile1,
            this.mnuExit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(35, 20);
			this.mnuFile.Text = "&File";
			// 
			// mnuHelp
			// 
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Size = new System.Drawing.Size(106, 22);
			this.mnuHelp.Text = "&Help";
			this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
			// 
			// mnusepFile1
			// 
			this.mnusepFile1.Name = "mnusepFile1";
			this.mnusepFile1.Size = new System.Drawing.Size(103, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.Size = new System.Drawing.Size(106, 22);
			this.mnuExit.Text = "E&xit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// grpbxFiles
			// 
			this.grpbxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpbxFiles.Controls.Add(this.radbtnSpecify);
			this.grpbxFiles.Controls.Add(this.radbtnFileExtension);
			this.grpbxFiles.Controls.Add(this.txtbxFileLocation);
			this.grpbxFiles.Controls.Add(this.btnGetFiles);
			this.grpbxFiles.Controls.Add(this.lblFileLocation);
			this.grpbxFiles.Location = new System.Drawing.Point(8, 147);
			this.grpbxFiles.Name = "grpbxFiles";
			this.grpbxFiles.Size = new System.Drawing.Size(524, 117);
			this.grpbxFiles.TabIndex = 11;
			this.grpbxFiles.TabStop = false;
			this.grpbxFiles.Text = "Files";
			// 
			// radbtnSpecify
			// 
			this.radbtnSpecify.AutoSize = true;
			this.radbtnSpecify.Location = new System.Drawing.Point(10, 44);
			this.radbtnSpecify.Name = "radbtnSpecify";
			this.radbtnSpecify.Size = new System.Drawing.Size(148, 17);
			this.radbtnSpecify.TabIndex = 5;
			this.radbtnSpecify.TabStop = true;
			this.radbtnSpecify.Text = "Specify the Files to Count.";
			this.radbtnSpecify.UseVisualStyleBackColor = true;
			this.radbtnSpecify.CheckedChanged += new System.EventHandler(this.radbtnSpecify_CheckedChanged);
			// 
			// radbtnFileExtension
			// 
			this.radbtnFileExtension.AutoSize = true;
			this.radbtnFileExtension.Location = new System.Drawing.Point(10, 21);
			this.radbtnFileExtension.Name = "radbtnFileExtension";
			this.radbtnFileExtension.Size = new System.Drawing.Size(210, 17);
			this.radbtnFileExtension.TabIndex = 4;
			this.radbtnFileExtension.TabStop = true;
			this.radbtnFileExtension.Text = "Specify a Directory to Count Files From.";
			this.radbtnFileExtension.UseVisualStyleBackColor = true;
			this.radbtnFileExtension.CheckedChanged += new System.EventHandler(this.radbtnFileExtension_CheckedChanged);
			// 
			// cmbbxCommentStyle
			// 
			this.cmbbxCommentStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbbxCommentStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbbxCommentStyle.Location = new System.Drawing.Point(107, 19);
			this.cmbbxCommentStyle.Name = "cmbbxCommentStyle";
			this.cmbbxCommentStyle.Size = new System.Drawing.Size(407, 21);
			this.cmbbxCommentStyle.TabIndex = 3;
			this.cmbbxCommentStyle.SelectedIndexChanged += new System.EventHandler(this.cmbbxCommentStyle_SelectedIndexChanged);
			// 
			// lnkReport
			// 
			this.lnkReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkReport.AutoSize = true;
			this.lnkReport.Location = new System.Drawing.Point(5, 455);
			this.lnkReport.Name = "lnkReport";
			this.lnkReport.Size = new System.Drawing.Size(65, 13);
			this.lnkReport.TabIndex = 12;
			this.lnkReport.TabStop = true;
			this.lnkReport.Text = "View Report";
			this.lnkReport.Visible = false;
			this.lnkReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReport_LinkClicked);
			// 
			// lblCommentStyle
			// 
			this.lblCommentStyle.AutoSize = true;
			this.lblCommentStyle.Location = new System.Drawing.Point(7, 22);
			this.lblCommentStyle.Name = "lblCommentStyle";
			this.lblCommentStyle.Size = new System.Drawing.Size(94, 13);
			this.lblCommentStyle.TabIndex = 13;
			this.lblCommentStyle.Text = "Commenting Style:";
			// 
			// grpbxCommentingStyle
			// 
			this.grpbxCommentingStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpbxCommentingStyle.Controls.Add(this.rtxtbxCommentStyle);
			this.grpbxCommentingStyle.Controls.Add(this.cmbbxCommentStyle);
			this.grpbxCommentingStyle.Controls.Add(this.lblCommentStyle);
			this.grpbxCommentingStyle.Location = new System.Drawing.Point(8, 27);
			this.grpbxCommentingStyle.Name = "grpbxCommentingStyle";
			this.grpbxCommentingStyle.Size = new System.Drawing.Size(523, 110);
			this.grpbxCommentingStyle.TabIndex = 14;
			this.grpbxCommentingStyle.TabStop = false;
			this.grpbxCommentingStyle.Text = "Commenting Style";
			// 
			// rtxtbxCommentStyle
			// 
			this.rtxtbxCommentStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtxtbxCommentStyle.BackColor = System.Drawing.SystemColors.Control;
			this.rtxtbxCommentStyle.Location = new System.Drawing.Point(10, 48);
			this.rtxtbxCommentStyle.Name = "rtxtbxCommentStyle";
			this.rtxtbxCommentStyle.ReadOnly = true;
			this.rtxtbxCommentStyle.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtxtbxCommentStyle.Size = new System.Drawing.Size(504, 53);
			this.rtxtbxCommentStyle.TabIndex = 14;
			this.rtxtbxCommentStyle.Text = "";
			// 
			// LineCounter
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(540, 502);
			this.Controls.Add(this.grpbxCommentingStyle);
			this.Controls.Add(this.lnkReport);
			this.Controls.Add(this.grpbxFiles);
			this.Controls.Add(this.grpbxResults);
			this.Controls.Add(this.btnCount);
			this.Controls.Add(this.mnuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuMain;
			this.MinimumSize = new System.Drawing.Size(330, 530);
			this.Name = "LineCounter";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Line Counter";
			this.grpbxResults.ResumeLayout(false);
			this.grpbxResults.PerformLayout();
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.grpbxFiles.ResumeLayout(false);
			this.grpbxFiles.PerformLayout();
			this.grpbxCommentingStyle.ResumeLayout(false);
			this.grpbxCommentingStyle.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.GroupBox grpbxFiles;
		private System.Windows.Forms.ComboBox cmbbxCommentStyle;
		private System.Windows.Forms.RadioButton radbtnFileExtension;
		private System.Windows.Forms.RadioButton radbtnSpecify;
		private System.Windows.Forms.Label lblNumberFilesProcessed;
		private System.Windows.Forms.TextBox txtbxNumberFilesProcessed;
		private System.Windows.Forms.LinkLabel lnkReport;
		private System.Windows.Forms.Label lblCommentStyle;
		private System.Windows.Forms.GroupBox grpbxCommentingStyle;
		private System.Windows.Forms.RichTextBox rtxtbxCommentStyle;

	} // End class.
} // End namespace.