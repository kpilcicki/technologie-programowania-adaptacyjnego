using System;
using System.Collections.Generic;
using System.Linq;
using DataTransferGraph.Model;

namespace Reflection.Model
{
    public class NamespaceModel
    {
        public string Name { get; set;  }

        public List<TypeModel> Types { get; set; }

        public NamespaceModel(string namespaceName, List<Type> types)
        {
            Name = namespaceName;
            Types = types
                .OrderBy(t => t.Name)
                .Select(t => TypeModel.LoadType(t))
                .ToList();
        }

        public NamespaceModel(NamespaceDtg namespaceModel)
        {
            Name = namespaceModel.Name;
            Types = namespaceModel.Types?.Select(t => TypeModel.LoadType(t)).ToList();
        }
    }
}
