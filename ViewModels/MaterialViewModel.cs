using ClothesForHandsMVVM.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClothesForHandsMVVM.ViewModels
{
    public class MaterialViewModel : ViewModelBase
    {
        private List<Material> _materialsList;
        private List<string> _sortTypes;
        public MaterialViewModel()
        {
            MaterialsList = new List<Material>();
            SortTypes = new List<string>();
            SortTypes.Insert(0, "Сортировка");
            SortTypes.Insert(1, "Наименование по возрастанию");
            SortTypes.Insert(2, "Наименование по убыванию");
            SortTypes.Insert(3, "Остаток на складе по возрастанию");
            SortTypes.Insert(4, "Остаток на складе по убыванию");
            SortTypes.Insert(5, "Стоимость по возрастанию");
            SortTypes.Insert(6, "Стоимость по убыванию");
            #region DEBUG
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                _materialsList = new List<Material>
                {
                     new Material
                    {
                        Title = "Test Material 1",
                        Cost = 1000,
                        CountInPack = 5,
                        CountInStock = 200,
                        Image = @"\materials\material_8.jpeg",
                        MinCount = 20,
                        MaterialType = new MaterialType
                        {
                            Title = "Material Type 1",
                        },
                        Suppliers = new List<Supplier>
                        {
                            new Supplier
                            {
                                Title="Supplier 1",
                            },
                              new Supplier
                            {
                                Title="Supplier 2",
                            },
                                new Supplier
                            {
                                Title="Supplier 3",
                            },
                                  new Supplier
                            {
                                Title="Supplier 4",
                            },
                                    new Supplier
                            {
                                Title="Supplier 5",
                            },
                        },
                    },
                      new Material
                    {
                        Title = "Test Material 2",
                        Cost = 1000,
                        CountInPack = 5,
                        CountInStock = 200,
                        Image = @"\materials\material_8.jpeg",
                        MinCount = 20,
                        MaterialType = new MaterialType
                        {
                            Title = "Material Type 2",
                        },
                        Suppliers = new List<Supplier>
                        {
                            new Supplier
                            {
                                Title="Supplier 1",
                            },
                              new Supplier
                            {
                                Title="Supplier 2",
                            },
                                new Supplier
                            {
                                Title="Supplier 3",
                            },
                                  new Supplier
                            {
                                Title="Supplier 4",
                            },
                                    new Supplier
                            {
                                Title="Supplier 5",
                            },
                        },
                    },
                       new Material
                    {
                        Title = "Test Material 3",
                        Cost = 1000,
                        CountInPack = 5,
                        CountInStock = 200,
                        Image = null,
                        MinCount = 20,
                        MaterialType = new MaterialType
                        {
                            Title = "Material Type 3",
                        },

                    },
                };
                return;
            }
            #endregion
            new ClothesForHandsBaseEntities().Materials.ToList().ForEach(_materialsList.Add);
        }

        public List<Material> MaterialsList
        {
            get => _materialsList; set
            {
                _materialsList = value;
                OnPropertyChanged();
            }
        }

        public List<string> SortTypes
        {
            get => _sortTypes; set
            {
                _sortTypes = value;
                OnPropertyChanged();
            }
        }
    }
}
