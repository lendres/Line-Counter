namespace DigitalProduction.LineCounter
{
	/// <summary>
	/// Summary not provided for the class LongCommentStyle.
	/// </summary>
	public class LongCommentStyle
	{
		#region Members

		/// <summary>
		/// The string that starts the comment.
		/// </summary>
		//[XmlAttribute("startstring")]
		public string StartString			= "";

		/// <summary>
		/// The string that ends the comment.
		/// </summary>
		//[XmlAttribute("endstring")]
		public string EndString				= "";

		#endregion

		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		public LongCommentStyle() {}

		/// <summary>
		/// Constructor to initialize fields.
		/// </summary>
		/// <param name="startstring">Starting string.</param>
		/// <param name="endstring">End string.</param>
		public LongCommentStyle(string startstring, string endstring)
		{
			StartString = startstring;
			EndString	= endstring;
		}

		#endregion

	} // End class.
} // End namespace.