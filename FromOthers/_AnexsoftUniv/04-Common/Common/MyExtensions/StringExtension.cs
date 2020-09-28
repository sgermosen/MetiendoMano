using System.Text;
using System.Text.RegularExpressions;

namespace Common.MyExtensions
{
    public static class StringExtension
    {
        public static string Sluglify(this string value)
        {
            //First to lower case
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value.Trim();
        }

        public static string Truncate(this string value, int length = 20)
        {
            return value.Substring(0, length);
        }

        public static string RemoveWhiteSpaces(this string dt)
        {
            return dt.Replace(" ", "");
        }
    }
}
