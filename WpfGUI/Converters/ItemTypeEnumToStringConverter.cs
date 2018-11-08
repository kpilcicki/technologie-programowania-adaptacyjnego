using System;
using System.Globalization;
using System.Windows.Data;
using BusinessLogic.Model;

namespace WpfGUI.Converters
{
    [ValueConversion(typeof(ItemTypeEnum), typeof(string))]
    public class ItemTypeEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) return (ItemTypeEnum)value;
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
