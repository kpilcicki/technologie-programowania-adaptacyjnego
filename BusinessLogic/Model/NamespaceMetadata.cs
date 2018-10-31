using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Model
{
    internal class NamespaceMetadata
    {
        public string NamespaceName { get; }

        public IEnumerable<TypeMetadata> Types { get; }

        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            NamespaceName = name;
            Types = from type in types orderby type.Name select new TypeMetadata(type);
        }
    }
}