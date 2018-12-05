using Reflection.Model;

namespace BusinessLogic.Model
{
    public class PropertyTreeItem : MetadataTreeItem
    {
        public PropertyModel PropertyModel { get; }

        public PropertyTreeItem(PropertyModel type)
        {
            PropertyModel = type;
        }

        public override string ToString()
        {
            return PropertyModel.Name;
        }

        protected override void BuildTreeView()
        {
            if (PropertyModel.Type != null)
            {
                Children.Add(new TypeTreeItem(PropertyModel.Type));
            }
        }
    }
}
