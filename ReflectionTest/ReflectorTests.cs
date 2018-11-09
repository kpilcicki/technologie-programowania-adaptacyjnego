using NUnit.Framework;
using Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    [TestFixture]
    public class ReflectorTests 
    {
        private const string DllFilePath = @"..\..\..\ExampleLib\bin\Debug\ExampleLib.dll";
        private Reflector _reflector;

        [SetUp]
        public void SetUp()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _reflector = new Reflector(DllFilePath);
        }

        [Test]
        public void CheckAmountOfNamespaces()
        {
            Assert.AreEqual(2, _reflector.AssemblyModel.NamespaceModels.Count);
        }

    }
}
