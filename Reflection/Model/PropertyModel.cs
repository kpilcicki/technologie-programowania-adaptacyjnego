using Reflection.PersistenceModel;
using System.Reflection;

namespace Reflection.Model
{
    public class PropertyModel : IPropertyModel
    {
        public string Name { get; set; }

        public ITypeModel Type { get; set; }

        public PropertyModel(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            Type = TypeModel.LoadType(propertyInfo.PropertyType);
        }

        public PropertyModel(IPropertyModel propertyInfo)
        {
            Name = propertyInfo.Name;
            Type = TypeModel.LoadType(propertyInfo.Type);
        }
    }
}