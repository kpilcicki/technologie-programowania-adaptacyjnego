using Reflection.Exceptions;

namespace Reflection.Enums
{
    public static class EnumMapper
    {
        public static AccessLevel GetAccessLevel(DataTransferGraph.Enums.AccessLevel accessLevel)
        {
            switch (accessLevel)
            {
                case DataTransferGraph.Enums.AccessLevel.Internal:
                    return AccessLevel.Internal;
                case DataTransferGraph.Enums.AccessLevel.Private:
                    return AccessLevel.Private;
                case DataTransferGraph.Enums.AccessLevel.Protected:
                    return AccessLevel.Protected;
                case DataTransferGraph.Enums.AccessLevel.Public:
                    return AccessLevel.Public;
                default:
                    throw new ReflectionException("Something unexpected happened");
            }
        }

        public static TypeKind GetTypeKind(DataTransferGraph.Enums.TypeKind typeKind)
        {
            switch (typeKind)
            {
                case DataTransferGraph.Enums.TypeKind.Class:
                    return TypeKind.Class;
                case DataTransferGraph.Enums.TypeKind.Enum:
                    return TypeKind.Enum;
                case DataTransferGraph.Enums.TypeKind.Interface:
                    return TypeKind.Interface;
                case DataTransferGraph.Enums.TypeKind.Struct:
                    return TypeKind.Struct;
                default:
                    throw new ReflectionException("Something unexpected happened");
            }
        }

        public static DataTransferGraph.Enums.AccessLevel GetAccessLevel(AccessLevel accessLevel)
        {
            switch (accessLevel)
            {
                case AccessLevel.Internal:
                    return DataTransferGraph.Enums.AccessLevel.Internal;
                case AccessLevel.Private:
                    return DataTransferGraph.Enums.AccessLevel.Private;
                case AccessLevel.Protected:
                    return DataTransferGraph.Enums.AccessLevel.Protected;
                case AccessLevel.Public:
                    return DataTransferGraph.Enums.AccessLevel.Public;
                default:
                    throw new ReflectionException("Something unexpected happened");
            }
        }

        public static DataTransferGraph.Enums.TypeKind GetTypeKind(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return DataTransferGraph.Enums.TypeKind.Class;
                case TypeKind.Enum:
                    return DataTransferGraph.Enums.TypeKind.Enum;
                case TypeKind.Interface:
                    return DataTransferGraph.Enums.TypeKind.Interface;
                case TypeKind.Struct:
                    return DataTransferGraph.Enums.TypeKind.Struct;
                default:
                    throw new ReflectionException("Something unexpected happened");
            }
        }
    }
}
