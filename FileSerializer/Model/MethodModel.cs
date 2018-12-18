using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataTransferGraph.Enums;
using DataTransferGraph.Model;

namespace FileSerializer.Model
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

        public MethodModel(MethodDtg methodModel)
        {
            Name = methodModel.Name;
            GenericArguments = methodModel.GenericArguments?.Select(TypeModel.LoadType).ToList();
            Accessibility = methodModel.Accessibility;
            IsAbstract = methodModel.IsAbstract;
            IsStatic = methodModel.IsStatic;
            IsVirtual = methodModel.IsVirtual;
            ReturnType = TypeModel.LoadType(methodModel.ReturnType);
            IsExtensionMethod = methodModel.IsExtensionMethod;
            Parameters = methodModel.Parameters?.Select(p => new ParameterModel(p)).ToList();
        }
    }
}
