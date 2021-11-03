using ClothesForHandsMVVM.Commands;
using ClothesForHandsMVVM.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ClothesForHandsMVVM.ViewModels
{
    public class AddEditMaterialViewModel : ViewModelBase
    {
        private Material _material;
        private RelayCommand _saveChangesCommand;
        private List<string> _unit;
        private RelayCommand _putPictureCommand;
        private List<Supplier> _suppliers;
        private string _errors;
        private RelayCommand _deleteSupplierCommand;
        private RelayCommand _addSupplierCommand;
        private string _searchText;
        private ObservableCollection<Supplier> _suppliersOfMaterial;
        private MaterialType _currentType;
        private List<MaterialType> _materialTypes;
        private string _currentUnit;
        public AddEditMaterialViewModel() { }

        public AddEditMaterialViewModel(Material material)
        {
            Material = new Material();
            if (material != null)
            {
                Material = material;
            }
            Material.Suppliers.ToList().ForEach(SuppliersOfMaterial.Add);
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                using (ClothesForHandsBaseEntities repository = new ClothesForHandsBaseEntities())
                {
                    MaterialTypes = new List<MaterialType>(repository.MaterialTypes.ToList());
                    CurrentType = MaterialTypes.First(t => t.ID == Material.MaterialType.ID);
                    Unit = new List<string>(repository.Materials.Select(m => m.Unit).Distinct());
                    CurrentUnit = Unit.First(u => u.Equals(Material.Unit));
                }
            }
            Material.PropertyChanged += Material_PropertyChanged;
            CheckForErrors();
        }

        private void Material_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckForErrors();
        }

        private void CheckForErrors()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Material.Cost.ToString())
                || !decimal.TryParse(Material.Cost.ToString(), out _)
                || Material.Cost <= 0)
            {
                _ = errors.AppendLine("Стоимость должна быть положительной");
            }
            if (string.IsNullOrWhiteSpace(Material.CountInPack.ToString())
                || !int.TryParse(Material.CountInPack.ToString(), out _)
                || Material.CountInPack < 0)
            {
                _ = errors.AppendLine("Количество в упаковке - это неотрицательное целое число");
            }
            if (string.IsNullOrWhiteSpace(Material.MinCount.ToString())
                || !int.TryParse(Material.MinCount.ToString(), out _)
                || Material.MinCount < 0)
            {
                _ = errors.AppendLine("Минимальное количество - это неотрицательное целое число");
            }
            if (string.IsNullOrWhiteSpace(Material.CountInStock.ToString())
                || !int.TryParse(Material.CountInStock.ToString(), out _)
                || Material.CountInStock < 0)
            {
                _ = errors.AppendLine("Количество на складе - это неотрицательное целое число");
            }
            if (CurrentUnit is null)
            {
                _ = errors.AppendLine("Укажите единицу измерения материала");
            }
            if (CurrentType is null)
            {
                _ = errors.AppendLine("Укажите тип материала");
            }
            if (string.IsNullOrWhiteSpace(Material.Title))
            {
                _ = errors.AppendLine("Укажите наименование материала до 100 символов");
            }
            Errors = errors.ToString();
        }

        public Material Material
        {
            get => _material; set
            {
                _material = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                {
                    _saveChangesCommand = new RelayCommand(param => SaveMaterial(param));
                }
                return _saveChangesCommand;
            }
        }

        public List<string> Unit
        {
            get => _unit;

            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }

        public ICommand PutPictureCommand
        {
            get
            {
                if (_putPictureCommand == null)
                {
                    _putPictureCommand = new RelayCommand(param => UpdatePicturePreview());
                }
                return _putPictureCommand;
            }
        }

        public string Errors
        {
            get => _errors; set
            {
                _errors = value;
                OnPropertyChanged();
            }
        }

        public List<Supplier> Suppliers
        {
            get
            {
                if (_suppliers == null)
                {
                    using (ClothesForHandsBaseEntities repository = new ClothesForHandsBaseEntities())
                    {
                        _suppliers = repository.Suppliers.ToList();
                    }
                }
                return _suppliers;
            }

            set
            {
                _suppliers = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteSupplierCommand
        {
            get
            {
                if (_deleteSupplierCommand == null)
                {
                    _deleteSupplierCommand = new RelayCommand(param => RemoveSupplier(param));
                }
                return _deleteSupplierCommand;
            }
        }

        private void RemoveSupplier(object param)
        {
            SuppliersOfMaterial.Remove(param as Supplier);
            FindByName();
        }

        private void FindByName()
        {
            using (ClothesForHandsBaseEntities repository = new ClothesForHandsBaseEntities())
            {
                List<Supplier> currentSuppliers = new List<Supplier>(repository.Suppliers);
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    currentSuppliers = currentSuppliers.Where(s => s.Title.ToLower()
                    .Contains(SearchText.ToLower())).ToList();
                }
                Suppliers = currentSuppliers.Where(s => !SuppliersOfMaterial
                .Select(e => e.ID).Contains(s.ID)).ToList();
            }
        }

        public ICommand AddSupplierCommand
        {
            get
            {
                if (_addSupplierCommand == null)
                {
                    _addSupplierCommand = new RelayCommand(param => AddSupplier(param));
                }
                return _addSupplierCommand;
            }
        }

        private void AddSupplier(object param)
        {
            SuppliersOfMaterial.Add(item: param as Supplier);
            FindByName();
        }

        public string SearchText
        {
            get => _searchText; set
            {
                _searchText = value;
                OnPropertyChanged();
                FindByName();
            }
        }

        public ObservableCollection<Supplier> SuppliersOfMaterial
        {
            get
            {
                if (_suppliersOfMaterial == null)
                {
                    _suppliersOfMaterial = new ObservableCollection<Supplier>();
                }
                return _suppliersOfMaterial;
            }
            set => _suppliersOfMaterial = value;
        }

        public MaterialType CurrentType
        {
            get => _currentType; set
            {
                _currentType = value;
                OnPropertyChanged();
            }
        }

        public List<MaterialType> MaterialTypes
        {
            get => _materialTypes; set
            {
                _materialTypes = value;
                OnPropertyChanged();
            }
        }

        public string CurrentUnit
        {
            get => _currentUnit; set
            {
                _currentUnit = value;
                OnPropertyChanged();
            }
        }

        private void UpdatePicturePreview()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    File.Copy(openFileDialog.FileName, "../../Resources/materials/"
                        + openFileDialog.SafeFileName);
                    Material.Image = "/materials/" + openFileDialog.SafeFileName;
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Не удалось изменить изображение. Это могло произойти, " +
                        "если у вас нет прав на изменение файлов в Вашей файловой системе," +
                        "изображение с таким названием уже есть в базе данных " +
                        "или изображение неверного формата. Поддерживаются только форматы " +
                        "изображений, такие как .jpg, .png и т.д. Сообщение об ошибке: " +
                        ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void SaveMaterial(object repository)
        {
            if (Material.ID == 0)
            {
                _ = ((ClothesForHandsBaseEntities)repository).Materials.Add(Material);
            }
            else
            {
                try
                {
                    var currentMaterial = ((ClothesForHandsBaseEntities)repository)
                        .Materials.Find(Material.ID);
                    currentMaterial.Suppliers.Clear();
                    currentMaterial.MaterialType = ((ClothesForHandsBaseEntities)repository)
                        .MaterialTypes.First(m => m.ID == CurrentType.ID);
                    currentMaterial.Unit = CurrentUnit;
                    foreach (var supplier in SuppliersOfMaterial)
                    {
                        currentMaterial.Suppliers.Add(((ClothesForHandsBaseEntities)repository)
                            .Suppliers.Find(supplier.ID));
                    }
                    ((ClothesForHandsBaseEntities)repository)
                        .Entry(((ClothesForHandsBaseEntities)repository)
                        .Materials.Find(Material.ID)).CurrentValues
                        .SetValues(currentMaterial);
                }
                catch (InvalidOperationException ex)
                {
                    _ = MessageBox.Show("Материал не обновлён из-за ошибки: " + ex.Message +
                    "Это могло возникнуть из-за конфликта с предыдущим сохранением. " +
                    "Пожалуйста, попробуйте перезайти в приложение и сохранить ещё раз. " +
                    "Если будет ошибка, то обратитесь к администратору вашего предприятия.",
                                               "Ошибка",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
                }
            }
            try
            {
                _ = ((DbContext)repository).SaveChanges();
                _ = MessageBox.Show("Материал успешно обновлён в базе данных!",
                                 "Успешно!",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("Материал не обновлён из-за ошибки: " + ex.Message +
                     "Это могло возникнуть из-за ошибок с подключением к базе данных " +
                     "или из-за сторонней ошибки сохранения, возникшей до попытки текущего сохранения. " +
                     "Пожалуйста, попробуйте перезайти в приложение и сохранить ещё раз. " +
                     "Если будет ошибка, то обратитесь к администратору вашего предприятия.",
                                                "Ошибка",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
            }
        }
    }
}
