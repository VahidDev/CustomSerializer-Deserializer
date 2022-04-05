using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Demo.Utilities.AssemblyUtilities;

namespace Demo.Utilities.PropertyUtilities
{
    public class PropertyGetter
    {
        public static PropertyInfo GetProperty
            (Type[] allTypes,Stack<string>typeNames,string key)
        {
            return allTypes
                .FirstOrDefault(t => t.Name.ToLower() == typeNames.Peek().ToLower())
                .GetProperties().FirstOrDefault(p => p.Name.ToLower() == key.ToLower());
        }
        public static PropertyInfo[] GetProperties (PropertyInfo propInfo)
        {
            return AssemblyGetter.GetAssembly()
                .GetType(propInfo.PropertyType.FullName)
                .GetProperties();
        }
    }
}
