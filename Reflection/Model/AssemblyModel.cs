using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection.Model
{
    public class AssemblyModel
    {
        public string Name { get; }

        public List<NamespaceModel> NamespaceModels { get; }

        public AssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            NamespaceModels = assembly
                .GetTypes()
                .Where(t => t.IsVisible)
                .GroupBy(t => t.Namespace)
                .OrderBy(grouping => grouping.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList()))
                .ToList();
        }
    }
}
