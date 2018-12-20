using System.Collections.Generic;
using DataTransferGraph.Enums;

namespace DataTransferGraph.Model
{
    public class TypeDtg
    {
        public string Name { get; set; }

        public string NamespaceName { get; set; }
        public TypeDtg BaseType { get; set; }

        public List<TypeDtg> GenericArguments { get; set; }

        public AccessLevel Accessibility { get; set; }

        public bool IsSealed { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsStatic { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeDtg> ImplementedInterfaces { get; set; }

        public List<TypeDtg> NestedTypes { get; set; }

        public List<PropertyDtg> Properties { get; set; }
        public TypeDtg DeclaringType { get; set; }

        public List<MethodDtg> Methods { get; set; }

        public List<MethodDtg> Constructors { get; set; }

        public List<FieldDtg> Fields { get; set; }
    }
}
