using System.Text;
using Reflection.Model;

namespace BusinessLogic.Model
{
    public class MethodTreeItem : MetadataTreeItem
    {
        public MethodModel MethodModel { get; set; }

        public MethodTreeItem(MethodModel methodModel)
        {
            MethodModel = methodModel;
        }

        public override string ToString()
        {
            if (MethodModel == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append($"{MethodModel.Accessibility.ToString().ToLowerInvariant()} ");
            sb.Append(MethodModel.IsAbstract ? $"abstract " : string.Empty);
            sb.Append(MethodModel.IsStatic ? $"static" : string.Empty);
            sb.Append(MethodModel.IsAbstract ? $"virtual " : string.Empty);
            sb.Append(MethodModel.Name);
            return sb.ToString();
        }

        protected override void BuildTreeView()
        {
            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(genericArgument));
                }
            }

            if (MethodModel.Parameters != null)
            {
                foreach (ParameterModel parameter in MethodModel.Parameters)
                {
                    Children.Add(new ParameterTreeItem(parameter));
                }
            }

            if (MethodModel.ReturnType != null)
            {
                Children.Add(new TypeTreeItem(MethodModel.ReturnType));
            }
        }
    }
}
