using DataContract.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection;
using Reflection.Model;
using System.Collections.Generic;
using System.Linq;

namespace ReflectionTest
{
    [TestClass]
    public class ReflectorTests 
    {
        private const string DllFilePath = @"..\..\..\ExampleLib\bin\Debug\ExampleLib.dll";
        public const string BasicNamespace = "ExampleLib";
        private const string NamespaceCircle = "ExampleLib.Circle";
        private const string NamespaceTitleplane = "ExampleLib.Titleplane";
        private Reflector _reflector;

        [TestInitialize]
        public void SetUp()
        {
            _reflector = new Reflector(DllFilePath);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfNamespaces()
        {
            _reflector.AssemblyModel.NamespaceModels.Count.Should().Be(3);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfTypes()
        {
            List<TypeModel> firstNamespace = _reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == NamespaceCircle).Types;
            List<TypeModel> secondNamespace = _reflector.AssemblyModel.NamespaceModels.Find(t => t.Name == NamespaceTitleplane).Types;
            firstNamespace.Count.Should().Be(3);
            secondNamespace.Count.Should().Be(3);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfStaticClasses()
        {
            List<TypeModel> staticClasses = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == BasicNamespace).Types
                .Where(t => t.Modifiers.Item4 == StaticEnum.Static).ToList();
           staticClasses.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfAbstractClasses()
        {
            List<TypeModel> abstractClasses = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == NamespaceTitleplane).Types
                .Where(t => t.Modifiers.Item3 == AbstractEnum.Abstract).ToList();
             abstractClasses.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfGenericArguments()
        {
            List<TypeModel> genericClasses = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == NamespaceTitleplane).Types.Where(t => t.GenericArguments != null)
                .ToList();
            genericClasses.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfClassesWithBaseType()
        {
            List<TypeModel> classesWithBaseType = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == NamespaceTitleplane).Types.Where(t => t.BaseType != null).ToList();
            classesWithBaseType.Count.Should().Be(1);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfPropertiesInClass()
        {
            List<TypeModel> classes = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == NamespaceCircle).Types.Where(t => t.Name == "B").ToList();
            classes.First().Properties.Count.Should().Be(2);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfMethodsInClass()
        {
            List<TypeModel> classes = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == NamespaceTitleplane).Types.Where(t => t.Name == "Picture").ToList();
            classes.First().Methods.Count.Should().Be(2);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfConstructorsInClass()
        {
            List<TypeModel> classes = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == NamespaceCircle).Types.Where(t => t.Name == "B").ToList();
            classes.First().Constructors.Count.Should().Be(2);
        }

        [TestMethod]
        public void When_ReflectorConstructorCalled_Expect_CorrectNumberOfFieldsInClass()
        {
            List<TypeModel> classes = _reflector.AssemblyModel.NamespaceModels
                .Find(t => t.Name == NamespaceCircle).Types.Where(t => t.Name == "A").ToList();
            classes.First().Fields.Count.Should().Be(3);
        }
    }
}
