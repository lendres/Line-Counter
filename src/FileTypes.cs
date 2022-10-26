using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using DigitalProduction.XML.Serialization;

namespace DigitalProduction.LineCounter
{
	/// <summary>
	/// File types used by line counter.  Also contains comment strings, determines if a string is a comment, 
	/// and constructs the open file dialog filter string.
	/// </summary>
	[XmlRootAttribute("linecounter")]
	public class FileTypes
	{
		#region Members

		private string			_description;
		private List<FileType>	_fileTypes			= new List<FileType>();

		#endregion

		#region Construction

		/// <summary>
		/// Constructor.
		/// </summary>
		public FileTypes()
        {
        }

		#endregion

		#region Properties

		/// <summary>
		/// Number of file types.
		/// </summary>
		[XmlIgnore()]
		public int NumberOfFileTypes
		{
			get
			{
				return _fileTypes.Count;
			}
		}

		/// <summary>
		/// Description of file.
		/// </summary>
		[XmlElement("description")]
		public string Description
		{
			get
			{
				return _description;
			}

			set
			{
				_description = value;
			}
		}

		/// <summary>
		/// File types.
		/// </summary>
		[XmlArray("commentingstyles"), XmlArrayItem("style")]
		public List<FileType> Types
		{
			get
			{
				return _fileTypes;
			}

			set
			{
				_fileTypes = value;
			}
		}

		#endregion

		#region Methods

		#region Counts

		public LineCounts Count(string filename)
		{
			foreach (FileType filetype in _fileTypes)
			{
				if (filetype.IsAssociatedFileType(filename))
				{
					return filetype.Count(filename);
				}
			}

			// Didn't file a file type associated with the file extension of the file name
			// that was passed to use, so we will return all zeros.
			return new LineCounts();
		}

		#endregion

		#region String Construction Functions

		/// <summary>
		/// Returns the file types / comment strings in the form of "name: *.ext1; *.ext2"
		/// </summary>
		/// <returns></returns>
		public string[] GetFileTypes()
		{
			string[] filetypes = new string[_fileTypes.Count];

			int i = 0;
			foreach (FileType filetype in _fileTypes)
			{
				filetypes[i++] = filetype.CommentStyle;
			}

			return filetypes;
		}

		/// <summary>
		/// Returns the filter string used for the get files dialog box.
		/// </summary>
		/// <returns>The filter string.</returns>
		public string GetFilterString()
		{
			string filterstring = "";

			foreach (FileType filetype in _fileTypes)
			{
				filterstring += filetype.FilterString;
			}

			filterstring = filterstring.TrimEnd('|');
			return filterstring;
		}

		#endregion

		#endregion

		#region XML

		/// <summary>
		/// Read the file types from a file.
		/// </summary>
		/// <param name="file">The file to read the file types from.</param>
		/// <returns>The deserialized file types.</returns>
		public static FileTypes ReadFileTypesFile(string file)
		{
			return Serialization.DeserializeObject<FileTypes>(file);
		}

		/// <summary>
		/// Write the file types to a file.
		/// </summary>
		/// <param name="file">The file to write the file types to.</param>
		public void WriteFileTypesFile(string file)
		{
			Serialization.SerializeObject(this, file);
		}

		#endregion

	} // End class.
} // End namespace.