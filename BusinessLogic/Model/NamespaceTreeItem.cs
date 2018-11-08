using Reflection.Model;

namespace BusinessLogic.Model
{
    public class NamespaceTreeItem : TreeViewItem
    {
        private readonly NamespaceModel _namespaceModel;

        public NamespaceTreeItem(NamespaceModel namespaceModel)
            : base(namespaceModel.Name)
        {
            _namespaceModel = namespaceModel;
        }

        protected override void BuildTreeView()
        {
            if (_namespaceModel?.Types != null)
            {
                foreach (TypeModel typeModel in _namespaceModel?.Types)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
                }
            }
        }
    }
}
