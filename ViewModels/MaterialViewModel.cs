using ClothesForHandsMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClothesForHandsMVVM.ViewModels
{
    public class MaterialViewModel : ViewModelBase
    {
        private IEnumerable<Material> _materialsList;

        public MaterialViewModel()
        {
            using (ClothesForHandsBaseEntities context = new ClothesForHandsBaseEntities())
            {
                MaterialsList = context.Materials.ToList();
            }
        }

        public IEnumerable<Material> MaterialsList
        {
            get => _materialsList; set
            {
                _materialsList = value;
                OnPropertyChanged();
            }
        }
    }
}
