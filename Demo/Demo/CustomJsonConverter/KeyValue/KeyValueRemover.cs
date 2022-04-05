using System.Collections.Generic;
using System.Reflection;

namespace Demo.CustomJsonConverter.KeyValue
{
    public static class KeyValueRemover
    {
        public static void RemoveKeyValuePair
            (this List<KeyValuePair<string, string>> list,PropertyInfo prop)
        {
            list
                .Remove(list
                .Find(p => p.Key.ToLower() == 
                (prop.DeclaringType.Name + "." + prop.Name).ToLower()));
        }
    }
}
