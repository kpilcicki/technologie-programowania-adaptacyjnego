using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.Model.Enums;

namespace DataContract.Model
{
    public class TypeMetadataDto
    {
        public Guid  Id { get; set; }
        public string MTypeName { get; set; }
        public string MNamespaceName { get; set; }
        public TypeMetadataDto MBaseType { get; set; }
        public IEnumerable<TypeMetadataDto> MGenericArguments { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractENum> MModifiers { get; set; }
        public TypeKind MTypeKind { get; set; }
        public IEnumerable<Attribute> MAttributes { get; set; }
        public IEnumerable<TypeMetadataDto> MImplementedInterfaces { get; set; }
        public IEnumerable<TypeMetadataDto> MNestedTypes { get; set; }
        public IEnumerable<PropertyMetadataDto> MProperties { get; set; }
        public TypeMetadataDto MDeclaringType { get; set; }
        public IEnumerable<MethodMetadataDto> MMethods { get; set; }
        public IEnumerable<MethodMetadataDto> MConstructors { get; set; }
    }
}