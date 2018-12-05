using System.Reflection;

namespace Reflection.Model
{
    public class PropertyModel
    {
        public string Name { get; }

        public TypeModel Type { get; }

        public PropertyModel(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            Type = TypeModel.LoadType(propertyInfo.PropertyType);
        }
    }
}