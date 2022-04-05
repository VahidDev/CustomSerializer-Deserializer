namespace Demo.CustomJsonConverter.KeyValue.Validators
{
   public class ValueValidator
    {
        public static bool WrapperQuotesExist(string value)
        {
            if (value[0] == '’' || value[0] == '‘' || value[0] == '\''
             ||value[0]=='`'
                ||value[0] == '\"' || value[0] == '“' || value[0] == '”') return true;
            return false;
        }
    }
}
