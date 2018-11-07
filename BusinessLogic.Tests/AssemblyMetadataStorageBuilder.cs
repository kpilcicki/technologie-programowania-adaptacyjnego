using System;
using DataContract.Model;
using System.Collections.Generic;
using DataContract.Model.Enums;

namespace BusinessLogic.Tests
{
    public class AssemblyMetadataStorageBuilder
    {
        private AssemblyMetadataStorage _storage;

        public AssemblyMetadataStorage Build()
        {
            return _storage;
        }

        public AssemblyMetadataStorageBuilder WithAssemblyMetadata(string assemblyName)
        {
            AssemblyMetadataDto assemblyMetadata = new AssemblyMetadataDto()
            {
                Id = assemblyName,
                Name = assemblyName,
                Namespaces = new List<NamespaceMetadataDto>()
            };

            _storage = new AssemblyMetadataStorage(assemblyMetadata);

            return this;
        }

        public AssemblyMetadataStorageBuilder WithNamespaceMetaData(string namespaceName)
        {
            NamespaceMetadataDto namespaceData = new NamespaceMetadataDto()
            {
                Id = namespaceName,
                NamespaceName = namespaceName,
                Types = new List<TypeMetadataDto>()
            };

            ((List<NamespaceMetadataDto>) _storage.AssemblyMetadata.Namespaces).Add(namespaceData);
            _storage.NamespacesDictionary.Add(namespaceData.Id, namespaceData);

            return this;
        }

        public AssemblyMetadataStorageBuilder WithTypeMetaData(string namespaceName, string typeName,
            AccessLevel access)
        {
            NamespaceMetadataDto namespaceMetadata = _storage.NamespacesDictionary[namespaceName];
            TypeMetadataDto typeMetadata = CreateSimpleTypeMetadata(namespaceName, typeName, access);
            (namespaceMetadata.Types as List<TypeMetadataDto>).Add(typeMetadata);
            _storage.TypesDictionary.Add(typeMetadata.Id, typeMetadata);

            return this;
        }

        public AssemblyMetadataStorageBuilder WithPropertyMetadata(string typeName, string propertyName,
            string propertyTypeName)
        {
            TypeMetadataDto typeMetadata = _storage.TypesDictionary[typeName];
            typeMetadata.TypeKind = TypeKind.ClassType;

            TypeMetadataDto propertyTypeMetadata = _storage.TypesDictionary[propertyTypeName];
            PropertyMetadataDto propertyMetadata = new PropertyMetadataDto()
            {
                Id = propertyName,
                Name = propertyName,
                TypeMetadata = propertyTypeMetadata
            };
            (typeMetadata.Properties as List<PropertyMetadataDto>).Add(propertyMetadata);
            _storage.PropertiesDictionary.Add(propertyMetadata.Id, propertyMetadata);

            return this;
        }

        public AssemblyMetadataStorageBuilder WithParametrlessVoidMethod(string typeName, string methodName, AccessLevel access)
        {
            TypeMetadataDto typeMetadata = _storage.TypesDictionary[typeName];
            typeMetadata.TypeKind = TypeKind.ClassType;

            MethodMetadataDto methodMetadata = new MethodMetadataDto()
            {
                Id = methodName,
                Name = methodName,
                GenericArguments = new List<TypeMetadataDto>(),
                Modifiers = new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(
                    access,
                    default(AbstractEnum),
                    default(StaticEnum),
                    default(VirtualEnum)),
                ReturnType = null,
                Parameters = new List<ParameterMetadataDto>()
            };

            (typeMetadata.Methods as List<MethodMetadataDto>).Add(methodMetadata);
            _storage.MethodsDictionary.Add(methodMetadata.Id, methodMetadata);

            return this;
        }

        private TypeMetadataDto CreateSimpleTypeMetadata(string namespaceName, string typeName, AccessLevel access)
        {
            return new TypeMetadataDto
            {
                Id = typeName,
                TypeName = typeName,
                NamespaceName = namespaceName,
                Modifiers = new Tuple<AccessLevel, SealedEnum, AbstractEnum>(
                    access,
                    default(SealedEnum),
                    default(AbstractEnum)),
                TypeKind = default(TypeKind),
                Attributes = new List<Attribute>(),
                Properties = new List<PropertyMetadataDto>(),
                Constructors = new List<MethodMetadataDto>(),
                GenericArguments = new List<TypeMetadataDto>(),
                ImplementedInterfaces = new List<TypeMetadataDto>(),
                Methods = new List<MethodMetadataDto>(),
                NestedTypes = new List<TypeMetadataDto>()
            };
        }
    }
}