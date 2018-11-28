using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DataContract.Model;
using Reflection.Exceptions;
using Reflection.Extensions;
using Reflection.Helpers;
using ServiceContract.Services;
using static Reflection.Helpers.TypeLoaderHelpers;

namespace Reflection
{
    public class Reflector : IReflector
    {
        private string _currentAssemblyName = string.Empty;

        public AssemblyModel ReflectDll(string dllFilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(dllFilePath))
                    throw new ArgumentNullException(nameof(dllFilePath));

                Assembly assembly = Assembly.LoadFrom(dllFilePath);
                AssemblyModel assemblyModel = LoadAssembly(assembly);

                return assemblyModel;
            }
            catch (FileLoadException e)
            {
                throw new AssemblyBlockedException(e.Message);
            }
            catch (Exception e)
            {
                throw new ReflectionException(e.Message);
            }
            finally
            {
                DictionaryTypeSingleton.Instance.Clear();
                _currentAssemblyName = string.Empty;
            }
        }

        private AssemblyModel LoadAssembly(Assembly assembly)
        {
            _currentAssemblyName = assembly.ManifestModule.Name;
            return new AssemblyModel()
            {
                Name = _currentAssemblyName,
                NamespaceModels = assembly.GetTypes()
                    .Where(t => t.IsVisible)
                    .GroupBy(t => t.Namespace)
                    .OrderBy(grouping => grouping.Key)
                    .Select(t => LoadNamespace(t.Key, t.ToList()))
                    .ToList()
            };
        }

        private NamespaceModel LoadNamespace(string namespaceName, List<Type> typesInNamespace)
        {
            return new NamespaceModel()
            {
                Name = namespaceName,
                Types = typesInNamespace
                    .OrderBy(t => t.Name)
                    .Select(LoadType)
                    .ToList()
            };
        }

        private TypeModel LoadType(Type type)
        {
            if (type == null) return null;

            DictionaryTypeSingleton dict = DictionaryTypeSingleton.Instance;
            if (dict.ContainsKey(type.Name))
            {
                return dict.GetType(type.Name);
            }
            else
            {
                return type.Assembly.ManifestModule.Name != _currentAssemblyName ? CreateOutsideType(type) : CreateType(type);
            }
        }

        private TypeModel CreateType(Type type)
        {
            DictionaryTypeSingleton dict = DictionaryTypeSingleton.Instance;

            TypeModel typeModel = new TypeModel();
            dict.RegisterType(type.Name, typeModel);

            typeModel.Name = type.Name;
            typeModel.NamespaceName = type.GetNamespace();
            typeModel.Type = GetTypeKind(type);
            typeModel.IsStatic = type.IsSealed && type.IsAbstract;
            typeModel.IsAbstract = type.IsAbstract;
            typeModel.IsSealed = type.IsSealed;

            typeModel.BaseType = LoadType(GetBaseType(type));
            typeModel.DeclaringType = LoadType(GetDeclaringType(type));

            typeModel.NestedTypes = GetNestedTypes(type)
                .Select(LoadType)
                .ToList();

            typeModel.GenericArguments = GetGenericArguments(type)
                .Select(LoadType)
                .ToList();

            typeModel.ImplementedInterfaces = GetImplementedInterfaces(type)
                .Select(LoadType)
                .ToList();

            typeModel.Properties = GetProperties(type)
                .Select(LoadProperty)
                .ToList();

            typeModel.Fields = GetFields(type)
                .Select(LoadField)
                .ToList();

            IEnumerable<MethodBase> methods = GetConstructors(type);
            typeModel.Constructors = methods
                .Select(LoadMethod)
                .ToList();

            IEnumerable<MethodBase> methods2 = GetMethods(type);

            typeModel.Methods = methods2
                .Select(LoadMethod)
                .ToList();

            return typeModel;
        }

        private TypeModel CreateOutsideType(Type type)
        {
            DictionaryTypeSingleton dict = DictionaryTypeSingleton.Instance;

            TypeModel typeModel = new TypeModel();
            dict.RegisterType(type.Name, typeModel);

            typeModel.Name = type.Name;
            typeModel.NamespaceName = type.GetNamespace();
            typeModel.Type = GetTypeKind(type);
            typeModel.IsStatic = type.IsSealed && type.IsAbstract;
            typeModel.IsAbstract = type.IsAbstract;
            typeModel.IsSealed = type.IsSealed;

            return typeModel;
        }

        private MethodModel LoadMethod(MethodBase method)
        {
            return new MethodModel()
            {
                Name = method.Name,
                Accessibility = MethodLoaderHelpers.GetAccessibility(method),
                IsAbstract = method.IsAbstract,
                IsExtensionMethod = MethodLoaderHelpers.IsExtensionMethod(method),
                IsStatic = method.IsStatic,
                IsVirtual = method.IsVirtual,
                ReturnType = LoadType(MethodLoaderHelpers.GetReturnType(method)),

                GenericArguments = MethodLoaderHelpers.GetGenericArguments(method)
                    .Select(LoadType)
                    .ToList(),

                Parameters = MethodLoaderHelpers
                    .GetParameters(method)
                    .Select(LoadParameter)
                    .ToList()
            };
        }

        private FieldModel LoadField(FieldInfo field)
        {
            return new FieldModel()
            {
                Name = field.Name,
                Type = LoadType(field.FieldType)
            };
        }

        private PropertyModel LoadProperty(PropertyInfo property)
        {
            return new PropertyModel()
            {
                Name = property.Name,
                Type = LoadType(property.PropertyType)
            };
        }

        private ParameterModel LoadParameter(ParameterInfo parameter)
        {
            return new ParameterModel()
            {
                Name = parameter.Name,
                Type = LoadType(parameter.ParameterType)
            };
        }
    }
}