using ClothesForHandsMVVM.Models;

namespace ClothesForHandsMVVM.ViewModels
{
    public class AddEditMaterialViewModel : ViewModelBase
    {
        private Material _material;

        public AddEditMaterialViewModel()
        {
        }

        public AddEditMaterialViewModel(Material material)
        {
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
    }
}
