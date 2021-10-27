using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesForHandsMVVM.Models
{
    public class MaterialService : ICRUDOperationHandler<Material>
    {
        private static IEnumerable<Material> _materialsList;

        public MaterialService()
        {
            using (ClothesForHandsBaseEntities context = new ClothesForHandsBaseEntities())
            {
                _materialsList = context.Materials;
            }
        }

        public bool Create(Material obj)
        {
            _materialsList.Append(obj);
            return true;
        }

        public bool Delete(int id)
        {
            _materialsList
        }

        public bool Read(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Material obj)
        {
            throw new NotImplementedException();
        }
    }
}
