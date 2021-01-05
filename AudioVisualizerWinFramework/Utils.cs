using System.Text.RegularExpressions;

namespace AudioVisualizerWinFramework
{
    public static class Utils
    {
        public static string StripString(string s)
        {
            string n = Regex.Match(s, "^.*[a-zA-Z0-9].*$").Value;
            return n;
        }
    }
}
