using ClothesForHandsMVVM.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClothesForHandsMVVM.ViewModels
{
    public class MaterialViewModel : ViewModelBase
    {
        private List<Material> _materialsList;
        private List<string> _sortTypes;
        private List<MaterialType> _filtrationTypes;
        private readonly ClothesForHandsBaseEntities repository;
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

            FiltrationTypes = new List<MaterialType>();

            #region DEBUG
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                FiltrationTypes.Insert(0, new MaterialType
                {
                    Title = "Все типы",
                });
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
                        Image = "NULL",
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
            repository = new ClothesForHandsBaseEntities();
            repository.MaterialTypes.ToList().ForEach(FiltrationTypes.Add);
            FiltrationTypes.Insert(0, new MaterialType
            {
                Title = "Все типы",
            });
            repository.Materials.ToList().ForEach(MaterialsList.Add);
            PlaceholderText = PLACEHOLDER_TEXT;
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

        public List<MaterialType> FiltrationTypes
        {
            get => _filtrationTypes; set
            {
                _filtrationTypes = value;
                OnPropertyChanged();
            }
        }

        public string CurrentSortType
        {
            get => _currentSortType; set
            {
                _currentSortType = value;
                FilterMaterials();
                OnPropertyChanged();
            }
        }

        private readonly string PLACEHOLDER_TEXT = "Введите для поиска";
        private string _placeholderText;

        public string SearchText
        {
            get => _searchText;

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    PlaceholderText = null;
                    if (!value.Equals(_searchText))
                    {
                        _searchText = value;
                        FilterMaterials(value);
                        OnPropertyChanged();
                    }
                }
                else
                {
                    PlaceholderText = PLACEHOLDER_TEXT;
                }
            }
        }

        public string PlaceholderText
        {
            get => _placeholderText; set
            {
                _placeholderText = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        private string _currentSortType;

        private void FilterMaterials(object value = null)
        {
            string searchQuery = value as string;
            List<Material> currentMaterials = repository.Materials.ToList();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                currentMaterials = currentMaterials
                    .Where(m => m.Title.ToLower().Contains(searchQuery.ToLower())
                                        || m.Description != null
                                        && m.Description.Contains(searchQuery))
                    .ToList();
            }
            if (CurrentSortType != "Сортировка")
            {
                switch (CurrentSortType)
                {
                    case "Наименование по возрастанию":
                        currentMaterials = currentMaterials.OrderBy(m => m.Title).ToList();
                        break;
                    case "Наименование по убыванию":
                        currentMaterials = currentMaterials.OrderByDescending(m => m.Title).ToList();
                        break;
                    case "Остаток на складе по возрастанию":
                        currentMaterials = currentMaterials.OrderBy(m => m.CountInStock).ToList();
                        break;
                    case "Остаток на складе по убыванию":
                        currentMaterials = currentMaterials.OrderByDescending(m => m.CountInStock).ToList();
                        break;
                    case "Стоимость по возрастанию":
                        currentMaterials = currentMaterials.OrderBy(m => m.Cost).ToList();
                        break;
                    case "Стоимость по убыванию":
                        currentMaterials = currentMaterials.OrderByDescending(m => m.Cost).ToList();
                        break;
                }
            }
            MaterialsList = currentMaterials;
        }
    }
}
