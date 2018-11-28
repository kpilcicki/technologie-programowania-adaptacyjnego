using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using DataContract.Enums;
using Reflection.Loaders;

namespace Reflection.Model
{
    [DataContract(IsReference = true)]
    public class MethodModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<TypeModel> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel Accessibility { get; set; }

        [DataMember]
        public bool IsAbstract { get; set; }

        [DataMember]
        public bool IsStatic { get; set; }

        [DataMember]
        public bool IsVirtual { get; set; }
        
        [DataMember]
        public TypeModel ReturnType { get; set; }

        [DataMember]
        public bool IsExtensionMethod { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<ParameterModel> Parameters { get; set; }

        public MethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : MethodLoaderHelpers.GetGenericArguments(method);
            ReturnType = MethodLoaderHelpers.GetReturnType(method);
            Parameters = MethodLoaderHelpers.GetParameters(method);
            Accessibility = MethodLoaderHelpers.GetAccessibility(method);
            IsAbstract = method.IsAbstract;
            IsStatic = method.IsStatic;
            IsVirtual = method.IsVirtual;
            IsExtensionMethod = MethodLoaderHelpers.IsExtensionMethod(method);
        }
    }
}
