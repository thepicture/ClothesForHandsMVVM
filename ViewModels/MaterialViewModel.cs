using ClothesForHandsMVVM.Commands;
using ClothesForHandsMVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ClothesForHandsMVVM.ViewModels
{
    public class MaterialViewModel : ViewModelBase
    {
        private List<Material> _materialsList;
        private List<string> _sortTypes;
        private List<MaterialType> _filtrationTypes;
        private ClothesForHandsBaseEntities repository;
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

        public ICommand FilterMaterialsCommand
        {
            get
            {
                if (_filterMaterialsCommand == null)
                {
                    _filterMaterialsCommand = new RelayCommand(value => FilterMaterials(value));
                }
                return _filterMaterialsCommand;
            }
        }

        private string _placeholderText = "Введите для поиска";

        public string SearchText
        {
            get => _searchText;

            set
            {
                PlaceholderText = string.IsNullOrEmpty(value) ? "Введите для поиска" : null;
                if (value != _searchText)
                {
                    _searchText = value;
                    FilterMaterialsCommand.Execute(value);
                    OnPropertyChanged();
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

        private void FilterMaterials(object value)
        {
            string searchQuery = value as string;
            List<Material> currentMaterials = repository.Materials.ToList();
            MaterialsList = currentMaterials
                .Where(m => m.Title.ToLower().Contains(searchQuery.ToLower())
                                    || m.Description != null
                                    && m.Description.Contains(searchQuery))
                .ToList();
        }

        private ICommand _filterMaterialsCommand;
    }
}
