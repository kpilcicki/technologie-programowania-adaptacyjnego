using System.Collections.Generic;
using System.Linq;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class NamespaceModel
    {
        public NamespaceModel()
        {
                
        }
        public int NamespaceModelId { get; set; }
        public string Name { get; set; }

        public List<TypeModel> Types { get; set; }

        public NamespaceModel(NamespaceDtg namespaceModel)
        {
            Name = namespaceModel.Name;
            Types = namespaceModel.Types?.Select(TypeModel.LoadType).ToList();
        }
    }
}
