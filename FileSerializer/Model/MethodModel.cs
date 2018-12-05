using System.Collections.Generic;
using System.Runtime.Serialization;
using Reflection.Enums;

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
    }
}
