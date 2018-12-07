using Reflection.PersistenceModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection.Model
{
    public class NamespaceModel : INamespaceModel
    {
        public string Name { get; set;  }

        public List<ITypeModel> Types { get; set; }

        public NamespaceModel(string namespaceName, List<Type> types)
        {
            Name = namespaceName;
            Types = types
                .OrderBy(t => t.Name)
                .Select(t => TypeModel.LoadType(t) as ITypeModel)
                .ToList();
        }

        public NamespaceModel(INamespaceModel namespaceModel)
        {
            Name = namespaceModel.Name;
            Types = namespaceModel.Types?.Select(t => TypeModel.LoadType(t) as ITypeModel).ToList();
        }
    }
}
