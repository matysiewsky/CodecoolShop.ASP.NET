using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IDao<T>
    {
        void Add(T item);
        void Remove(T item);

        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
