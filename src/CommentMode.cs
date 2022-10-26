using System.ComponentModel;

namespace DigitalProduction.LineCounter
{
    /// <summary>
    /// Mode used for determining the commenting sytle used.
    /// </summary>
    public enum CommentMode : int
    {
        /// <summary>File extension.</summary>
		[Description("File Extension")]
        FileExtension,

        /// <summary>Specified.</summary>
		[Description("Specified")]
        Specified,

        /// <summary>The number of types/items in the enumeration.</summary>
		[Description("Length")]
        Length
    }
}