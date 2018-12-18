using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataTransferGraph.Enums;
using DataTransferGraph.Model;
using Reflection.Extensions;

namespace Reflection.Model
{
    public class TypeModel
    {
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

        public TypeModel(Type type)
        {
            DictionaryTypeSingleton.Instance.RegisterType(type.Name, this);
            Name = type.Name;
            NamespaceName = type.GetNamespace();
            Accessibility = GetAccessibility(type);
            Type = GetTypeKind(type);
            IsStatic = type.IsSealed && type.IsAbstract;
            IsAbstract = type.IsAbstract;
            IsSealed = type.IsSealed;

            BaseType = GetBaseType(type);
            DeclaringType = GetDeclaringType(type);
            NestedTypes = GetNestedTypes(type);
            GenericArguments = GetGenericArguments(type);
            ImplementedInterfaces = GetImplementedInterfaces(type);
            Properties = GetProperties(type);
            Fields = GetFields(type);
            Constructors = GetConstructors(type);
            Methods = GetMethods(type);
        }

        public TypeModel(TypeDtg type)
        {
            DictionaryTypeSingleton.Instance.RegisterType(type.Name, this);
            Name = type.Name;
            NamespaceName = type.NamespaceName;
            Accessibility = type.Accessibility;
            Type = type.Type;
            IsStatic = type.IsSealed && type.IsAbstract;
            IsAbstract = type.IsAbstract;
            IsSealed = type.IsSealed;

            BaseType = LoadType(type.BaseType);
            DeclaringType = LoadType(type.DeclaringType);
            NestedTypes = type.NestedTypes?.Select(t => LoadType(t)).ToList();

            GenericArguments = type.GenericArguments?.Select(t => LoadType(t)).ToList();

            ImplementedInterfaces = type.ImplementedInterfaces?.Select(t => LoadType(t)).ToList(); ;

            Properties = type.Properties?.Select(p => new PropertyModel(p)).ToList();
            Fields = type.Fields?.Select(field => new FieldModel(field)).ToList();
            Constructors = type.Constructors?.Select(c => new MethodModel(c)).ToList();
            Methods = type.Methods?.Select(m => new MethodModel(m)).ToList();
        }

        public static TypeModel LoadType(Type type)
        {
            return DictionaryTypeSingleton.Instance.GetType(type.Name) ?? new TypeModel(type);
        }

        public static TypeModel LoadType(TypeDtg type)
        {
            if (type == null) return null;
            return DictionaryTypeSingleton.Instance.GetType(type.Name) ?? new TypeModel(type);
        }

        private static List<TypeModel> GetGenericArguments(Type type)
        {
            if (!type.ContainsGenericParameters) return null;
            return type
                .GetGenericArguments()
                .OrderBy(t => t.Name)
                .Select(LoadType)
                .ToList();
        }

        private static List<MethodModel> GetConstructors(Type type)
        {
            return type
                .GetConstructors()
                .Select(m => new MethodModel(m))
                .ToList();
        }

        private static List<MethodModel> GetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic |
                                   BindingFlags.DeclaredOnly |
                                   BindingFlags.Public |
                                   BindingFlags.Static |
                                   BindingFlags.Instance)
                .Select(m => new MethodModel(m))
                .ToList();
        }

        private static List<PropertyModel> GetProperties(Type type)
        {
            return type
                .GetProperties(BindingFlags.NonPublic |
                               BindingFlags.DeclaredOnly |
                               BindingFlags.Public |
                               BindingFlags.Static |
                               BindingFlags.Instance)
                .Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(p => new PropertyModel(p))
                .ToList();
        }

        private static List<FieldModel> GetFields(Type type)
        {
            return type.GetFields(BindingFlags.NonPublic |
                                  BindingFlags.DeclaredOnly |
                                  BindingFlags.Public |
                                  BindingFlags.Static |
                                  BindingFlags.Instance)
                .OrderBy(f => f.Name)
                .Select(f => new FieldModel(f))
                .ToList();
        }

        private static TypeModel GetBaseType(Type type)
        {
            Type baseType = type.BaseType;
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
                return null;
            return LoadType(baseType);
        }

        private static List<TypeModel> GetNestedTypes(Type type)
        {
            return type
                .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)
                .OrderBy(t => t.Name)
                .Select(LoadType)
                .ToList();
        }

        private static List<TypeModel> GetImplementedInterfaces(Type type)
        {
            return type.GetInterfaces()
                .OrderBy(t => t.Name)
                .Select(LoadType)
                .ToList();
        }

        private static TypeModel GetDeclaringType(Type type)
        {
            Type declaringType = type.DeclaringType;
            if (declaringType == null)
                return null;

            return LoadType(declaringType);
        }

        private static AccessLevel GetAccessibility(Type type)
        {
            return type.IsPublic || type.IsNestedPublic ? AccessLevel.Public :
                type.IsNestedFamily ? AccessLevel.Protected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.Private;
        }

        private static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.Enum :
                type.IsValueType ? TypeKind.Struct :
                type.IsInterface ? TypeKind.Interface :
                TypeKind.Class;
        }
    }
}