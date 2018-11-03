using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataContract.Model;
using DataContract.Model.Enums;
using Reflector.ExtensionMethods;

namespace Reflector.Model
{
    internal static class TypeLoader
    {
        internal static IEnumerable<TypeMetadataDto> EmitGenericArguments(IEnumerable<Type> arguments, AssemblyMetadataStorage metaStore)
        {
            return from Type argument in arguments select LoadTypeMetadataDto(argument, metaStore);
        }

        internal static TypeMetadataDto LoadTypeMetadataDto(Type type, AssemblyMetadataStorage metaStore)
        {
            if (!metaStore.TypesDictionary.ContainsKey(type.FullName))
            {
                TypeMetadataDto metadataType;
                if (type.Assembly.ManifestModule.FullyQualifiedName != metaStore.AssemblyMetadata.Id)
                {
                    metadataType = new TypeMetadataDto()
                    {
                        Id = type.FullName,
                        TypeName = type.Name,
                        NamespaceName = type.Namespace,
                        Modifiers = EmitModifiers(type),
                        TypeKind = GetTypeKind(type),
                        Attributes = type.GetCustomAttributes(false).Cast<Attribute>()
                    };
                    metadataType.Properties = new List<PropertyMetadataDto>();
                    metadataType.Attributes = new List<Attribute>();
                    metadataType.Constructors = new List<MethodMetadataDto>();
                    metadataType.GenericArguments = new List<TypeMetadataDto>();
                    metadataType.ImplementedInterfaces = new List<TypeMetadataDto>();
                    metadataType.Methods = new List<MethodMetadataDto>();
                    metadataType.NestedTypes = new List<TypeMetadataDto>();

                    metaStore.TypesDictionary.Add(type.FullName, metadataType);
                }
                else
                {
                    metadataType = new TypeMetadataDto()
                    {
                        Id = type.FullName,
                        TypeName = type.Name,
                        NamespaceName = type.Namespace,
                        Modifiers = EmitModifiers(type),
                        TypeKind = GetTypeKind(type),
                        Attributes = type.GetCustomAttributes(false).Cast<Attribute>()
                    };

                    metaStore.TypesDictionary.Add(type.FullName, metadataType);

                    metadataType.DeclaringType = EmitDeclaringType(type.DeclaringType, metaStore);
                    metadataType.Constructors = MethodLoader.EmitMethods(type.GetConstructors(), metaStore);

                    metadataType.Methods =
                        MethodLoader.EmitMethods(type.GetMethods(BindingFlags.DeclaredOnly), metaStore);

                    metadataType.NestedTypes = EmitNestedTypes(type.GetNestedTypes(), metaStore);
                    metadataType.ImplementedInterfaces = EmitImplements(type.GetInterfaces(), metaStore);
                    metadataType.GenericArguments = !type.IsGenericTypeDefinition
                        ? new List<TypeMetadataDto>()
                        : TypeLoader.EmitGenericArguments(type.GetGenericArguments(), metaStore);
                    metadataType.BaseType = EmitExtends(type.BaseType, metaStore);
                    metadataType.Properties = PropertyLoader.EmitProperties(type.GetProperties(), metaStore);
                }

                return metadataType;
            }
            else
            {
                return metaStore.TypesDictionary[type.FullName];
            }
        }


        private static TypeMetadataDto EmitExtends(Type baseType, AssemblyMetadataStorage metaStore)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
            {
                return null;
            }

            return LoadTypeMetadataDto(baseType, metaStore);
        }

        private static TypeMetadataDto EmitDeclaringType(Type declaringType, AssemblyMetadataStorage metaStore)
        {
            if (declaringType == null)
            {
                return null;
            }

            return LoadTypeMetadataDto(declaringType, metaStore);
        }

        private static IEnumerable<TypeMetadataDto> EmitNestedTypes(IEnumerable<Type> nestedTypes,
            AssemblyMetadataStorage metaStore)
        {
            return (from type in nestedTypes
                where type.IsVisible()
                select LoadTypeMetadataDto(type, metaStore)).ToList();
        }

        private static IEnumerable<TypeMetadataDto> EmitImplements(IEnumerable<Type> interfaces, AssemblyMetadataStorage metaStore)
        {
            return (from currentInterface in interfaces
                select LoadTypeMetadataDto(currentInterface, metaStore)).ToList();
        }

        private static TypeKind GetTypeKind(Type type) // #80 TPA: Reflection - Invalid return value of GetTypeKind()
        {
            return type.IsEnum ? TypeKind.EnumType :
                type.IsValueType ? TypeKind.StructType :
                type.IsInterface ? TypeKind.InterfaceType :
                TypeKind.ClassType;
        }

        private static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            AccessLevel accessLevel = AccessLevel.IsPrivate;
            AbstractEnum abstractEnum = AbstractEnum.NotAbstract;
            SealedEnum sealedEnum = SealedEnum.NotSealed;

            // check if not default
            if (type.IsPublic)
            {
                accessLevel = AccessLevel.IsPublic;
            }
            else if (type.IsNestedPublic)
            {
                accessLevel = AccessLevel.IsPublic;
            }
            else if (type.IsNestedFamily)
            {
                accessLevel = AccessLevel.IsProtected;
            }
            else if (type.IsNestedFamANDAssem)
            {
                accessLevel = AccessLevel.IsProtectedInternal;
            }

            if (type.IsSealed)
            {
                sealedEnum = SealedEnum.Sealed;
            }

            if (type.IsAbstract)
            {
                abstractEnum = AbstractEnum.Abstract;
            }

            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(accessLevel, sealedEnum, abstractEnum);
        }
    }
}