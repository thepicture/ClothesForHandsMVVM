using System;
using System.Globalization;
using System.Windows.Data;

namespace ClothesForHandsMVVM.Converters
{
    public class IntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null
                ? 0
                : System.Convert.ToInt32(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return decimal.TryParse((string)value, out _)
                ? (object)int.Parse(((string)value), CultureInfo.InvariantCulture)
                : 0;
        }
    }
}
