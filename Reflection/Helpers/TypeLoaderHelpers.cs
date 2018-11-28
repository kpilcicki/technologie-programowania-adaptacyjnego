using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataContract.Enums;
using Reflection.Extensions;

namespace Reflection.Helpers
{
    internal static class TypeLoaderHelpers
    {

        public static IEnumerable<Type> GetGenericArguments(Type type)
        {
            if (!type.ContainsGenericParameters) return Enumerable.Empty<Type>();
            return type
                .GetGenericArguments()
                .OrderBy(t => t.Name);
        }

        public static IEnumerable<MethodBase> GetConstructors(Type type)
        {
            return type
                .GetConstructors();
        }

        public static IEnumerable<MethodBase> GetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic |
                                   BindingFlags.DeclaredOnly |
                                   BindingFlags.Public |
                                   BindingFlags.Static |
                                   BindingFlags.Instance);
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type
                .GetProperties(BindingFlags.NonPublic |
                               BindingFlags.DeclaredOnly |
                               BindingFlags.Public |
                               BindingFlags.Static |
                               BindingFlags.Instance)
                .Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible());
        }

        public static IEnumerable<FieldInfo> GetFields(Type type)
        {
            return type.GetFields(BindingFlags.NonPublic |
                                  BindingFlags.DeclaredOnly |
                                  BindingFlags.Public |
                                  BindingFlags.Static |
                                  BindingFlags.Instance)
                .OrderBy(f => f.Name);
        }

        public static Type GetBaseType(Type type)
        {
            Type baseType = type.BaseType;
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return baseType;
        }

        public static IEnumerable<Type> GetNestedTypes(Type type)
        {
            return type
                .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)
                .OrderBy(t => t.Name);
        }

        public static IEnumerable<Type> GetImplementedInterfaces(Type type)
        {
            return type.GetInterfaces()
                .OrderBy(t => t.Name);
        }

        public static Type GetDeclaringType(Type type)
        {
            Type declaringType = type.DeclaringType;
            if (declaringType == null)
                return null;

            return declaringType;
        }

        public static AccessLevel GetAccessibility(Type type)
        {
               return type.IsPublic || type.IsNestedPublic? AccessLevel.Public :
                    type.IsNestedFamily? AccessLevel.Protected :
                    type.IsNestedFamANDAssem? AccessLevel.Internal :
                    AccessLevel.Private;
        }

        public  static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.Enum :
                type.IsValueType ? TypeKind.Struct :
                type.IsInterface ? TypeKind.Interface :
                TypeKind.Class;
        }
    }
}
