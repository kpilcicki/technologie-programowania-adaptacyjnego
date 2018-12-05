using System.Reflection;

namespace Reflection.Model
{
    public class FieldModel
    {
        public string Name { get; }

        public TypeModel Type { get; }

        public FieldModel(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeModel.LoadType(fieldInfo.FieldType);
        }
    }
}
