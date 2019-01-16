using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataTransferGraph.Model;

namespace Reflection.Model
{
    public class AssemblyModel
    {
        public string Name { get; set; }

        public List<NamespaceModel> NamespaceModels { get; set; }

        public static string CurrentAssemblyName;

        public AssemblyModel(Assembly assembly)
        {
            DictionaryTypeSingleton.Instance.Clear();
            CurrentAssemblyName = assembly.ManifestModule.FullyQualifiedName;
            Name = assembly.ManifestModule.Name;
            NamespaceModels = assembly
                .GetTypes()?
                .GroupBy(t => t.Namespace)
                .OrderBy(grouping => grouping.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList()))
                .ToList();
        }

        public AssemblyModel(AssemblyDtg assembly)
        {
            Name = assembly.Name;
            NamespaceModels = assembly.NamespaceModels?.Select(ns => new NamespaceModel(ns)).ToList();
        }
    }
}
