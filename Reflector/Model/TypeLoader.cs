using System;
using System.Collections.Generic;
using System.Linq;
using DataContract.Model;
using DataContract.Model.Enums;
using Reflector.ExtensionMethods;

namespace Reflector.Model
{
    internal static class TypeLoader
    {
        internal static TypeMetadataDto EmitReference(Type type)
        {
            if (!type.IsGenericType)
            {
                return LoadTypeMetadataDto(type.Name, type.Namespace);
            }
            else
            {
                return LoadTypeMetadataDto(type.Name, type.GetNamespace(),
                    EmitGenericArguments(type.GetGenericArguments()));
            }
        }

        internal static IEnumerable<TypeMetadataDto> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type argument in arguments select EmitReference(argument);
        }

        internal static TypeMetadataDto LoadTypeMetadataDto(Type type, AssemblyMetadataStorage metaStore)
        {
            if (!metaStore.TypesDictionary.ContainsKey(type.FullName))
            {
                TypeMetadataDto typeMetadataDto = new TypeMetadataDto()
                {
                    Id = type.FullName,
                    TypeName = type.Name,
                    NamespaceName = type.Namespace,
                    Modifiers = EmitModifiers(type),
                    TypeKind = GetTypeKind(type),
                    Attributes = type.GetCustomAttributes(false).Cast<Attribute>()
                };

                metaStore.TypesDictionary.Add(type.FullName, typeMetadataDto);

                typeMetadataDto.DeclaringType = EmitDeclaringType(type.DeclaringType);
                typeMetadataDto.Constructors = MethodLoader.EmitMethods(type.GetConstructors(), metaStore);
                foreach (var method in typeMetadataDto.Constructors)
                {
                    method.Id = $"{type.FullName}.{method.Name}";
                }
                typeMetadataDto.Methods = MethodLoader.EmitMethods(type.GetMethods(), metaStore);
                foreach (var method in typeMetadataDto.Methods)
                {
                    method.Id = $"{type.FullName}.{method.Name}";
                }
                typeMetadataDto.NestedTypes = EmitNestedTypes(type.GetNestedTypes(), metaStore);
                typeMetadataDto.ImplementedInterfaces = EmitImplements(type.GetInterfaces());
                typeMetadataDto.GenericArguments = !type.IsGenericTypeDefinition
                    ? null
                    : TypeLoader.EmitGenericArguments(type.GetGenericArguments());
                typeMetadataDto.BaseType = EmitExtends(type.BaseType);
                typeMetadataDto.Properties = PropertyLoader.EmitProperties(type.GetProperties());
                foreach (var property in typeMetadataDto.Properties)
                {
                    property.Id = $"{type.FullName}.{property.Name}";
                }

                return typeMetadataDto;
            }
            else
            {
                return metaStore.TypesDictionary[type.FullName];
            }
        }

        private static TypeMetadataDto LoadTypeMetadataDto(string typeName, string namespaceName)
        {
            return new TypeMetadataDto()
            {
                TypeName = typeName,
                NamespaceName = namespaceName
            };
        }

        private static TypeMetadataDto LoadTypeMetadataDto(string typeName, string namespaceName,
            IEnumerable<TypeMetadataDto> genericArguments)
        {
            TypeMetadataDto typeMetadataDto = LoadTypeMetadataDto(typeName, namespaceName);
            typeMetadataDto.GenericArguments = genericArguments;
            return typeMetadataDto;
        }

        private static TypeMetadataDto EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
            {
                return null;
            }

            return EmitReference(baseType);
        }

        private static TypeMetadataDto EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
            {
                return null;
            }

            return EmitReference(declaringType);
        }

        private static IEnumerable<TypeMetadataDto> EmitNestedTypes(IEnumerable<Type> nestedTypes,
            AssemblyMetadataStorage metaStore)
        {
            return from type in nestedTypes
                where type.IsVisible()
                select LoadTypeMetadataDto(type, metaStore);
        }

        private static IEnumerable<TypeMetadataDto> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                select EmitReference(currentInterface);
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