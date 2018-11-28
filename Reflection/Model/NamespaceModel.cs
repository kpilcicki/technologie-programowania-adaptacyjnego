using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Reflection.Loaders;

namespace Reflection.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeModel> Types { get; set; }

        public NamespaceModel(string name, List<Type> types)
        {
            Name = name;
            Types = types
                .OrderBy(t => t.Name)
                .Select(TypeLoaderHelpers.LoadTypeModel)
                .ToList();
        }

    }
}
