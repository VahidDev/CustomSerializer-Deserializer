using System.Reflection;

namespace Demo.Utilities.PropertyUtilities
{
    public static class PropertyValueGetter
    {
        public static object GetPropertyValue(this PropertyInfo prop,object obj)
        {
            return prop.GetValue(obj);
        }
        public static object GetPropertyValue<T>(this PropertyInfo prop, T obj)
        {
            return prop.GetValue(obj);
        }
    }
}
