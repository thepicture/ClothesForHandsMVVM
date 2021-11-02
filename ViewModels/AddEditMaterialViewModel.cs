using ClothesForHandsMVVM.Commands;
using ClothesForHandsMVVM.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
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

        public AddEditMaterialViewModel() { }

        public AddEditMaterialViewModel(Material material)
        {
            Material = new Material();
            if (material != null)
            {
                Material = material;
            }
        }

        public Material Material
        {
            get => _material; set
            {
                _material = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveChangesCommand
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
            get
            {
                if (_unit == null)
                {
                    using (ClothesForHandsBaseEntities repository = new ClothesForHandsBaseEntities())
                    {
                        _unit = new List<string>(repository.Materials.Select(m => m.Unit).Distinct());
                    }
                }
                return _unit;
            }

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

        private void UpdatePicturePreview()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    File.Copy(openFileDialog.FileName, "../../Resources/materials/" + openFileDialog.SafeFileName);
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
                ((ClothesForHandsBaseEntities)repository)
                    .Entry(((ClothesForHandsBaseEntities)repository)
                    .Materials.Find(Material.ID)).CurrentValues
                    .SetValues(Material);
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
