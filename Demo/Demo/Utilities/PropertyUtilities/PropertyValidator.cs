using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Demo.Utilities.PropertyUtilities
{
    public class PropertyValidator
    {
        public static bool PropertyExist
            (Type[] allTypes,Stack<string>typeNames,string key)
        {
            if (allTypes
                .FirstOrDefault(t => t.Name.ToLower() == typeNames.Peek().ToLower())
                .GetProperties().Any(p => p.Name.ToLower() == key.ToLower()))
                return true;
            
            return false;
        }
        public static bool HasPropertyValue<T>
            (T obj,PropertyInfo prop)
        {
            if(obj.GetType().GetProperty(prop.Name).GetValue(obj) != null)
                return true;

            return false;
        }
        public static bool HasPropertyValue
            (object obj, PropertyInfo prop)
        {
            if (obj.GetType().GetProperty(prop.Name).GetValue(obj) != null)
                return true;

            return false;
        }
    }
}
