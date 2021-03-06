using ClothesForHandsMVVM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ClothesForHandsMVVM.Converters
{
    public class SupplierConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICollection<Supplier> suppliers = value as ICollection<Supplier>;
            return suppliers.Count == 0 ? "Данных нет" : string.Join(", ",
                suppliers.Select(s => s.Title));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
