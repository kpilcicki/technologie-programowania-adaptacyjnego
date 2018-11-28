using DataContract.Model;

namespace BusinessLogic.Model
{
    public class NamespaceTreeItem : MetadataTreeItem
    {
        private readonly NamespaceModel _namespaceModel;

        public NamespaceTreeItem(NamespaceModel namespaceModel)
        {
            _namespaceModel = namespaceModel;
        }

        public override string ToString()
        {
            return _namespaceModel.Name;
        }

        protected override void BuildTreeView()
        {
            if (_namespaceModel?.Types != null)
            {
                foreach (TypeModel typeModel in _namespaceModel?.Types)
                {
                    Children.Add(new TypeTreeItem(typeModel));
                }
            }
        }
    }
}
