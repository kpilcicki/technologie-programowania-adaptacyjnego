using DataContract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class AssemblyMetadataStorageBuilder
    {
        private AssemblyMetadataDto _assemblyMetadata;
        private AssemblyMetadataStorage _storage;

        public AssemblyMetadataStorageBuilder WithAssemblyMetadata(string assemblyName)
        {
            _assemblyMetadata = new AssemblyMetadataDto()
            {
                Id = assemblyName,
                Name = assemblyName,
                Namespaces = new List<NamespaceMetadataDto>()
            };

            _storage = new AssemblyMetadataStorage(_assemblyMetadata);

            return this;
        }

        public AssemblyMetadataStorageBuilder WithNamespaceMetaData(string namespaceName)
        {
            NamespaceMetadataDto namespaceData = new NamespaceMetadataDto()
            {
                Id = namespaceName,
                NamespaceName = namespaceName,
                Types = new List<TypeMetadataDto>()
            };

            ((List<NamespaceMetadataDto>)_assemblyMetadata.Namespaces).Add(namespaceData);
            _storage.NamespacesDictionary.Add(namespaceData.Id, namespaceData);

            return this;
        }
    }
}
