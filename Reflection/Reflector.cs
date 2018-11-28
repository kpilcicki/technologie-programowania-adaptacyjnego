using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DataContract.Model;
using Reflection.Exceptions;
using Reflection.Extensions;
using Reflection.Helpers;
using static Reflection.Helpers.TypeLoaderHelpers;

namespace Reflection
{
    public class Reflector
    {
        public AssemblyModel AssemblyModel { get; set; }

        public Reflector(string assemblyPath)
        {
            try
            {
                if (string.IsNullOrEmpty(assemblyPath))
                    throw new System.ArgumentNullException();
                Assembly assembly = Assembly.LoadFrom(assemblyPath);

                DictionaryTypeSingleton.Instance.Clear();

                AssemblyModel = LoadAssembly(assembly);
            }
            catch (FileLoadException e)
            {
                throw new AssemblyBlockedException(e.Message);
            }
            catch (Exception e)
            {
                throw new ReflectionException(e.Message);
            }
        }

        public AssemblyModel LoadAssembly(Assembly assembly)
        {
            AssemblyModel assemblyModule = new AssemblyModel(assembly.ManifestModule.Name);
            AssemblyModel = assemblyModule;
            assemblyModule.NamespaceModels = assembly.GetTypes()
                .Where(t => t.IsVisible)
                .GroupBy(t => t.Namespace)
                .OrderBy(grouping => grouping.Key)
                .Select(t => LoadNamespace(t.Key, t.ToList()))
                .ToList();

            return assemblyModule;
        }

        public NamespaceModel LoadNamespace(string namespaceName, List<Type> typesInNamespace)
        {
            return new NamespaceModel(namespaceName)
            {
                Types = typesInNamespace
                    .OrderBy(t => t.Name)
                    .Select(LoadType)
                    .ToList()
            };
        }

        public TypeModel LoadType(Type type)
        {
            if (type == null) return null;
            DictionaryTypeSingleton dict = DictionaryTypeSingleton.Instance;
            if (dict.ContainsKey(type.Name))
            {
                return dict.GetType(type.Name);
            }
            else
            {
                if (type.Assembly.ManifestModule.Name != AssemblyModel.Name)
                {
                    TypeModel typeModel = new TypeModel(type.Name, type.GetNamespace());
                    dict.RegisterType(type.Name, typeModel);
                    return typeModel;
                }

                return CreateType(type);
            }
        }

        private TypeModel CreateType(Type type)
        {
            DictionaryTypeSingleton dict = DictionaryTypeSingleton.Instance;

            TypeModel typeModel = new TypeModel(type.Name, type.GetNamespace());
            dict.RegisterType(type.Name, typeModel);

            typeModel.Type = GetTypeKind(type);
            typeModel.IsStatic = type.IsSealed && type.IsAbstract;
            typeModel.IsAbstract = type.IsAbstract;
            typeModel.IsSealed = type.IsSealed;

            typeModel.BaseType = LoadType(GetBaseType(type));
            typeModel.DeclaringType = LoadType(GetDeclaringType(type));

            typeModel.NestedTypes = GetNestedTypes(type).Select(LoadType).ToList();
            typeModel.GenericArguments = GetGenericArguments(type).Select(LoadType).ToList();
            typeModel.ImplementedInterfaces = GetImplementedInterfaces(type).Select(LoadType).ToList();


            typeModel.Properties = GetProperties(type).Select(LoadProperty).ToList();
            typeModel.Fields = GetFields(type).Select(LoadField).ToList();
            IEnumerable<MethodBase> methods = GetConstructors(type);
            typeModel.Constructors = methods.Select(LoadMethod).ToList();
            typeModel.Methods = GetMethods(type).Select(LoadMethod).ToList();

            return typeModel;
        }

        private MethodModel LoadMethod(MethodBase method)
        {
            return new MethodModel(method.Name)
            {
                Accessibility = MethodLoaderHelpers.GetAccessibility(method),
                GenericArguments = MethodLoaderHelpers.GetGenericArguments(method).Select(LoadType).ToList(),
                IsAbstract = method.IsAbstract,
                IsExtensionMethod = MethodLoaderHelpers.IsExtensionMethod(method),
                IsStatic = method.IsStatic,
                IsVirtual = method.IsVirtual,
                Parameters = MethodLoaderHelpers.GetParameters(method).Select(LoadParameter).ToList(),
                ReturnType = LoadType(MethodLoaderHelpers.GetReturnType(method))
            };
        }

        private FieldModel LoadField(FieldInfo field)
        {
            return new FieldModel(
                field.Name,
                LoadType(field.FieldType)
            );
        }

        private PropertyModel LoadProperty(PropertyInfo property)
        {
            return new PropertyModel(
                property.Name,
                LoadType(property.PropertyType)
            );
        }

        private ParameterModel LoadParameter(ParameterInfo parameter)
        {
            return new ParameterModel(
                parameter.Name,
                LoadType(parameter.ParameterType)
            );
        }
    }
}