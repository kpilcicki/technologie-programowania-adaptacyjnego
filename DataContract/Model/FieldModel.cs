using System.Runtime.Serialization;

namespace DataContract.Model
{
    [DataContract(IsReference = true)]
    public class FieldModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }

        public FieldModel(string name, TypeModel propertyType)
        {
            Name = name;
            Type = propertyType;
        }
    }
}
