using System.Runtime.Serialization;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class PropertyModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }
    }
}