using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace FileSerializer.Model
{
    [DataContract]
    public class AssemblyModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<NamespaceModel> NamespaceModels { get; set; }

        public AssemblyModel(AssemblyDtg assemblyModel)
        {
            Name = assemblyModel.Name;
            NamespaceModels = assemblyModel.NamespaceModels?.Select(ns => new NamespaceModel(ns)).ToList();
        }
    }
}
