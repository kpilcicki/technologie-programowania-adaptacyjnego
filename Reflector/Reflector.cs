using System.Reflection;
using DataContract.Model;
using Reflector.Model;

namespace Reflector
{
    public class Reflector
    {
        public AssemblyMetadataStorage GetAssemblyMetadata(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
            {
                throw new System.ArgumentNullException($"Could not find assembly file such with path: {assemblyFile}");
            }

            Assembly assembly = Assembly.LoadFrom(assemblyFile);

            return AssemblyLoader.LoadAssemblyMetadata(assembly);
        }
    }
}
