using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflector.ExtensionMethods
{
    public static class NamespaceExtensions
    {
        public static string AddNamespacePrefix(this string namespaceName)
        {
            return $"Namespace: {namespaceName}";
        }
    }
}
