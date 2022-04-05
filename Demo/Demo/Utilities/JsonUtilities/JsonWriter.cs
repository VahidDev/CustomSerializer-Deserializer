using System.Reflection;
using Demo.Utilities.PropertyUtilities;

namespace Demo.Utilities.JsonUtilities
{
    public class JsonWriter
    {
        public static string WriteBeginningOfJson(string json,PropertyInfo prop)
        {
            return json+ $"\"{prop.Name.ToLower()}\""+ ":"+ "{";
        }
        public static string WriteEndOfJson(string json)
        {
            return json.Remove(json.Length - 1, 1)+ "}";
        }
        public static string WriteMiddleOfJson(string json, PropertyInfo prop,object obj)
        {
            return json+ $"\"{prop.Name.ToLower()}\"" + ":"
                + $"\"{prop.GetPropertyValue(obj)}\""+ ",";
        }
    }
}
