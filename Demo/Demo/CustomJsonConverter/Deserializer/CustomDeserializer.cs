using System;
using System.Collections.Generic;
using System.Reflection;
using Demo.CustomJsonConverter.KeyValue;
using Demo.Utilities.PropertyUtilities;
using Demo.Utilities.TypeUtilities;

namespace Demo.CustomJsonConverter.Deserializer
{
    public static class CustomDeserializer
    {
        public static T Deserialize<T>(string json) where T:class,new()
        {
            T obj = new();
          
            List<KeyValuePair<string, string>> list = 
                KeyValueCreator.CreateKeyValueList(json,obj.GetType());

            PropertyInfo[] props = obj.GetType().GetProperties();

            return DerializeProps<T>(obj,props,list);
        }

        public static object CreateCustomObject(List<KeyValuePair<string, string>> list, PropertyInfo propInfo)
        {
            object obj = Activator.CreateInstance(propInfo.PropertyType);
            PropertyInfo[] props = PropertyGetter.GetProperties(propInfo);

            return DerializeProps<object>(obj, props, list);
        }

        public static T DerializeProps<T>(T obj, PropertyInfo[] props, List<KeyValuePair<string, string>> list)
        {
            foreach (PropertyInfo prop in props)
            {
                if (!prop.IsGivenType(nameof(System)))
                {
                    object customObj = CreateCustomObject(list, prop);
                    prop.SetCustomProperty(customObj, obj);
                }
                else if (prop.IsGivenType(nameof(Nullable)))
                {
                    prop.SetNullableProperty(obj, list);
                }
                else
                {
                    prop.SetSystemProperty(obj, list);
                }
            }

            return obj;
        }
    }
}
