using Reflection.Model;

namespace BusinessLogic.Model
{
    public class NamespaceTreeItem : MetadataTreeItem
    {
        public NamespaceModel NamespaceModel { get; }

        public NamespaceTreeItem(NamespaceModel namespaceModel)
        {
            NamespaceModel = namespaceModel;
        }

        public override string ToString()
        {
            return NamespaceModel.Name;
        }

        protected override void BuildTreeView()
        {
            if (NamespaceModel?.Types == null) return;
            foreach (TypeModel typeModel in NamespaceModel?.Types)
            {
                Children.Add(new TypeTreeItem(typeModel));
            }
        }
    }
}
