using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataContract.Enums;
using Reflection.Model;

namespace Reflection.Loaders
{
    internal static class MethodLoaderHelpers
    {
        public static List<TypeModel> GetGenericArguments(MethodBase method)
        {
            return method
                .GetGenericArguments()
                .Select(TypeLoaderHelpers.LoadTypeModel).ToList();
        }

        public static List<ParameterModel> GetParameters(MethodBase method)
        {
            return method
                .GetParameters()
                .Select(t => new ParameterModel(t.Name, TypeLoaderHelpers.LoadTypeModel(t.ParameterType)))
                .ToList();
        }

        public static TypeModel GetReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeLoaderHelpers.LoadTypeModel(methodInfo.ReturnType);
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
