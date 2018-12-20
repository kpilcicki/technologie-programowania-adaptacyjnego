using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class PropertyModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }

        public PropertyModel(PropertyDtg propertyModel)
        {
            Name = propertyModel.Name;
            Type = TypeModel.LoadType(propertyModel.Type);
        }
    }
}