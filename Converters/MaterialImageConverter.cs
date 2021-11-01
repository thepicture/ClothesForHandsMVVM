using System;
using System.Globalization;
using System.Windows.Data;

namespace ClothesForHandsMVVM.Converters
{
    public class MaterialImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath = (string)value;
            return (imagePath == null || imagePath.Equals("NULL"))
            ? new Uri("pack://application:,,,/Resources/picture.png")
            : new Uri("pack://application:,,,/Resources" + imagePath);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}