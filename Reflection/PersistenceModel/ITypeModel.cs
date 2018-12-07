using System.Collections.Generic;
using System.Runtime.Serialization;
using Reflection.Enums;

namespace Reflection.PersistenceModel
{
    public interface ITypeModel
    {
        string Name { get; set; }

        string NamespaceName { get; set; }

        ITypeModel BaseType { get; set; }

        List<ITypeModel> GenericArguments { get; set; }

        AccessLevel Accessibility { get; set; }

        bool IsSealed { get; set; }

        bool IsAbstract { get; set; }

        bool IsStatic { get; set; }

        TypeKind Type { get; set; }

        List<ITypeModel> ImplementedInterfaces { get; set; }

        List<ITypeModel> NestedTypes { get; set; }

        List<IPropertyModel> Properties { get; set; }

        ITypeModel DeclaringType { get; set; }

        List<IMethodModel> Methods { get; set; }

        List<IMethodModel> Constructors { get; set; }

        List<IFieldModel> Fields { get; set; }
    }
}
