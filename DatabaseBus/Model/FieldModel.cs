using System.Collections.Generic;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class FieldModel
    {
        public FieldModel()
        {
            TypeFields = new HashSet<TypeModel>();
        }
        public int FieldModelId { get; set; }

        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public FieldModel(FieldDtg fieldModel)
        {
            Name = fieldModel.Name;
            Type = TypeModel.LoadType(fieldModel.Type);
        }

        public ICollection<TypeModel> TypeFields { get; set; }
    }
}
