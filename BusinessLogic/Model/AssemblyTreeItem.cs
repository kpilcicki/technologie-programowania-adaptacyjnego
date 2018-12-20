using Reflection.Model;

namespace BusinessLogic.Model
{
    public class AssemblyTreeItem : MetadataTreeItem
    {
        public AssemblyModel AssemblyModel { get; }

        public AssemblyTreeItem(AssemblyModel assembly)
        {
            AssemblyModel = assembly;
        }

        public override string ToString()
        {
            return AssemblyModel.Name;
        }

        protected override void BuildTreeView()
        {
            if (AssemblyModel?.NamespaceModels == null) return;

            foreach (NamespaceModel namespaceModel in AssemblyModel.NamespaceModels)
            {
                Children.Add(new NamespaceTreeItem(namespaceModel));
            }
        }
    }
}
