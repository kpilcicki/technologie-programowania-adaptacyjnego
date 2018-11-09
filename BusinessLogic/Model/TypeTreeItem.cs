using DataContract.Enums;
using Reflection.Model;

namespace BusinessLogic.Model
{
    public class TypeTreeItem : MetadataTreeItem
    {
        private readonly TypeModel _typeModel;

        public TypeTreeItem(TypeModel typeModel)
            : base(GetModifiers(typeModel) + typeModel.Name)
        {
            _typeModel = typeModel;
        }

        public static string GetModifiers(TypeModel model)
        {
            if (model.Modifiers != null)
            {
                string type = null;
                type += model.Modifiers.Item1.ToString().ToLower() + " ";
                type += model.Modifiers.Item2 == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : string.Empty;
                type += model.Modifiers.Item3 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : string.Empty;
                type += model.Modifiers.Item4 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : string.Empty;
                return type;
            }

            return null;
        }

        protected override void BuildTreeView()
        {
            if (_typeModel.BaseType != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[_typeModel.BaseType.Name]));
            }

            if (_typeModel.DeclaringType != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[_typeModel.DeclaringType.Name]));
            }

            if (_typeModel.Properties != null)
            {
                foreach (PropertyModel propertyModel in _typeModel.Properties)
                {
                    Children.Add(new PropertyTreeItem(propertyModel, GetModifiers(propertyModel.Type) + propertyModel.Type.Name + " " + propertyModel.Name));
                }
            }

            if (_typeModel.Fields != null)
            {
                foreach (ParameterModel parameterModel in _typeModel.Fields)
                {
                    Children.Add(new ParameterTreeItem(parameterModel));
                }
            }

            if (_typeModel.GenericArguments != null)
            {
                foreach (TypeModel typeModel in _typeModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
                }
            }

            if (_typeModel.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in _typeModel.ImplementedInterfaces)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
                }
            }

            if (_typeModel.NestedTypes != null)
            {
                foreach (TypeModel typeModel in _typeModel.NestedTypes)
                {
                    Children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name]));
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
