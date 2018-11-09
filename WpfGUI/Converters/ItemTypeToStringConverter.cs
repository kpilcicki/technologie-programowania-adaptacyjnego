using System;
using System.Globalization;
using System.Windows.Data;
using BusinessLogic.Constants;

namespace WpfGUI.Converters
{
    [ValueConversion(typeof(object), typeof(string))]
    public class ItemTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => TypeToStringMap.GetStringFromType(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
