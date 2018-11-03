using System;
using System.Collections.Generic;
using System.Linq;
using DataContract.Model;
using Reflector.ExtensionMethods;

namespace Reflector.Model
{
    internal static class NamespaceLoader
    {
        internal static NamespaceMetadataDto LoadNamespaceMetadata(string name, IEnumerable<Type> types, AssemblyMetadataStorage metaStore)
        {
            NamespaceMetadataDto namespaceMetadata = new NamespaceMetadataDto()
            {
                Id = name.AddNamespacePrefix(),
                NamespaceName = name.AddNamespacePrefix()
            };

            metaStore.NamespacesDictionary.Add(namespaceMetadata.NamespaceName, namespaceMetadata);

            namespaceMetadata.Types = (from type in types orderby type.Name select TypeLoader.LoadTypeMetadataDto(type, metaStore)).ToList();

            return namespaceMetadata;   
        }
    }
}