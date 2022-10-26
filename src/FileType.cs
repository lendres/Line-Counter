using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DigitalProduction.LineCounter
{
	/// <summary>
	/// Summary not provided for the class FileType.
	/// </summary>
	public class FileType
	{
		#region Members / Variables / Delegates.

		private string					_name;
		private List<string>			_extensions				= new List<string>();
		private List<string>			_shortcommentstyles		= new List<string>();
		private List<LongCommentStyle>	_longcommentstyles		= new List<LongCommentStyle>();
		private bool					_longcommentactive		= false;
		private LongCommentStyle		_currentlongcomment;

		#endregion

		#region Construction.

		/// <summary>
		/// Default constructor.
		/// </summary>
		public FileType() { }

		#endregion

		#region Properties.

		/// <summary>
		/// Name of this file type.
		/// </summary>
		[XmlElement("name")]
		public string Name
		{
			get
			{
				return _name;
			}
			
			set
			{
				_name = value + " Files";
			}
		}

		/// <summary>
		/// Comment style.  The name followed by a list of file extensions associated with this
		/// file type / commenting style.
		/// </summary>
		public string CommentStyle
		{
			get
			{
				return _name + ":  " + this.ExtensionList;
			}
		}

		/// <summary>
		/// A semi-colon seperated list of the file extensions associated with this file type.
		/// </summary>
		public string ExtensionList
		{
			get
			{
				string extensionlist = "";
				foreach (string extension in _extensions)
				{
					extensionlist += "*." + extension + ";";
				}

				extensionlist = extensionlist.TrimEnd(';');
				return extensionlist;
			}
		}

		/// <summary>
		/// The filter string used in dialog boxes that is associated with this file type / commenting style.
		/// </summary>
		public string FilterString
		{
			get
			{
				return _name + " (" + this.ExtensionList + ")|" + this.ExtensionList + "|";
			}
		}

		/// <summary>
		/// File extensions.
		/// </summary>
		[XmlArray("extentions"), XmlArrayItem("extension")]
		public List<string> Extensions
		{
			get
			{
				return _extensions;
			}

			set
			{
				_extensions = value;
			}
		}

		/// <summary>
		/// Short comments.  Short comments are a single delimeter that comment everything after it.
		/// </summary>
		[XmlArray("shortcommentstyles"), XmlArrayItem("shortcomment")]
		public List<string> ShortCommentStyles
		{
			get
			{
				return _shortcommentstyles;
			}

			set
			{
				_shortcommentstyles = value;
			}
		}

		/// <summary>
		/// Long comments.  Long comments have a starting and ending dellimeter and comment everything between
		/// the two.
		/// </summary>
		[XmlArray("longcommentstyles"), XmlArrayItem("longcomment")]
		public List<LongCommentStyle> LongCommentStyles
		{
			get
			{
				return _longcommentstyles;
			}

			set
			{
				_longcommentstyles = value;
			}
		}

		#endregion

		#region Functions.

		/// <summary>
		/// Does the file passed have an extension that is associated with this file type / comment style.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
		/// <returns>True if the extension matches one for this file type, false otherwise.</returns>
		public bool IsAssociatedFileType(string filename)
		{
			string fileextension = System.IO.Path.GetExtension(filename);
			fileextension = fileextension.TrimStart('.');

			foreach (string extension in _extensions)
			{
				if (extension == fileextension)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Count the lines in a file.
		/// </summary>
		/// <param name="filename">Name of the file.</param>
		/// <returns>The counts in a LineCounts object.</returns>
		public LineCounts Count(string filename)
		{
			// Line counts.
			LineCounts linecounts = new LineCounts();
			linecounts.IncrementFileCount();

			// Open the file.
			System.IO.StreamReader reader = System.IO.File.OpenText(filename);

			// Loop over the lines in the file.
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				// Count the total number of lines counted.  Should equal comments+blanks+code.
				linecounts.IncrementLineCount();

				// Check for comments.
				if (IsComment(line))
				{
					linecounts.IncrementCommentLineCount();
					continue;
				}

				// Check for blank lines.  Need to do this after checking for comments so that we can count
				// blank lines in long comments as comments.
				if (line.Trim() == "")
				{
					linecounts.IncrementBlankLineCount();
				}
			}

			// Close the file.
			reader.Close();

			return linecounts;
		}

		/// <summary>
		/// Check to see if this is a comment line.
		/// </summary>
		/// <param name="line">Line the check.</param>
		private bool IsComment(string line)
		{
			#region Archive FORTRAN comment code.

			//if (filetype == EFileType.Fortran)
			//{
			//    if (line == "")
			//    {
			//        return false;
			//    }
			//    else
			//    {
			//        if (line.Substring(0, 1) == " ")
			//        {
			//            return false;
			//        }
			//        else
			//        {
			//            for (int i = 0; i < _commentstrings[(int)filetype].Length; i++)
			//            {
			//                if (line.Substring(0, 1) == _commentstrings[(int)filetype][i])
			//                {
			//                    return true;
			//                }
			//            }
			//            return false;
			//        }
			//    }
			//}

			#endregion

			// TODO: Modify this to account for variations of long comment.
			// Now it does not allow for the possibility of a long comment ending and non-comment content
			// to be included on the same line.
			//
			// Example:
			// /* comment
			// */ for (int i = 1 ...
			//
			// The second line should be counted as code.
			//
			// This code does not allow for a long comment ending and another long comment beginning on the
			// same line.
			//
			// Example:
			// /* comment
			// */ for (int i = 1; i < 2; i++)  /*
			// comment
			// */

			// If we are in a long comment, then check for the end.  If we are at the end, turn the
			// switch back to regular mode.
			if (_longcommentactive)
			{
				if (line.Contains(_currentlongcomment.EndString))
				{
					_longcommentactive = false;
				}

				return true;
			}

			// Check for the beginning of a long comment.
			foreach (LongCommentStyle commentstyle in _longcommentstyles)
			{
				if (line.Contains(commentstyle.StartString))
				{
					// If the line contains the end string, then it is a long comment that starts and ends
					// on the same line.  Consider this a regular comment line.
					if (line.Contains(commentstyle.EndString))
					{
						return true;
					}

					// If the line does not contain the end string, then set up for a multi-line comment.
					_longcommentactive	= true;
					_currentlongcomment	= commentstyle;

					// If the line starts with the comment string, consider this a comment line, otherwise
					// assume there is some non-comment on the line and therefore is counted as such.
					if (line.Trim().StartsWith(commentstyle.StartString))
					{
						return true;
					}
				}				
			}

			// Check for a short comment.
			foreach (string commentstyle in _shortcommentstyles)
			{
				if (line.Trim().StartsWith(commentstyle))
			    {
			        return true;
			    }
			}

			return false;
		}



		#endregion

		#region XML.



		#endregion

	} // End class.
} // End namespace.