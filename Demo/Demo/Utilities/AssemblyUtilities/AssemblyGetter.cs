using System;
using System.Linq;
using System.Reflection;

namespace Demo.Utilities.AssemblyUtilities
{
    public static class AssemblyGetter
    {
        public static Assembly GetAssembly()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == nameof(DomainModels));
        }
    }
}
