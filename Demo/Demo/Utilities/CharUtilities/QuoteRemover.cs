namespace Demo.Utilities.CharUtilities
{
    public static class QuoteRemover
    {
        public static string RemoveQuotes(this string str)
        {
            str = str.Replace("’", "");
            str = str.Replace("‘", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("“", "");
            str = str.Replace("”", "");
            return str;
        }
    }
}
