using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace ClothesForHandsMVVM.Converters
{
    public class MaterialImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath = (string)value;
            if (imagePath == null || imagePath.Equals("NULL"))
            {
                return new Uri("pack://application:,,,/Resources/picture.png");
            }
            else
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../Resources{imagePath}".Replace("/", @"\"));
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}