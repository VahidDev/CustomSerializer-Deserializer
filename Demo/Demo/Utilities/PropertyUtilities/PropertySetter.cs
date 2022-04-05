using System;
using System.Collections.Generic;
using System.Reflection;

namespace Demo.Utilities.PropertyUtilities
{
    public static class PropertySetter
    {
        public static void SetNullableProperty
            (this PropertyInfo prop,object obj,List<KeyValuePair<string,string>>list)
        {
            string fullPropName = obj.GetType().Name + "." + prop.Name;
            if (list.Exists(p => p.Key.ToLower() == fullPropName.ToLower()))
                prop?.SetValue(obj,
                    Convert.ChangeType(fullPropName.ToLower(),
                    Nullable.GetUnderlyingType(prop.PropertyType)), null);
        }
        public static void SetSystemProperty
            (this PropertyInfo prop, object obj, List<KeyValuePair<string, string>> list)
        {
            string fullPropName = obj.GetType().Name + "." + prop.Name;
            if (list.Exists(p => p.Key.ToLower() == fullPropName.ToLower()))
                prop?.SetValue(obj, Convert.ChangeType
                    (list.Find(p => p.Key.ToLower() == fullPropName.ToLower()).Value,
                    prop.PropertyType), null);
        }
        public static void SetCustomProperty
            (this PropertyInfo prop, object customObj,object obj)
        {
            prop?.SetValue(obj,
                    Convert.ChangeType(customObj, prop.PropertyType), null);
        }
    }
}
