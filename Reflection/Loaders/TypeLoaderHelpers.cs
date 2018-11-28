using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataContract.Enums;
using Reflection.Extensions;
using Reflection.Model;

namespace Reflection.Loaders
{
    internal static class TypeLoaderHelpers
    {
        public static TypeModel LoadTypeModel(Type type)
        {
            DictionaryTypeSingleton dict = DictionaryTypeSingleton.Instance;
            if (dict.ContainsKey(type.Name))
            {
                return dict.GetType(type.Name);
            }
            else
            {
                return CreateType(type);
            }
        }

        private static TypeModel CreateType(Type type)
        {
            DictionaryTypeSingleton dict = DictionaryTypeSingleton.Instance;

            TypeModel typeModel = new TypeModel(type.Name, type.GetNamespace());
            dict.RegisterType(type.Name, typeModel);

            typeModel.Type = GetTypeKind(type);
            typeModel.BaseType = GetBaseType(type);
            typeModel.IsStatic = type.IsSealed && type.IsAbstract;
            typeModel.IsAbstract = type.IsAbstract;
            typeModel.IsSealed = type.IsSealed;
            typeModel.DeclaringType = GetDeclaringType(type);
            typeModel.Constructors = GetConstructors(type);
            typeModel.Methods = GetMethods(type);
            typeModel.NestedTypes = GetNestedTypes(type);
            typeModel.ImplementedInterfaces = GetImplementedInterfaces(type);
            typeModel.GenericArguments = GetGenericArguments(type);
            typeModel.Properties = GetProperties(type);
            typeModel.Fields = GetFields(type);

            return typeModel;
        }

        private static List<TypeModel> GetGenericArguments(Type type)
        {
            return type
                .GetGenericArguments()
                .OrderBy(t => t.Name)
                .Select(TypeLoaderHelpers.LoadTypeModel)
                .ToList();
        }

        private static List<MethodModel> GetConstructors(Type type)
        {
            return type
                .GetConstructors()
                .Select(t => new MethodModel(t))
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
                .Select(t => new PropertyModel(t.Name, TypeLoaderHelpers.LoadTypeModel(t.PropertyType)))
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
                .Select(f => new FieldModel(f.Name, TypeLoaderHelpers.LoadTypeModel(f.FieldType)))
                .ToList();
        }

        private static TypeModel GetBaseType(Type type)
        {
            Type baseType = type.BaseType;
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return TypeLoaderHelpers.LoadTypeModel(baseType);
        }

        private static List<TypeModel> GetNestedTypes(Type type)
        {
            return type
                .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)
                .OrderBy(t => t.Name)
                .Select(TypeLoaderHelpers.LoadTypeModel)
                .ToList();
        }

        private static List<TypeModel> GetImplementedInterfaces(Type type)
        {
            return type.GetInterfaces()
                .OrderBy(t => t.Name)
                .Select(TypeLoaderHelpers.LoadTypeModel)
                .ToList();
        }

        private static TypeModel GetDeclaringType(Type type)
        {
            Type declaringType = type.DeclaringType;
            if (declaringType == null)
                return null;

            return TypeLoaderHelpers.LoadTypeModel(declaringType);
        }

        private static AccessLevel GetAccessibility(Type type)
        {
               return type.IsPublic || type.IsNestedPublic? AccessLevel.Public :
                    type.IsNestedFamily? AccessLevel.Protected :
                    type.IsNestedFamANDAssem? AccessLevel.Internal :
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
