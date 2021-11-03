using System;
using System.Globalization;
using System.Windows.Data;

namespace ClothesForHandsMVVM.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null
                ? 0
                : (decimal)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return decimal.TryParse((string)value, out _)
                ? (object)decimal.Parse(((string)value).Replace(',', '.'), CultureInfo.InvariantCulture)
                : .0;
        }
    }
}
