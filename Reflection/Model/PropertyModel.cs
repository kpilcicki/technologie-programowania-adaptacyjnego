using System.Runtime.Serialization;

namespace Reflection.Model
{
    [DataContract(IsReference = true)]
    public class PropertyModel
    {
        [DataMember] public string Name { get; set; }

        [DataMember] public TypeModel Type { get; set; }

        public PropertyModel(string name, TypeModel propertyType)
        {
            Name = name;
            Type = propertyType;
        }
    }
}