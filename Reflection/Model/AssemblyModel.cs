using Reflection.PersistenceModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection.Model
{
    public class AssemblyModel : IAssemblyModel
    {
        public string Name { get; set; }

        public List<INamespaceModel> NamespaceModels { get; set; }

        public AssemblyModel(Assembly assembly)
        {
            DictionaryTypeSingleton.Instance.Clear();

            Name = assembly.ManifestModule.Name;
            NamespaceModels = assembly
                .GetTypes()?
                .Where(t => t.IsVisible)
                .GroupBy(t => t.Namespace)
                .OrderBy(grouping => grouping.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList()) as INamespaceModel)
                .ToList();
        }

        public AssemblyModel(IAssemblyModel assembly)
        {
            Name = assembly.Name;
            NamespaceModels = assembly.NamespaceModels?.Select(ns => new NamespaceModel(ns) as INamespaceModel).ToList();
        }
    }
}
