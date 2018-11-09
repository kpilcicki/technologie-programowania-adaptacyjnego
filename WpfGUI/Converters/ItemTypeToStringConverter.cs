using System;
using System.Globalization;
using System.Windows.Data;
using BusinessLogic.Constants;
using BusinessLogic.Model;

namespace WpfGUI.Converters
{
    [ValueConversion(typeof(ItemTypeEnum), typeof(string))]
    public class ItemTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => TypeToStringMap.GetStringFromType(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
