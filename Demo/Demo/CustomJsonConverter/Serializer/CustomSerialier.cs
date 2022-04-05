using System.Reflection;
using Demo.Utilities.JsonUtilities;
using Demo.Utilities.PropertyUtilities;
using Demo.Utilities.TypeUtilities;

namespace Demo.CustomJsonConverter.Serializer
{
    public class CustomSerialier
    {
        public static string Serialize<T>(T obj)
        {
            string json = "{";
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                //If prop doens't have a value then ignore
                if (!PropertyValidator.HasPropertyValue<T>(obj,prop)) continue;
                if (!prop.IsGivenType(nameof(System)))
                {
                    json = SerializeCustomObj(ref json,prop,prop.GetPropertyValue<T>(obj));
                    continue;
                }
                json = JsonWriter.WriteMiddleOfJson(json, prop, obj);
            }
            json = json.Remove(json.Length - 1, 1);
            json += "}";
            return json;
        }
        public static string SerializeCustomObj(ref string json,PropertyInfo propinfo,object obj)
        {
            json = JsonWriter.WriteBeginningOfJson(json, propinfo);
            PropertyInfo[] props = propinfo.PropertyType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!PropertyValidator.HasPropertyValue(obj,prop)) continue;
                if (!prop.IsGivenType(nameof(System)))
                {
                    json += SerializeCustomObj(ref json,prop,prop.GetPropertyValue(obj));
                }
                json = JsonWriter.WriteMiddleOfJson(json, prop, obj); 
            }
            json = JsonWriter.WriteEndOfJson(json);
            return json;
        }

    }
}
