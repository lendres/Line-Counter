using System.IO;

namespace DigitalProduction.LineCounter
{
	/// <summary>
	/// Summary not provided for the class LineCounts.
	/// </summary>
	public class LineCounts
	{
		#region Members

		private int		_files				= 0;
		private int		_lines				= 0;
		private int		_commentlines		= 0;
		private int		_blanklines			= 0;

		#endregion

		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		public LineCounts()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Number of files we counted.
		/// </summary>
		public int Files
		{
			get
			{
				return _files;
			}
		}

		/// <summary>
		/// Total number of lines.
		/// </summary>
		public int Lines
		{
			get
			{
				return _lines;
			}
		}

		/// <summary>
		/// Number of lines of code.
		/// </summary>
		public int CodeLines
		{
			get
			{

				return _lines-_commentlines-_blanklines;
			}
		}

		/// <summary>
		/// Number of lines of comments.
		/// </summary>
		public int CommentLines
		{
			get
			{
				return _commentlines;
			}
		}

		/// <summary>
		/// Number of lines that are blank.
		/// </summary>
		public int BlankLines
		{
			get
			{
				return _blanklines;
			}
		}

		#endregion

		#region Functions

		public void IncrementFileCount()
		{
			_files++;
		}

		public void IncrementLineCount()
		{
			_lines++;
		}

		public void IncrementCommentLineCount()
		{
			_commentlines++;
		}

		public void IncrementBlankLineCount()
		{
			_blanklines++;
		}

		public void WriteReport(StreamWriter writer)
		{
			writer.WriteLine("Number of Files Processed: " + _files);
			writer.WriteLine("Number of Lines of Code: " + this.CodeLines);
			writer.WriteLine("Number of Blank Lines: " + _blanklines);
			writer.WriteLine("Number of Comment Lines: " + _commentlines);
			writer.WriteLine("Total Number of Lines: " + _lines);
			writer.WriteLine();
			writer.WriteLine();
			writer.WriteLine();
			writer.WriteLine("The same information is printed below in a format that can be copied into Excel (tab delimited).");
			writer.WriteLine();
			writer.Write("Number of Files Processed\t");
			writer.Write("Number of Lines of Code\t");
			writer.Write("Number of Blank Lines\t");
			writer.Write("Number of Comment Lines\t");
			writer.Write("Total Number of Lines" + writer.NewLine);
			writer.Write(_files + "\t");
			writer.Write(this.CodeLines + "\t");
			writer.Write(_blanklines + "\t");
			writer.Write(_commentlines + "\t");
			writer.Write(_lines);
		}

		public static LineCounts operator+(LineCounts left, LineCounts right)
		{
			LineCounts counts		= new LineCounts();
			counts._files			= left._files			+ right._files;
			counts._lines			= left._lines			+ right._lines;
			counts._commentlines	= left._commentlines	+ right._commentlines;
			counts._blanklines		= left._blanklines		+ right._blanklines;
			return counts;
		}

		#endregion

		#region XML

		#endregion

	} // End class.
} // End namespace.