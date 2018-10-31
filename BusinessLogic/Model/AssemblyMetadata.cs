using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BusinessLogic.Model
{
    internal class AssemblyMetadata
    {
        public string Name { get; }

        public IEnumerable<NamespaceMetadata> Namespaces { get; }

        internal AssemblyMetadata(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type type in assembly.GetTypes()
                where type.GetVisible()
                group type by type.GetNamespace()
                into namespaceGroup
                orderby namespaceGroup.Key
                select new NamespaceMetadata(namespaceGroup.Key, namespaceGroup);
        }
    }
}