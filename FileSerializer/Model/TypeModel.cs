using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Reflection.Enums;
using Reflection.PersistenceModel;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class TypeModel : ITypeModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ITypeModel BaseType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<ITypeModel> GenericArguments { get; set; }

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
        public List<ITypeModel> ImplementedInterfaces { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<ITypeModel> NestedTypes { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<IPropertyModel> Properties { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ITypeModel DeclaringType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<IMethodModel> Methods { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<IMethodModel> Constructors { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<IFieldModel> Fields { get; set; }

        public TypeModel(ITypeModel type)
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
            NestedTypes = type.NestedTypes?.Select(t => TypeModel.LoadType(t) as ITypeModel).ToList();

            GenericArguments = type.GenericArguments?.Select(t => TypeModel.LoadType(t) as ITypeModel).ToList();

            ImplementedInterfaces = type.ImplementedInterfaces?.Select(t => TypeModel.LoadType(t) as ITypeModel).ToList();

            Properties = type.Properties?.Select(p => new PropertyModel(p) as IPropertyModel).ToList();
            Fields = type.Fields?.Select(t => new FieldModel(t) as IFieldModel).ToList();
            Constructors = type.Constructors?.Select(c => new MethodModel(c) as IMethodModel).ToList();
            Methods = type.Methods?.Select(m => new MethodModel(m) as IMethodModel).ToList();
        }

        public static TypeModel LoadType(ITypeModel type)
        {
            if (type == null) return null;
            return DictionaryTypeSingleton.Instance.GetType(type.Name) ?? new TypeModel(type);
        }
    }
}
