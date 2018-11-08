using System.ComponentModel;

namespace CommandLineGUI.Base
{
    internal class Binding
    {
        public object Target { get; }

        public PropertyDescriptor SourceProperty { get; }

        public PropertyDescriptor TargetProperty { get; }

        public string DisplayChangeInfo { get; }

        public Binding(object target, PropertyDescriptor targetProperty, PropertyDescriptor sourceProperty, string displayChangeInfo = "")
        {
            Target = target;
            SourceProperty = sourceProperty;
            TargetProperty = targetProperty;
            DisplayChangeInfo = displayChangeInfo;
        }
    }
}
