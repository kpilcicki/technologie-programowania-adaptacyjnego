using System;
using System.IO;
using System.Reflection;
using Reflection.Exceptions;
using Reflection.Model;

namespace Reflection
{
    public class Reflector
    {
        public AssemblyModel AssemblyModel { get; private set; }

        public Reflector(string assemblyPath)
        {
            try
            {
                if (string.IsNullOrEmpty(assemblyPath))
                    throw new System.ArgumentNullException();
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                AssemblyModel = new AssemblyModel(assembly);
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
    }
}
