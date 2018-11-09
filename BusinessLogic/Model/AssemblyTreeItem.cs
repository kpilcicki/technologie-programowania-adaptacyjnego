using Reflection.Model;

namespace BusinessLogic.Model
{
    public class AssemblyTreeItem : TreeViewItem
    {
        private readonly AssemblyModel _assemblyModel;

        public AssemblyTreeItem(AssemblyModel assembly)
            : base(assembly.Name)
        {
            _assemblyModel = assembly;
        }

        protected override void BuildTreeView()
        {
            if (_assemblyModel?.NamespaceModels != null)
            {
                foreach (NamespaceModel namespaceModel in _assemblyModel.NamespaceModels)
                {
                    Children.Add(new NamespaceTreeItem(namespaceModel));
                }
            }
        }
    }
}
