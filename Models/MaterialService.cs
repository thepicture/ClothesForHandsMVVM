using System.Collections.Generic;
using System.Linq;

namespace ClothesForHandsMVVM.Models
{
    public class MaterialService : ICRUDOperationHandler<Material>
    {
        private static IList<Material> _materialsList;

        public MaterialService()
        {
            using (ClothesForHandsBaseEntities context = new ClothesForHandsBaseEntities())
            {
                _materialsList = context.Materials.ToList();
            }
        }

        public bool Create(Material obj)
        {
            _ = _materialsList.Append(obj);
            return true;
        }

        public bool Delete(int id)
        {
            Material material = _materialsList.FirstOrDefault(m => m.ID == id);
            return !(material is null) && _materialsList.Remove(material);
        }

        public Material Read(int id)
        {
            return _materialsList.FirstOrDefault(m => m.ID == id);
        }

        public bool Update(Material obj)
        {
            Material material = _materialsList.FirstOrDefault(m => m.ID == obj.ID);
            using (ClothesForHandsBaseEntities context = new ClothesForHandsBaseEntities())
            {
                context.Entry(material).CurrentValues.SetValues(obj);
            }
            return true;
        }
    }
}
