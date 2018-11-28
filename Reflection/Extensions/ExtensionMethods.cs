using System;
using System.Reflection;

namespace Reflection.Extensions
{
    public static class ExtensionMethods
    {
        internal static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }

        internal static string GetNamespace(this Type type)
        {
            string ns = type.Namespace;
            return ns ?? string.Empty;
        }
    }
}
