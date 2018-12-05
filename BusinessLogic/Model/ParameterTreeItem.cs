using Reflection.Model;

namespace BusinessLogic.Model
{
    public class ParameterTreeItem : MetadataTreeItem
    {
        public ParameterModel ParameterModel { get; }

        public ParameterTreeItem(ParameterModel parameterModel)
        {
            ParameterModel = parameterModel;
        }

        public override string ToString()
        {
            return ParameterModel.Name;
        }

        protected override void BuildTreeView()
        {
            if (ParameterModel.Type != null)
            {
                Children.Add(new TypeTreeItem(ParameterModel.Type));
            }
        }
    }
}
