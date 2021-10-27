namespace ClothesForHandsMVVM.Models
{
    public interface ICRUDOperationHandler<T>
    {
        bool Create(T obj);
        bool Read(int id);
        bool Update(T obj);
        bool Delete(int id);
    }
}
