using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class FieldModel
    {
        public FieldModel()
        {

        }
        public int FieldModelId { get; set; }

        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public FieldModel(FieldDtg fieldModel)
        {
            Name = fieldModel.Name;
            Type = TypeModel.LoadType(fieldModel.Type);
        }
    }
}
