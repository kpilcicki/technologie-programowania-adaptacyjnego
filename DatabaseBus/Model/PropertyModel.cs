using System.Collections.Generic;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class PropertyModel
    {
        public PropertyModel()
        {
            TypeProperties = new HashSet<TypeModel>();
        }
        public int PropertyModelId { get; set; }
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public PropertyModel(PropertyDtg propertyModel)
        {
            Name = propertyModel.Name;
            Type = TypeModel.LoadType(propertyModel.Type);
        }

        public ICollection<TypeModel> TypeProperties { get; set; }

    }
}