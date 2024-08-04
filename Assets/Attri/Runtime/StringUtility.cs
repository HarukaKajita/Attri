using System.Text.RegularExpressions;

namespace Attri.Runtime
{
    public static class StringUtility
    {
        public static string ReplaceCrlf(this string str, string replacement = ",")
        {
            return Regex.Replace(str, @"\r\n|\r|\n", replacement);
        }
    }
}
