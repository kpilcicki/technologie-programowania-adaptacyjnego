using DataContract.Enums;
using Reflection.Model;

namespace BusinessLogic.Model
{
    public class MethodTreeItem : TreeViewItem
    {
        public MethodModel MethodModel { get; set; }

        public MethodTreeItem(MethodModel methodModel)
            : base(GetModifiers(methodModel) + methodModel.Name)
        {
            MethodModel = methodModel;
        }

        public static string GetModifiers(MethodModel model)
        {
            string type = null;
            type += model.Modifiers.Item1.ToString().ToLower() + " ";
            type += model.Modifiers.Item2 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : string.Empty;
            type += model.Modifiers.Item3 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : string.Empty;
            type += model.Modifiers.Item4 == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : string.Empty;
            return type;
        }

        protected override void BuildTreeView()
        {
            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[genericArgument.Name]));
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
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[MethodModel.ReturnType.Name]));
            }
        }
    }
}
