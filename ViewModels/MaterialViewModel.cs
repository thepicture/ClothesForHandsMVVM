﻿using ClothesForHandsMVVM.Models;
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