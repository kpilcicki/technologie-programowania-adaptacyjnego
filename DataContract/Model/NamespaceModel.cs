using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContract.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeModel> Types { get; set; }
    }
}
