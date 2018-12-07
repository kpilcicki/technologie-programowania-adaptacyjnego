using Reflection.PersistenceModel;
using System.Reflection;

namespace Reflection.Model
{
    public class FieldModel : IFieldModel
    {
        public string Name { get; set; }

        public ITypeModel Type { get; set; }

        public FieldModel(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeModel.LoadType(fieldInfo.FieldType);
        }
        public FieldModel(IFieldModel fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeModel.LoadType(fieldInfo.Type);
        }
    }
}
