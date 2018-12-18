using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataTransferGraph.Enums;
using DataTransferGraph.Model;

namespace FileSerializer.Model
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

        public TypeModel(TypeDtg type)
        {
            DictionaryTypeSingleton.Instance.RegisterType(type.Name, this);
            Name = type.Name;
            NamespaceName = type.NamespaceName;
            Accessibility = type.Accessibility;
            Type = type.Type;
            IsStatic = type.IsStatic;
            IsAbstract = type.IsAbstract;
            IsSealed = type.IsSealed;

            BaseType = TypeModel.LoadType(type.BaseType);
            DeclaringType = TypeModel.LoadType(type.DeclaringType);
            NestedTypes = type.NestedTypes?.Select(t => TypeModel.LoadType(t)).ToList();

            GenericArguments = type.GenericArguments?.Select(t => TypeModel.LoadType(t)).ToList();

            ImplementedInterfaces = type.ImplementedInterfaces?.Select(t => TypeModel.LoadType(t)).ToList();

            Properties = type.Properties?.Select(p => new PropertyModel(p)).ToList();
            Fields = type.Fields?.Select(t => new FieldModel(t)).ToList();
            Constructors = type.Constructors?.Select(c => new MethodModel(c)).ToList();
            Methods = type.Methods?.Select(m => new MethodModel(m)).ToList();
        }

        public static TypeModel LoadType(TypeDtg type)
        {
            if (type == null) return null;
            return DictionaryTypeSingleton.Instance.GetType(type.Name) ?? new TypeModel(type);
        }
    }
}
