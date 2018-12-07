using Reflection.PersistenceModel;
using System.Runtime.Serialization;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class PropertyModel : IPropertyModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ITypeModel Type { get; set; }

        public PropertyModel(IPropertyModel propertyModel)
        {
            Name = propertyModel.Name;
            Type = TypeModel.LoadType(propertyModel.Type) as ITypeModel;
        }
    }
}