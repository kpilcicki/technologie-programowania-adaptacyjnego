using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContract.Model
{
    [DataContract]
    public class AssemblyModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<NamespaceModel> NamespaceModels { get; set; }

        public AssemblyModel(string name)
        {
            Name = name;
        }
    }
}
