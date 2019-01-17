using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection;
using Reflection.Enums;
using Reflection.Model;

namespace ReflectorTests
{
    [TestClass]
    public class ReflectorTests
    {
        private const string DllFilePath = @"..\..\TPA.ApplicationArchitecture.dll";
        private const string FirstNamespace = "TPA.ApplicationArchitecture.Data";
        private const string SecondNamespace = "TPA.ApplicationArchitecture.Data.CircularReference";
        private AssemblyModel _assemblyModel;

        [TestInitialize]
        public void SetUp()
        {
            Reflector reflector = new Reflector();
            _assemblyModel = reflector.ReflectDll(DllFilePath);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfNamespaces()
        {
            _assemblyModel.NamespaceModels.Count.Should().Be(4);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfTypes()
        {
            List<TypeModel> firstNamespace = _assemblyModel.NamespaceModels.Find(t => t.Name == FirstNamespace).Types;
            List<TypeModel> secondNamespace = _assemblyModel.NamespaceModels.Find(t => t.Name == SecondNamespace).Types;
            firstNamespace.Count.Should().Be(14);
            secondNamespace.Count.Should().Be(2);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfStaticClasses()
        {
            List<TypeModel> staticClasses = _assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types
                .Where(t => t.IsStatic).ToList();
            staticClasses.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfAbstractClasses()
        {
            List<TypeModel> abstractClasses = _assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types
                .Where(t => t.IsAbstract && t.Type == TypeKind.Class && !t.IsStatic).ToList();
            abstractClasses.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfGenericArguments()
        {
            List<TypeModel> genericClasses = _assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.GenericArguments != null)
                .ToList();
            genericClasses.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfClassesWithBaseType()
        {
            List<TypeModel> classesWithBaseType = _assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.BaseType != null).ToList();
            classesWithBaseType.Count.Should().Be(2);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfPropertiesInClass()
        {
            List<TypeModel> classes = _assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.Name == "AbstractClass").ToList();
            classes.First().Properties.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfMethodsInClass()
        {
            List<TypeModel> classes = _assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.Name == "DerivedClass").ToList();
            classes.First().Methods.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfConstructorsInClass()
        {
            List<TypeModel> classes = _assemblyModel.NamespaceModels
                .Find(t => t.Name == FirstNamespace).Types.Where(t => t.Name == "DerivedClass").ToList();
            classes.First().Constructors.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfFieldsInClass()
        {
            List<TypeModel> classes = _assemblyModel.NamespaceModels
                .Find(t => t.Name == SecondNamespace).Types.Where(t => t.Name == "ClassA").ToList();
            classes.First().Fields.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_NoDuplicateReferences()
        {
            TypeModel justType = _assemblyModel.NamespaceModels
                .Find(t => t.Name == SecondNamespace).Types.Find(t => t.Name == "ClassA");

            TypeModel referencedType = _assemblyModel.NamespaceModels
                .Find(t => t.Name == SecondNamespace).Types.Find(t => t.Name == "ClassA")
                .Properties.Find(t => t.Name == "ClassB").Type.Properties.Find(t => t.Name == "ClassA")
                .Type;

            justType.Should().Be(referencedType);
        }
    }
}