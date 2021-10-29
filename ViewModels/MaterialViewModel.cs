using ClothesForHandsMVVM.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClothesForHandsMVVM.ViewModels
{
    public class MaterialViewModel : ViewModelBase
    {
        private List<Material> _materialsList;

        public MaterialViewModel()
        {
            _materialsList = new List<Material>();
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
    }
}
