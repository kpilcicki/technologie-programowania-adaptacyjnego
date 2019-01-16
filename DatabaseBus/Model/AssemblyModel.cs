using System.Collections.Generic;
using System.Linq;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class AssemblyModel
    {
        public AssemblyModel()
        {

        }
        public int AssemblyModelId { get; set; }
        public string Name { get; set; }

        public List<NamespaceModel> NamespaceModels { get; set; }

        public AssemblyModel(AssemblyDtg assemblyModel)
        {
            Name = assemblyModel.Name;
            NamespaceModels = assemblyModel.NamespaceModels?.Select(ns => new NamespaceModel(ns)).ToList();
        }
    }
}
