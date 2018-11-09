using System;
using System.IO;
using System.Reflection;
using DataContract.API;
using DataContract.Model;
using Reflection.Exceptions;

namespace Reflection.AssemblyLoader
{
    public partial class Reflector : IMetadataStorageProvider
    {
        private readonly ILogger _logger;

        public Reflector(ILogger logger)
        {
            _logger = logger;
        }

        public AssemblyMetadataStorage GetMetadataStorage(string assemblyFile)
        {
            try
            {
                if (string.IsNullOrEmpty(assemblyFile))
                {
                    throw new System.ArgumentNullException($"Could not find assembly file such with path: {assemblyFile}");
                }

                Assembly assembly = Assembly.LoadFrom(assemblyFile);
                _logger.Trace("Opening assembly: " + assembly.FullName);

                return LoadAssemblyMetadata(assembly);
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
