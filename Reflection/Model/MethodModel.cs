using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataTransferGraph.Enums;
using DataTransferGraph.Model;

namespace Reflection.Model
{
    public class MethodModel
    {
        public string Name { get; set; }

        public List<TypeModel> GenericArguments { get; set; }

        public AccessLevel Accessibility { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsStatic { get; set; }

        public bool IsVirtual { get; set; }

        public TypeModel ReturnType { get; set; }

        public bool IsExtensionMethod { get; set; }

        public List<ParameterModel> Parameters { get; set; }

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

        public MethodModel(MethodDtg methodBase)
        {
            Name = methodBase.Name;
            Accessibility = methodBase.Accessibility;
            IsAbstract = methodBase.IsAbstract;
            IsExtensionMethod = methodBase.IsExtensionMethod;
            IsStatic = methodBase.IsStatic;
            IsVirtual = methodBase.IsVirtual;

            ReturnType = TypeModel.LoadType(methodBase.ReturnType);
            GenericArguments = methodBase.GenericArguments?.Select(t => TypeModel.LoadType(t)).ToList();
            Parameters = methodBase.Parameters?.Select(p => new ParameterModel(p)).ToList();
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
