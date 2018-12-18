using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferGraph.Model;
using Reflection.Model;

namespace Reflection.Mapper
{
    public static class DataTransferMapper
    {
        public static AssemblyDtg AssemblyDtg(AssemblyModel assemblyModel)
        {
            TypeDtgDictionary = new Dictionary<string, TypeDtg>();
            return new AssemblyDtg()
            {
                Name = assemblyModel.Name,
                NamespaceModels = assemblyModel.NamespaceModels?.Select(NamespaceDtg).ToList()
            };
        }

        private static FieldDtg FieldDtg(FieldModel fieldModel)
        {
            return new FieldDtg()
            {
                Name = fieldModel.Name,
                Type = LoadType(fieldModel.Type)
            };
        }

        private static MethodDtg MethodDtg(MethodModel methodModel)
        {
            return new MethodDtg()
            {
                Name = methodModel.Name,
                GenericArguments = methodModel.GenericArguments?.Select(LoadType).ToList(),
                Accessibility = methodModel.Accessibility,
                IsAbstract = methodModel.IsAbstract,
                IsStatic = methodModel.IsStatic,
                IsVirtual = methodModel.IsVirtual,
                ReturnType = LoadType(methodModel.ReturnType),
                IsExtensionMethod = methodModel.IsExtensionMethod,
                Parameters = methodModel.Parameters?.Select(ParameterDtg).ToList()
            };
        }

        private static NamespaceDtg NamespaceDtg(NamespaceModel namespaceModel)
        {
            return new NamespaceDtg()
            {
                Name = namespaceModel.Name,
                Types = namespaceModel.Types?.Select(LoadType).ToList()
            };
        }

        private static ParameterDtg ParameterDtg(ParameterModel parameterModel)
        {
            return new ParameterDtg()
            {
                Name = parameterModel.Name,
                Type = LoadType(parameterModel.Type)
            };
        }

        private static PropertyDtg PropertyDtg(PropertyModel propertyModel)
        {
            return new PropertyDtg()
            {
                Name = propertyModel.Name,
                Type = LoadType(propertyModel.Type)
            };

        }

        private static TypeDtg TypeDtg(TypeModel type)
        {

            TypeDtg typeDtg = new TypeDtg()
            {
                Name = type.Name,
                NamespaceName = type.NamespaceName,
                Accessibility = type.Accessibility,
                Type = type.Type,
                IsStatic = type.IsStatic,
                IsAbstract = type.IsAbstract,
                IsSealed = type.IsSealed,

               
            };

            TypeDtgDictionary.Add(type.Name, typeDtg);

            typeDtg.BaseType = LoadType(type.BaseType);
            typeDtg.DeclaringType = LoadType(type.DeclaringType);
            typeDtg.NestedTypes = type.NestedTypes?.Select(LoadType).ToList();
            typeDtg.GenericArguments = type.GenericArguments?.Select(LoadType).ToList();
            typeDtg.ImplementedInterfaces = type.ImplementedInterfaces?.Select(LoadType).ToList();
            typeDtg.Properties = type.Properties?.Select(PropertyDtg).ToList();
            typeDtg.Fields = type.Fields?.Select(FieldDtg).ToList();
            typeDtg.Constructors = type.Constructors?.Select(MethodDtg).ToList();
            typeDtg.Methods = type.Methods?.Select(MethodDtg).ToList();

            return typeDtg;
        }

        private static TypeDtg LoadType(TypeModel type)
        {
            if (type == null) return null;
            return GetType(type.Name) ?? TypeDtg(type);
        }

        private static Dictionary<string, TypeDtg> TypeDtgDictionary;
        private static TypeDtg GetType(string key)
        {
            TypeDtgDictionary.TryGetValue(key, out TypeDtg value);
            return value;
        }
    }
}
