namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IRepository<T>
{
    T Add(T item);
    void Remove(T item);

    T Get(int id);
    IEnumerable<T> GetAll();
}