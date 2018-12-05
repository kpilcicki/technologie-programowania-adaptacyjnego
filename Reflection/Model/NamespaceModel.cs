using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection.Model
{
    public class NamespaceModel
    {
        public string Name { get; }

        public List<TypeModel> Types { get; }

        public NamespaceModel(string namespaceName, List<Type> types)
        {
            Name = namespaceName;
            Types = types
                .OrderBy(t => t.Name)
                .Select(TypeModel.LoadType)
                .ToList();
        }
    }
}
