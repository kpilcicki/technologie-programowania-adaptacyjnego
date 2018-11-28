using System.Globalization;
using System.Text;
using Reflection.Model;

namespace BusinessLogic.Model
{
    public class TypeTreeItem : MetadataTreeItem
    {
        private readonly TypeModel _typeModel;

        public TypeTreeItem(TypeModel typeModel)
        {
            _typeModel = typeModel;
        }

        public override string ToString()
        {
            if (_typeModel == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append($"{_typeModel.Accessibility.ToString().ToLowerInvariant()} ");
            sb.Append(_typeModel.IsSealed ? $"sealed " : string.Empty);
            sb.Append(_typeModel.IsAbstract ? $"abstract " : string.Empty);
            sb.Append(_typeModel.IsStatic ? $"static " : string.Empty);
            sb.Append($"{_typeModel.Type.ToString().ToLower(CultureInfo.InvariantCulture)} ");
            sb.Append(_typeModel.Name);
            return sb.ToString();
        }

        protected override void BuildTreeView()
        {
            if (_typeModel.BaseType != null)
            {
                Children.Add(new TypeTreeItem(_typeModel.BaseType));
            }

            if (_typeModel.DeclaringType != null)
            {
                Children.Add(new TypeTreeItem(_typeModel.DeclaringType));
            }

            if (_typeModel.Properties != null)
            {
                foreach (PropertyModel propertyModel in _typeModel.Properties)
                {
                    Children.Add(new PropertyTreeItem(propertyModel));
                }
            }

            if (_typeModel.Fields != null)
            {
                foreach (FieldModel fieldModel in _typeModel.Fields)
                {
                    Children.Add(new FieldTreeItem(fieldModel));
                }
            }

            if (_typeModel.GenericArguments != null)
            {
                foreach (TypeModel typeModel in _typeModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(typeModel));
                }
            }

            if (_typeModel.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in _typeModel.ImplementedInterfaces)
                {
                    Children.Add(new TypeTreeItem(typeModel));
                }
            }

            if (_typeModel.NestedTypes != null)
            {
                foreach (TypeModel typeModel in _typeModel.NestedTypes)
                {
                    Children.Add(new TypeTreeItem(typeModel));
                }
            }

            if (_typeModel.Methods != null)
            {
                foreach (MethodModel methodModel in _typeModel.Methods)
                {
                    Children.Add(new MethodTreeItem(methodModel));
                }
            }

            if (_typeModel.Constructors != null)
            {
                foreach (MethodModel methodModel in _typeModel.Constructors)
                {
                    Children.Add(new MethodTreeItem(methodModel));
                }
            }
        }
    }
}
