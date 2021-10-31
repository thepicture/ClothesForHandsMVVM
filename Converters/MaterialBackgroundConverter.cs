using ClothesForHandsMVVM.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ClothesForHandsMVVM.Converters
{
    class MaterialBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Material material = value as Material;
            double minCount = material.MinCount;
            double? countInStock = material.CountInStock;
            if (material.CountInStock < minCount)
            {
                return "#f19292";
            }
            else if (countInStock > 3 * minCount)
            {
                return "#ffba01";
            }
            else
            {
                return "#ffffff";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
