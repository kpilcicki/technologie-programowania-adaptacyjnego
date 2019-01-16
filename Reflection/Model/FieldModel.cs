using DataTransferGraph.Model;
using System.Reflection;
using DataTransferGraph.Enums;

namespace Reflection.Model
{
    public class FieldModel
    {
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public AccessLevel Accessibility { get; set; }

        public bool IsStatic { get; set; }

        public FieldModel(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeModel.LoadType(fieldInfo.FieldType);
            Accessibility = GetAccessibility(fieldInfo);
            IsStatic = fieldInfo.IsStatic;

        }
        public FieldModel(FieldDtg fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeModel.LoadType(fieldInfo.Type);
        }

        private static AccessLevel GetAccessibility(FieldInfo info)
        {
            return info.IsPublic ? AccessLevel.Public :
                info.IsFamily ? AccessLevel.Protected : AccessLevel.Private;
        }
    }
}
