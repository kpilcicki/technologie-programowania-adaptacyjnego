using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Reflection.Enums;
using Reflection.PersistenceModel;

namespace Reflection.Model
{
    public class MethodModel : IMethodModel
    {
        public string Name { get; set; }

        public List<ITypeModel> GenericArguments { get; set; }

        public AccessLevel Accessibility { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsStatic { get; set; }

        public bool IsVirtual { get; set; }

        public ITypeModel ReturnType { get; set; }

        public bool IsExtensionMethod { get; set; }

        public List<IParameterModel> Parameters { get; set; }

        public MethodModel(MethodBase methodBase)
        {
            Name = methodBase.Name;
            Accessibility = GetAccessibility(methodBase);
            IsAbstract = methodBase.IsAbstract;
            IsExtensionMethod = IsExtension(methodBase);
            IsStatic = methodBase.IsStatic;
            IsVirtual = methodBase.IsVirtual;
            ReturnType = GetReturnType(methodBase);
            GenericArguments = GetGenericArguments(methodBase)?.Select(t => t as ITypeModel).ToList();
            Parameters = GetParameters(methodBase)?.Select(t => t as IParameterModel).ToList();
        }

        public MethodModel(IMethodModel methodBase)
        {
            Name = methodBase.Name;
            Accessibility = methodBase.Accessibility;
            IsAbstract = methodBase.IsAbstract;
            IsExtensionMethod = methodBase.IsExtensionMethod;
            IsStatic = methodBase.IsStatic;
            IsVirtual = methodBase.IsVirtual;

            ReturnType = TypeModel.LoadType(methodBase.ReturnType);
            GenericArguments = methodBase.GenericArguments?.Select(t => TypeModel.LoadType(t) as ITypeModel).ToList();
            Parameters = methodBase.Parameters?.Select(p => new ParameterModel(p) as IParameterModel).ToList();
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
