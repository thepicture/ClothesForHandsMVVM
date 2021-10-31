using System;
using System.Globalization;
using System.Windows.Data;

namespace ClothesForHandsMVVM.Converters
{
    public class CurrentPageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 0
                ? 0
                : (int)value - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 1;
        }
    }
}
