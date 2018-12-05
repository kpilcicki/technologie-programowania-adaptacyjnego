using System;
using System.IO;
using System.Reflection;
using Reflection.Exceptions;
using Reflection.Model;

namespace Reflection
{
    public class Reflector
    { 

        public AssemblyModel ReflectDll(string dllFilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(dllFilePath))
                    throw new ArgumentNullException(nameof(dllFilePath));

                Assembly assembly = Assembly.LoadFrom(dllFilePath);
                AssemblyModel assemblyModel = new AssemblyModel(assembly);

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
            }
        }
    }
}