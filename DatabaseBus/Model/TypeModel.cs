using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataTransferGraph.Enums;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class TypeModel
    {
        public TypeModel()
        {

        }
        public int TypeModelId { get; set; }
        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeModel BaseType { get; set; }

        public List<TypeModel> GenericArguments { get; set; }

        public AccessLevel Accessibility { get; set; }

        public bool IsSealed { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsStatic { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeModel> ImplementedInterfaces { get; set; }

        public List<TypeModel> NestedTypes { get; set; }

        public List<PropertyModel> Properties { get; set; }

        public TypeModel DeclaringType { get; set; }

        public List<MethodModel> Methods { get; set; }

        public List<MethodModel> Constructors { get; set; }

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
