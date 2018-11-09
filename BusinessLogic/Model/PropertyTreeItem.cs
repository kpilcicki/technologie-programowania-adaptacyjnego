using Reflection.Model;

namespace BusinessLogic.Model
{
    public class PropertyTreeItem : MetadataTreeItem
    {
        public PropertyModel PropertyModel { get; set; }

        public PropertyTreeItem(PropertyModel type, string name)
            : base(name)
        {
            PropertyModel = type;
        }

        protected override void BuildTreeView()
        {
            if (PropertyModel.Type != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[PropertyModel.Type.Name]));
            }
        }
    }
}
