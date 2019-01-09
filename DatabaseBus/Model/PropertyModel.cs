using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class PropertyModel
    {
        public PropertyModel()
        {

        }
        public int PropertyModelId { get; set; }
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public PropertyModel(PropertyDtg propertyModel)
        {
            Name = propertyModel.Name;
            Type = TypeModel.LoadType(propertyModel.Type);
        }
    }
}