using System;
using System.Linq;
using System.Reflection;
using DataContract.Model;
using Reflector.ExtensionMethods;

namespace Reflector.Model
{
    internal static class AssemblyLoader
    {
        internal static AssemblyMetadataStorage LoadAssemblyMetadata(Assembly assembly)
        {
            AssemblyMetadataDto assemblyMetadata = new AssemblyMetadataDto()
            {
                Name = assembly.ManifestModule.Name,
                FullName = assembly.ManifestModule.FullyQualifiedName
            };

            AssemblyMetadataStorage metaStore = new AssemblyMetadataStorage(assemblyMetadata);

            assemblyMetadata.Namespaces = (from Type type in assembly.GetTypes()
                where type.IsVisible()
                group type by type.GetNamespace() into namespaceGroup
                orderby namespaceGroup.Key
                select NamespaceLoader.LoadNamespaceMetadata(namespaceGroup.Key, namespaceGroup, metaStore)).ToList();

            return metaStore;
        }
    }
}