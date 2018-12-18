using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeModel> Types { get; set; }

        public NamespaceModel(NamespaceDtg namespaceModel)
        {
            Name = namespaceModel.Name;
            Types = namespaceModel.Types?.Select(TypeModel.LoadType).ToList();
        }
    }
}
