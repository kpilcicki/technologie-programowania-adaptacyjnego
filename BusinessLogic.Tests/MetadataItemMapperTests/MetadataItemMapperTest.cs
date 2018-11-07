using BusinessLogic.Model;
using BusinessLogic.Services;
using DataContract.Model;
using DataContract.Model.Enums;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace BusinessLogic.Tests.MetadataItemMapperTests
{
    
    class MetadataItemMapperTest
    {
        private MetadataItemMapper _context;

        private const string _assemblyName = "TestAssembly";
        private const string _namespaceName = "TestNamespace";
        private const string _typeName = "TestType";
        private const string _propertyTypeName = "PropertyType";
        private const string _propertyName = "TestProperty";
        private const string _methodName = "TestMethod";
        private const AccessLevel _typeAccessLevel = AccessLevel.IsPublic;
        private const AccessLevel _methodAccessLevel = AccessLevel.IsPublic;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            _context = new MetadataItemMapper();
        }

        [Test, TestCaseSource("TestCases")]
        public void MapStorageRootTest(AssemblyMetadataStorage storage, MetadataItem expectedRoot)
        {
            var rootItem = _context.Map(storage);

            rootItem.Should().BeEquivalentTo(expectedRoot);
        }

        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(
                    new AssemblyMetadataStorageBuilder().WithAssemblyMetadata(_assemblyName).Build(),
                    new MetadataItem(_assemblyName, false)
                );

                yield return new TestCaseData(
                    new AssemblyMetadataStorageBuilder()
                    .WithAssemblyMetadata(_assemblyName)
                    .WithNamespaceMetaData(_namespaceName)
                    .Build(),
                    new MetadataItem(_assemblyName, true)
                    {
                        Children = { new MetadataItem($"Namespace: {_namespaceName}", false) }
                    }
                );

                yield return new TestCaseData(
                    new AssemblyMetadataStorageBuilder()
                    .WithAssemblyMetadata(_assemblyName)
                    .WithNamespaceMetaData(_namespaceName)
                    .WithTypeMetaData(_namespaceName, _typeName, _typeAccessLevel)
                    .Build(),
                    new MetadataItem(_assemblyName, true)
                    {
                        Children =
                        {
                            new MetadataItem($"Namespace: {_namespaceName}", true)
                            {
                                Children = { new MetadataItem($"Enum: {_typeName}", false)}
                            }
                        }
                    }
                );

                yield return new TestCaseData(
                    new AssemblyMetadataStorageBuilder()
                    .WithAssemblyMetadata(_assemblyName)
                    .WithNamespaceMetaData(_namespaceName)
                    .WithTypeMetaData(_namespaceName, _typeName, _typeAccessLevel)
                    .WithTypeMetaData(_namespaceName, _propertyTypeName, _typeAccessLevel)
                    .WithPropertyMetadata(_typeName, _propertyName, _propertyTypeName)
                    .Build(),
                   new MetadataItem(_assemblyName, true)
                   {
                        Children =
                        {
                            new MetadataItem($"Namespace: {_namespaceName}", true)
                            {
                                Children =
                                {
                                    new MetadataItem($"Class: {_typeName}", true)
                                    {
                                        Children =
                                        {
                                            new MetadataItem($"Property: {_propertyName}", true)
                                            {
                                                Children = { new MetadataItem($"Enum: {_propertyTypeName}", false)}
                                            }
                                        }
                                    },
                                    new MetadataItem($"Enum: {_propertyTypeName}", false)
                                }
                            }
                        }
                   }
                );

                yield return new TestCaseData(
                    new AssemblyMetadataStorageBuilder()
                    .WithAssemblyMetadata(_assemblyName)
                    .WithNamespaceMetaData(_namespaceName)
                    .WithTypeMetaData(_namespaceName, _typeName, _typeAccessLevel)
                    .WithParametrlessVoidMethod(_typeName, _methodName, _methodAccessLevel)
                    .Build(),
                    new MetadataItem(_assemblyName, true)
                    {
                        Children =
                        {
                            new MetadataItem($"Namespace: {_namespaceName}", true)
                            {
                                Children =
                                {
                                    new MetadataItem($"Class: {_typeName}", true)
                                    {
                                        Children = { new MetadataItem($"IsPublic void {_methodName}", false) }
                                    }
                                }
                            }
                        }
                    }
                );
            }
        }  
    }
}
