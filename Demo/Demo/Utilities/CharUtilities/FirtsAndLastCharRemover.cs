namespace Demo.Utilities.CharUtilities
{
   public static class FirtsAndLastCharRemover
    {
        public static string RemoveFirtsAndLastChar(this string str)
        {
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            return str;
        }
    }
}
