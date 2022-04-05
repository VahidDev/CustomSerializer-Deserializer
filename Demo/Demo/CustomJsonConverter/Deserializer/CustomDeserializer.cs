using System;
using System.Collections.Generic;
using System.Reflection;
using Demo.CustomJsonConverter.CustomObject;
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
            foreach (PropertyInfo prop in props)
            {
                // if the prop type is a custom type
                // then the custom type obj gets created 
                if (prop.IsGivenType(nameof(Nullable)))
                {
                    prop.SetNullableProperty(obj, list);
                }
                else if (!prop.IsGivenType(nameof(System)))
                {
                    object customObj = CustomObjectCreator.CreateCustomObject(list, prop);
                    prop.SetCustomProperty(customObj, obj);
                }
                // if the prop is not a custom type then the system's type gets created 
                else
                {
                    prop.SetSystemProperty(obj, list);
                }
            }
            return obj;
        }
    }
}
