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
            json= SerializeProps<T>(ref json, obj, props);
            return JsonWriter.WriteEndOfJson(json);
        }
        public static string SerializeCustomObj(ref string json,PropertyInfo propinfo,object obj)
        {
            PropertyInfo[] props = propinfo.PropertyType.GetProperties();
            json = SerializeProps<object>(ref json, obj, props, propinfo);
            return  JsonWriter.WriteEndOfJson(json)+"}";
        }
        public static string SerializeProps<T>
        (ref string json, T obj, PropertyInfo[] props, PropertyInfo propinfo = null)
        {
            if (propinfo != null)
                json = JsonWriter.WriteBeginningOfJson(json, propinfo);
            foreach (PropertyInfo prop in props)
            {
                //If prop doens't have a value then ignore
                //if propinfo is null then call generic methods 
                if (propinfo != null)
                {
                    if (!PropertyValidator.HasPropertyValue(obj, prop)) continue;
                    if (!prop.IsGivenType(nameof(System)))
                    {
                        json += SerializeCustomObj(ref json, prop, prop.GetPropertyValue(obj));
                    }
                }
                else
                {
                    if (!PropertyValidator.HasPropertyValue<T>(obj, prop)) continue;
                    if (!prop.IsGivenType(nameof(System)))
                    {
                        json = SerializeCustomObj(ref json, prop, prop.GetPropertyValue<T>(obj));
                        continue;
                    }
                }
                json = JsonWriter.WriteMiddleOfJson(json, prop, obj);
            }
            return JsonWriter.WriteEndOfJson(json);
        }
    }
}
