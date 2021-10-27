using System.ComponentModel;
using System.Text;

namespace ClothesForHandsMVVM.Models
{
    public partial class Material : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                StringBuilder errors = new StringBuilder();
                switch (columnName)
                {
                    case "Cost":
                        if (Cost <= 0)
                        {
                            _ = errors.AppendLine("Стоимость должна быть положительной");
                        }
                        break;
                    case "CountInPack":
                        if (CountInPack < 0)
                        {
                            _ = errors.AppendLine("Количество в упаковке - это неотрицательное целое число");
                        }
                        break;
                    case "CountInStock":
                        if (CountInStock < 0)
                        {
                            _ = errors.AppendLine("Количество на складе - это неотрицательное целое число");
                        }
                        break;
                    case "Unit":
                        if (Unit is null)
                        {
                            _ = errors.AppendLine("Укажите единицу измерения материала");
                        }
                        break;
                    case "MaterialType":
                        if (MaterialType is null)
                        {
                            _ = errors.AppendLine("Укажите тип материала");
                        }
                        break;
                    case "Title":
                        if (string.IsNullOrWhiteSpace(Title))
                        {
                            _ = errors.AppendLine("Укажите наименование материала");
                        }
                        break;
                    default:
                        break;
                }
                return errors.ToString();
            }
        }

        public string Error => string.Empty;
    }
}
