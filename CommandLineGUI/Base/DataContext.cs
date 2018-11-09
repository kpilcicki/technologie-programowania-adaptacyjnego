using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CommandLineGUI.Base
{
    internal class DataContext
    {
        private readonly INotifyPropertyChanged _source;

        public INotifyPropertyChanged Source => _source;

        public DataContext(INotifyPropertyChanged source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            source.PropertyChanged += SourcePropertyChanged;
        }

        private readonly List<Binding> _bindings = new List<Binding>();

        public void SetBinding(object target, string targetProperty, string sourceProperty, string changeDisplayMessage = "")
        {
            PropertyDescriptor sourcePropertyDescriptor = TypeDescriptor.GetProperties(_source).Find(sourceProperty, false);
            if (sourcePropertyDescriptor == null)
            {
                Console.WriteLine($"Binding failed: {_source} does not contain property {sourceProperty}");
                return;
            }

            PropertyDescriptor targetPropertyDescriptor = TypeDescriptor.GetProperties(target).Find(targetProperty, false);
            if (targetPropertyDescriptor == null)
            {
                Console.WriteLine($"Binding failed: {target} does not contain property {targetProperty}");
                return;
            }

            _bindings.Add(new Binding(target, targetPropertyDescriptor, sourcePropertyDescriptor, changeDisplayMessage));
            object value = sourcePropertyDescriptor.GetValue(_source);
            targetPropertyDescriptor.SetValue(target, value);
        }

        protected virtual void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<Binding> toRun = _bindings.Where(bind => bind.SourceProperty.Name == e.PropertyName).ToList();
            foreach (Binding binding in toRun)
            {
                binding.TargetProperty.SetValue(binding.Target, binding.SourceProperty.GetValue(_source));
                if (!string.IsNullOrEmpty(binding.DisplayChangeInfo))
                {
                    Console.WriteLine(binding.DisplayChangeInfo);
                }
            }
        }
    }
}