using System;
using System.Collections.Generic;
using System.Linq;
using DataContract.Model.Enums;

namespace BusinessLogic.Model
{
    internal class TypeMetadata
    {
        private static TypeKind GetTypeKind(Type type) // #80 TPA: Reflection - Invalid return value of GetTypeKind()
        {
            return type.IsEnum ? TypeKind.EnumType :
                type.IsValueType ? TypeKind.StructType :
                type.IsInterface ? TypeKind.InterfaceType :
                TypeKind.ClassType;
        }

        private static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            // set defaults
            AccessLevel access = AccessLevel.IsPrivate;
            AbstractEnum isAbstract = AbstractEnum.NotAbstract;
            SealedEnum isSealed = SealedEnum.NotSealed;

            // check if not default
            if (type.IsPublic)
                access = AccessLevel.IsPublic;
            else if (type.IsNestedPublic)
                access = AccessLevel.IsPublic;
            else if (type.IsNestedFamily)
                access = AccessLevel.IsProtected;
            else if (type.IsNestedFamANDAssem)
                access = AccessLevel.IsProtectedInternal;
            if (type.IsSealed)
                isSealed = SealedEnum.Sealed;
            if (type.IsAbstract)
                isAbstract = AbstractEnum.Abstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(access, isSealed, isAbstract);
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }
        #region constructors

        internal TypeMetadata(Type type)
        {
            _typeName = type.Name;
            _declaringType = EmitDeclaringType(type.DeclaringType);
            _constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            _methods = MethodMetadata.EmitMethods(type.GetMethods());
            _nestedTypes = EmitNestedTypes(type.GetNestedTypes());
            _implementedInterfaces = EmitImplements(type.GetInterfaces());
            _genericArguments = !type.IsGenericTypeDefinition
                ? null
                : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            _modifiers = EmitModifiers(type);
            _baseType = EmitExtends(type.BaseType);
            _properties = PropertyMetadata.EmitProperties(type.GetProperties());
            _typeKind = GetTypeKind(type);
            _attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }

        #endregion

        #region API

        internal enum TypeKind
        {
            EnumType,
            StructType,
            InterfaceType,
            ClassType
        }

        internal static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
            {
                return new TypeMetadata(type.Name, type.GetNamespace());
            }
            else
            {
                return new TypeMetadata(
                    type.Name,
                    type.GetNamespace(),
                    EmitGenericArguments(type.GetGenericArguments()));
            }
        }

        internal static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type argument in arguments select EmitReference(argument);
        }

        #endregion

        #region private

        // vars
        private string _typeName;
        private string _namespaceName;
        private TypeMetadata _baseType;
        private IEnumerable<TypeMetadata> _genericArguments;
        private Tuple<AccessLevel, SealedEnum, AbstractEnum> _modifiers;
        private TypeKind _typeKind;
        private IEnumerable<Attribute> _attributes;
        private IEnumerable<TypeMetadata> _implementedInterfaces;
        private IEnumerable<TypeMetadata> _nestedTypes;
        private IEnumerable<PropertyMetadata> _properties;
        private TypeMetadata _declaringType;
        private IEnumerable<MethodMetadata> _methods;

        private IEnumerable<MethodMetadata> _constructors;

        // constructors
        private TypeMetadata(string typeName, string namespaceName)
        {
            _typeName = typeName;
            _namespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments)
            : this(typeName, namespaceName)
        {
            _genericArguments = genericArguments;
        }

        // methods
        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }

        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from type in nestedTypes
                where type.GetVisible()
                select new TypeMetadata(type);
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                select EmitReference(currentInterface);
        }
        #endregion
    }
}