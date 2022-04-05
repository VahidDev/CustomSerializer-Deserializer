using System.Reflection;
namespace Demo.Utilities.TypeUtilities
{
    public static class TypeChecker
    {
        public static bool IsGivenType(this PropertyInfo prop,string type)
        {
            if (prop.PropertyType.FullName.ToLower().Contains(type.ToLower()))
                return true;
            return false;
        }
    }
}
