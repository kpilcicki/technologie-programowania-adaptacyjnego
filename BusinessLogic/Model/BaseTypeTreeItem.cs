using System.Globalization;
using System.Text;
using Reflection.Model;

namespace BusinessLogic.Model
{
    public class BaseTypeTreeItem : MetadataTreeItem
    {
        public TypeModel TypeModel { get; }

        public BaseTypeTreeItem(TypeModel typeModel)
        {
            TypeModel = typeModel;
        }

        public override string ToString()
        {
            if (TypeModel == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append($"{TypeModel.Accessibility.ToString().ToLowerInvariant()} ");
            sb.Append(TypeModel.IsSealed ? $"sealed " : string.Empty);
            sb.Append(TypeModel.IsAbstract ? $"abstract " : string.Empty);
            sb.Append(TypeModel.IsStatic ? $"static " : string.Empty);
            sb.Append($"{TypeModel.Type.ToString().ToLower(CultureInfo.InvariantCulture)} ");
            sb.Append(TypeModel.Name);
            return sb.ToString();
        }

        protected override void BuildTreeView()
        {
            if (TypeModel.BaseType != null)
            {
                Children.Add(new BaseTypeTreeItem(TypeModel.BaseType));
            }

            if (TypeModel.DeclaringType != null)
            {
                Children.Add(new TypeTreeItem((TypeModel)TypeModel.DeclaringType));
            }

            if (TypeModel.Properties != null)
            {
                foreach (PropertyModel propertyModel in TypeModel.Properties)
                {
                    Children.Add(new PropertyTreeItem(propertyModel));
                }
            }

            if (TypeModel.Attributes != null)
            {
                foreach (TypeModel typeModel in TypeModel.Attributes)
                {
                    Children.Add(new AttributeTreeItem(typeModel));
                }
            }

            if (TypeModel.Fields != null)
            {
                foreach (FieldModel fieldModel in TypeModel.Fields)
                {
                    Children.Add(new FieldTreeItem(fieldModel));
                }
            }

            if (TypeModel.GenericArguments != null)
            {
                foreach (TypeModel typeModel in TypeModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(typeModel));
                }
            }

            if (TypeModel.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in TypeModel.ImplementedInterfaces)
                {
                    Children.Add(new ImplementedInterfaceTreeItem(typeModel));
                }
            }

            if (TypeModel.NestedTypes != null)
            {
                foreach (TypeModel typeModel in TypeModel.NestedTypes)
                {
                    Children.Add(new TypeTreeItem(typeModel));
                }
            }

            if (TypeModel.Methods != null)
            {
                foreach (MethodModel methodModel in TypeModel.Methods)
                {
                    Children.Add(new MethodTreeItem(methodModel));
                }
            }

            if (TypeModel.Constructors != null)
            {
                foreach (MethodModel methodModel in TypeModel.Constructors)
                {
                    Children.Add(new MethodTreeItem(methodModel));
                }
            }
        }
    }
}
