using BusinessLogic.Services;
using DataContract.Model;
using DataContract.Model.Enums;
using NUnit.Framework;
using System;

namespace BusinessLogic.Tests.MetadataItemMapperTests
{
    
    class MetadataItemMapperTest
    {
        private MetadataItemMapper _context;

        private const string _assemblyName = "TestAssembly";
        private const string _namespaceName = "TestNamespace";
        private const string _typeName = "TestType";
        private const string _secondTypeName = "2TestType";
        private const string _propertyName = "TestProperty";
        private const string _methodName = "TestMethod";
        private const AccessLevel _typeAccessLevel = AccessLevel.IsPublic;
        private const AccessLevel _methodAccessLevel = AccessLevel.IsPublic;

        [OneTimeSetUp]
        void BeforeAll()
        {
            _context = new MetadataItemMapper();
        }


    }
}
