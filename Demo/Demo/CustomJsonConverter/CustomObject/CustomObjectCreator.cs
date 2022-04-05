using System;
using System.Collections.Generic;
using System.Reflection;
using Demo.CustomJsonConverter.KeyValue;
using Demo.Utilities.PropertyUtilities;
using Demo.Utilities.TypeUtilities;

namespace Demo.CustomJsonConverter.CustomObject
{
   public class CustomObjectCreator
    {
        public static object CreateCustomObject
            (List<KeyValuePair<string, string>> list,PropertyInfo propInfo)
        {
            object obj = Activator.CreateInstance(propInfo.PropertyType);
            PropertyInfo[] props = PropertyGetter.GetProperties(propInfo);
            foreach (PropertyInfo prop in props)
            {
                // if it is a custom type
                if (!prop.IsGivenType(nameof(System)))
                {
                    object customObj = CreateCustomObject(list,prop);
                    prop?.SetValue(obj,
                        Convert.ChangeType(customObj, prop.PropertyType), null);
                } 
                // this gets triggered if the prop type is nullable 
                // for example Nullable<long> or long? or int?
                else if (prop.IsGivenType(nameof(Nullable)))
                {
                    prop.SetNullableProperty(obj,list);
                    list.RemoveKeyValuePair(prop);
                }
                // this gets triggered if the prop type is System's type like string, int 
                else
                {
                    prop.SetSystemProperty(obj, list);
                    list.RemoveKeyValuePair(prop);
                }
            }
            return obj;
        }
    }
}
