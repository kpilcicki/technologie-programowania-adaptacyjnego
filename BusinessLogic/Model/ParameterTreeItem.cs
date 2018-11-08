using Reflection.Model;

namespace BusinessLogic.Model
{
    public class ParameterTreeItem : TreeViewItem
    {
        public ParameterModel ParameterModel { get; set; }

        public ParameterTreeItem(ParameterModel parameterModel)
            : base(parameterModel.Name)
        {
            ParameterModel = parameterModel;
        }

        protected override void BuildTreeView()
        {
            if (ParameterModel.Type != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[ParameterModel.Type.Name]));
            }
        }
    }
}
