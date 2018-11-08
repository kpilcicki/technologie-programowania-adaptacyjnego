using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using BusinessLogic.Model;

namespace WpfGUI.Converters
{
    [ValueConversion(typeof(ItemTypeEnum), typeof(string))]
    public class ItemTypeToStringConverter : IValueConverter
    {
        private Dictionary<Type, string> _mapTypeToString = new Dictionary<Type, string>()
        {
            { typeof(TypeTreeItem), "Type" },
            { typeof(PropertyTreeItem), "Property" },
            { typeof(ParameterTreeItem), "Parameter" },
            { typeof(NamespaceTreeItem), "Namespace" },
            { typeof(MethodTreeItem), "Method" },
            { typeof(AssemblyTreeItem), "Type" },
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && _mapTypeToString.TryGetValue(value.GetType(), out string converted))
            {
                return $"<<{converted}>>";
            }

            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
