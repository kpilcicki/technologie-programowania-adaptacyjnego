using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class FieldModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }

        public FieldModel(FieldDtg fieldModel)
        {
            Name = fieldModel.Name;
            Type = TypeModel.LoadType(fieldModel.Type);
        }
    }
}
