using DataTransferGraph.Model;
using System.Reflection;

namespace Reflection.Model
{
    public class FieldModel
    {
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public FieldModel(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeModel.LoadType(fieldInfo.FieldType);
        }
        public FieldModel(FieldDtg fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeModel.LoadType(fieldInfo.Type);
        }
    }
}
