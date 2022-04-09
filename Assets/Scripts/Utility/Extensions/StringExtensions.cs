using System.Globalization;
using System.Threading;

public static class StringExtensions
{
    // http://csharphelper.com/blog/2016/12/convert-a-string-to-proper-case-title-case-in-c/
    /// <summary>
    /// Converts a string to proper case. Sometimes called title case.
    /// </summary>
    public static string ToProper(this string str) {
        CultureInfo culture_info = Thread.CurrentThread.CurrentCulture;
        TextInfo text_info = culture_info.TextInfo;
        return text_info.ToTitleCase(str);
    }
}
