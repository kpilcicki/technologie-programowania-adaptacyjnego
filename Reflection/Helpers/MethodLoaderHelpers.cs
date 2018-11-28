using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataContract.Enums;

namespace Reflection.Helpers
{
    internal static class MethodLoaderHelpers
    {
        public static IEnumerable<Type> GetGenericArguments(MethodBase method)
        {
            if (!method.IsGenericMethodDefinition)
                return Enumerable.Empty<Type>();
            return method
                .GetGenericArguments();
        }

        public static IEnumerable<ParameterInfo> GetParameters(MethodBase method)
        {
            return method
                .GetParameters();
        }

        public static Type GetReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return methodInfo.ReturnType;
        }

        public static bool IsExtensionMethod(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        public static AccessLevel GetAccessibility(MethodBase method)
        {
            return method.IsPublic? AccessLevel.Public : 
                   method.IsFamily? AccessLevel.Protected :
                   method.IsAssembly? AccessLevel.Internal : AccessLevel.Private;
        }
    }
}
