using ClothesForHandsMVVM.Commands;
using ClothesForHandsMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
            PlaceholderText = PLACEHOLDER_TEMPLATE;
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
            PageNumArray = new List<int>();
            InsertPageNums(repository.Materials.Count());
            CurrentPageNum = 1;
            repository.MaterialTypes.ToList().ForEach(FiltrationTypes.Add);
            FiltrationTypes.Insert(0, new MaterialType
            {
                Title = "Все типы",
            });
        }

        private RelayCommand _toggleMinCountWindowCommand;

        private void InsertPageNums(int foundItemsCount)
        {
            int pagesCount = Convert.ToInt32(Math.Round(foundItemsCount / 15d));
            List<int> currentPageNums = new List<int>();
            for (int i = 1; i < pagesCount; i++)
            {
                currentPageNums.Add(i);
            }
            PageNumArray = currentPageNums;
        }

        public List<Material> MaterialsList
        {
            get => _materialsList; set
            {
                _materialsList = value;
                OnPropertyChanged();
            }
        }

        private int _totalMaterialsCount;
        private int _shownMaterialsCount;

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

        public MaterialType CurrentFilterType
        {
            get => _currentFilterType; set
            {
                _currentFilterType = value;
                FilterMaterials();
                OnPropertyChanged();
            }
        }

        private readonly string PLACEHOLDER_TEMPLATE = "Введите для поиска";
        private string _placeholderText;

        public string SearchText
        {
            get => _searchText;

            set
            {
                if (!value.Equals(_searchText))
                {
                    _searchText = value;
                    PlaceholderText = string.IsNullOrEmpty(_searchText) ? PLACEHOLDER_TEMPLATE : null;
                    FilterMaterials();
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand _pageChangeCommand;

        public string PlaceholderText
        {
            get => _placeholderText; set
            {
                _placeholderText = value;
                OnPropertyChanged();
            }
        }

        public ICommand PagePreviousCommand
        {
            get
            {
                if (_pagePreviousCommand == null)
                {
                    _pagePreviousCommand = new RelayCommand(param => ChangePage(CurrentPageNum - 1));
                }
                return _pagePreviousCommand;
            }
        }
        public List<int> PageNumArray
        {
            get => _pageNumArray; private set
            {
                _pageNumArray = value;
                OnPropertyChanged();
            }
        }
        public ICommand PageNextCommand
        {
            get
            {
                if (_pageNextCommand == null)
                {
                    _pageNextCommand = new RelayCommand(param => ChangePage(CurrentPageNum + 1));
                }
                return _pageNextCommand;
            }
        }

        public int CurrentPageNum
        {
            get => _currentPageNum; set
            {
                _currentPageNum = value;
                if (_currentPageNum < 1)
                {
                    _currentPageNum = 1;
                }
                else if (_currentPageNum > PageNumArray.Count)
                {
                    _currentPageNum = PageNumArray.Count;
                }
                OnPropertyChanged();
            }
        }

        public ICommand PageChangeCommand
        {
            get
            {
                if (_pageChangeCommand == null)
                {
                    _pageChangeCommand = new RelayCommand(param => ChangePage(param));
                }
                return _pageChangeCommand;
            }
        }

        public int TotalMaterialCount
        {
            get => _totalMaterialsCount; private set
            {
                _totalMaterialsCount = value;
                OnPropertyChanged();
            }
        }

        public int ShownMaterialsCount
        {
            get => _shownMaterialsCount; set
            {
                _shownMaterialsCount = value;
                OnPropertyChanged();
            }
        }

        private bool _isInEditMode;

        public ICommand ToggleMinCountWindowCommand
        {
            get
            {
                if (_toggleMinCountWindowCommand == null)
                {
                    _toggleMinCountWindowCommand = new RelayCommand(param => ToggleEditMode(param));
                }
                return _toggleMinCountWindowCommand;
            }
        }

        private List<Material> _selectedMaterials;

        private void ToggleEditMode(object param)
        {
            if (param != null)
            {
                SelectedMaterials = ((System.Collections.IList)param).Cast<Material>().ToList();
            }
            IsInEditMode = !IsInEditMode;
        }

        private RelayCommand _setMinCountForSelectedItemsCommand;

        public bool IsInEditMode
        {
            get => _isInEditMode; set
            {
                if (!_isInEditMode)
                {
                    MeanMinCount = Convert.ToInt32(SelectedMaterials.Max(m => m.MinCount));
                }
                _isInEditMode = value;
                OnPropertyChanged();
            }
        }

        public ICommand SetMinCountForSelectedItemsCommand
        {
            get
            {
                if (_setMinCountForSelectedItemsCommand == null)
                {
                    _setMinCountForSelectedItemsCommand = new RelayCommand(param => UpdateMinCount(param));
                }
                return _setMinCountForSelectedItemsCommand;
            }
        }

        public int MeanMinCount
        {
            get => _meanMinCount; set
            {
                _meanMinCount = value;
                OnPropertyChanged();
            }
        }

        public List<Material> SelectedMaterials
        {
            get => _selectedMaterials; set
            {
                _selectedMaterials = value;
                OnPropertyChanged();
            }
        }

        private int _meanMinCount;

        private void UpdateMinCount(object param)
        {
            System.Collections.IList materials = (System.Collections.IList)param;
            materials.Cast<Material>().ToList().ForEach(m => m.MinCount = MeanMinCount);
            try
            {
                repository.SaveChanges();
                MessageBox.Show("Минимальное количество успешно обновлено!",
                    "Успешно!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                IsInEditMode = !IsInEditMode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Минимальное количество не обновлено! " +
                    "Попробуйте ещё раз. ",
                     "Ошибка: " + ex.Message,
                     MessageBoxButton.OK,
                     MessageBoxImage.Error);
            }
            FilterMaterials();
        }

        private void ChangePage(object value)
        {
            CurrentPageNum = (int)value;
            FilterMaterials();
        }

        private RelayCommand _pagePreviousCommand;
        private List<int> _pageNumArray;
        private RelayCommand _pageNextCommand;
        private int _currentPageNum;
        private string _searchText;
        private string _currentSortType;
        private MaterialType _currentFilterType;

        private void FilterMaterials()
        {
            List<Material> currentMaterials = repository.Materials.ToList();
            if (!string.IsNullOrEmpty(SearchText))
            {
                currentMaterials = currentMaterials
                    .Where(m => m.Title.ToLower().Contains(SearchText.ToLower())
                                        || (m.Description != null
                                        && m.Description.Contains(SearchText)))
                    .ToList();
            }
            if (CurrentSortType != null && !CurrentSortType.Equals("Сортировка"))
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
                    default:
                        break;
                }
            }
            if (CurrentFilterType != null && CurrentFilterType.ID != 0)
            {
                currentMaterials = currentMaterials.Where(m => m.MaterialType.Equals(CurrentFilterType)).ToList();
            }
            InsertPageNums(currentMaterials.Count);
            ShownMaterialsCount = currentMaterials.Count;
            currentMaterials = currentMaterials.Skip(CurrentPageNum * 15).Take(15).ToList();
            if (currentMaterials.Count == 0 && CurrentPageNum > 1)
            {
                ChangePage(CurrentPageNum - 1);
                return;
            }
            MaterialsList = currentMaterials;
            TotalMaterialCount = repository.Materials.Count();
        }
    }
}
