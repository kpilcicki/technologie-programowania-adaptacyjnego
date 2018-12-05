using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Reflection.Enums;

namespace Reflection.Model
{
    public class MethodModel
    {
        public string Name { get; }

        public List<TypeModel> GenericArguments { get; }

        public AccessLevel Accessibility { get; }

        public bool IsAbstract { get; }

        public bool IsStatic { get; }

        public bool IsVirtual { get; }

        public TypeModel ReturnType { get; }

        public bool IsExtensionMethod { get; }

        public List<ParameterModel> Parameters { get; }

        public MethodModel(MethodBase methodBase)
        {
            Name = methodBase.Name;
            Accessibility = GetAccessibility(methodBase);
            IsAbstract = methodBase.IsAbstract;
            IsExtensionMethod = IsExtension(methodBase);
            IsStatic = methodBase.IsStatic;
            IsVirtual = methodBase.IsVirtual;
            ReturnType = GetReturnType(methodBase);
            GenericArguments = GetGenericArguments(methodBase);
            Parameters = GetParameters(methodBase);
        }

        private static List<TypeModel> GetGenericArguments(MethodBase method)
        {
            if (!method.IsGenericMethodDefinition)
                return null;
            return method
                .GetGenericArguments()
                .Select(TypeModel.LoadType)
                .ToList();
        }

        private static List<ParameterModel> GetParameters(MethodBase method)
        {
            return method
                .GetParameters()
                .Select(p => new ParameterModel(p))
                .ToList();
        }

        private static TypeModel GetReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeModel.LoadType(methodInfo.ReturnType);
        }

        private static bool IsExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static AccessLevel GetAccessibility(MethodBase method)
        {
            return method.IsPublic ? AccessLevel.Public :
                method.IsFamily ? AccessLevel.Protected :
                method.IsAssembly ? AccessLevel.Internal : AccessLevel.Private;
        }
    }
}
