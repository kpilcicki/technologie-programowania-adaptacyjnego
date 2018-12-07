using Reflection.PersistenceModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace FileSerializer.Model
{
    [DataContract]
    public class AssemblyModel : IAssemblyModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<INamespaceModel> NamespaceModels { get; set; }

        public AssemblyModel(IAssemblyModel assemblyModel)
        {
            Name = assemblyModel.Name;
            NamespaceModels = assemblyModel.NamespaceModels?.Select(ns => new NamespaceModel(ns) as INamespaceModel).ToList();
        }
    }
}
