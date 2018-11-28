using System.Collections.Generic;
using System.Runtime.Serialization;
using DataContract.Enums;

namespace DataContract.Model
{
    [DataContract(IsReference = true)]
    public class TypeModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public TypeModel BaseType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<TypeModel> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel Accessibility { get; set; }

        [DataMember]
        public bool IsSealed { get; set; }

        [DataMember]
        public bool IsAbstract { get; set; }

        [DataMember]
        public bool IsStatic { get; set; }

        [DataMember]
        public TypeKind Type { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<TypeModel> ImplementedInterfaces { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<TypeModel> NestedTypes { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<PropertyModel> Properties { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public TypeModel DeclaringType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<MethodModel> Methods { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<MethodModel> Constructors { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<FieldModel> Fields { get; set; }
    }
}
