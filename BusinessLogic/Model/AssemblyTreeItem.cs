using Reflection.Model;

namespace BusinessLogic.Model
{
    public class AssemblyTreeItem : MetadataTreeItem
    {
        private readonly AssemblyModel _assemblyModel;

        public AssemblyTreeItem(AssemblyModel assembly)
        {
            _assemblyModel = assembly;
        }

        public override string ToString()
        {
            return _assemblyModel.Name;
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
