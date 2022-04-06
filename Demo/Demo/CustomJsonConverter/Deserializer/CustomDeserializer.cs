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
            #region KeyValue List 
            // this creates a list of key value pairs that
            // has all the props for our T type object
            #endregion
            List<KeyValuePair<string, string>> list = 
                KeyValueCreator.CreateKeyValueList(json,obj.GetType());
            PropertyInfo[] props = obj.GetType().GetProperties();
            return DerializeProps<T>(obj,props,list);
        }
        public static object CreateCustomObject
           (List<KeyValuePair<string, string>> list, PropertyInfo propInfo)
        {
            object obj = Activator.CreateInstance(propInfo.PropertyType);
            PropertyInfo[] props = PropertyGetter.GetProperties(propInfo);
            return DerializeProps<object>(obj, props, list);
        }
        public static T DerializeProps<T>
        (T obj, PropertyInfo[] props, List<KeyValuePair<string, string>> list)
        {
            foreach (PropertyInfo prop in props)
            {
                // if it is a custom type
                if (!prop.IsGivenType(nameof(System)))
                {
                    object customObj = CreateCustomObject(list, prop);
                    prop.SetCustomProperty(customObj, obj);
                }
                // this gets triggered if the prop type is nullable 
                // for example Nullable<long> or long? or int?
                else if (prop.IsGivenType(nameof(Nullable)))
                {
                    prop.SetNullableProperty(obj, list);
                }
                // this gets triggered if the prop type is System's type like string, int 
                else
                {
                    prop.SetSystemProperty(obj, list);
                }
            }
            return obj;
        }
    }
}
