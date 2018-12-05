using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileSerializer.Model
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
