using Reflection.Model;

namespace BusinessLogic.Model
{
    public class FieldTreeItem : MetadataTreeItem
    {
        public FieldModel FieldModel { get; }

        public FieldTreeItem(FieldModel type)
        {
            FieldModel = type;
        }

        public override string ToString()
        {
            return FieldModel.Name;
        }

        protected override void BuildTreeView()
        {
            if (FieldModel.Type != null)
            {
                Children.Add(new TypeTreeItem(FieldModel.Type));
            }
        }
    }
}
