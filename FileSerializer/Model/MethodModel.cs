using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Reflection.Enums;
using Reflection.PersistenceModel;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class MethodModel : IMethodModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<ITypeModel> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel Accessibility { get; set; }

        [DataMember]
        public bool IsAbstract { get; set; }

        [DataMember]
        public bool IsStatic { get; set; }

        [DataMember]
        public bool IsVirtual { get; set; }

        [DataMember]
        public ITypeModel ReturnType { get; set; }

        [DataMember]
        public bool IsExtensionMethod { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<IParameterModel> Parameters { get; set; }

        public MethodModel(IMethodModel methodModel)
        {
            Name = methodModel.Name;
            GenericArguments = methodModel.GenericArguments;
            Accessibility = methodModel.Accessibility;
            IsAbstract = methodModel.IsAbstract;
            IsStatic = methodModel.IsStatic;
            IsVirtual = methodModel.IsVirtual;
            ReturnType = methodModel.ReturnType;
            IsExtensionMethod = methodModel.IsExtensionMethod;
            Parameters = methodModel.Parameters?.Select(p => new ParameterModel(p) as IParameterModel).ToList();
        }
    }
}
