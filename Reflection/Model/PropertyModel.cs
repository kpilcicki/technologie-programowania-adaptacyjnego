using System.Reflection;
using DataTransferGraph.Model;

namespace Reflection.Model
{
    public class PropertyModel
    {
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public PropertyModel(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            Type = TypeModel.LoadType(propertyInfo.PropertyType);
        }

        public PropertyModel(PropertyDtg propertyInfo)
        {
            Name = propertyInfo.Name;
            Type = TypeModel.LoadType(propertyInfo.Type);
        }
    }
}