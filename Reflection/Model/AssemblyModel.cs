using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Reflection.Model
{
    [DataContract]
    public class AssemblyModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<NamespaceModel> NamespaceModels { get; set; }

        public AssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceModels = types
                .Where(t => t.IsVisible)
                .GroupBy(t => t.Namespace)
                .OrderBy(grouping => grouping.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList()))
                .ToList();
        }
    }
}
