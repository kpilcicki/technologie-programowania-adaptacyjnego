using BusinessLogic.Services;
using DataContract.Model;
using DataContract.Model.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

         public static IEnumerable<> TestCases
    {
        get
        {
            yield return new TestCaseData(12, 3).Returns(4);
            yield return new TestCaseData(12, 2).Returns(6);
            yield return new TestCaseData(12, 4).Returns(3);
        }
    }  

        [Test]


    }
}
