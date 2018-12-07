using Reflection.PersistenceModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceModel :  INamespaceModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<ITypeModel> Types { get; set; }

        public NamespaceModel(INamespaceModel namespaceModel)
        {
            Name = namespaceModel.Name;
            Types = namespaceModel.Types?.Select(t => TypeModel.LoadType(t) as ITypeModel).ToList();
        }
    }
}
