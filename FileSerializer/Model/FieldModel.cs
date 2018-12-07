using Reflection.PersistenceModel;
using System.Runtime.Serialization;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class FieldModel : IFieldModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ITypeModel Type { get; set; }

        public FieldModel(IFieldModel fieldModel)
        {
            Name = fieldModel.Name;
            Type = TypeModel.LoadType(fieldModel.Type);
        }
    }
}
