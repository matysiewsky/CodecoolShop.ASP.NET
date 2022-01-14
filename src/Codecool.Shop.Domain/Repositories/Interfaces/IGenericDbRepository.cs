using System.Linq.Expressions;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IGenericDbRepository<T> where T : class
{
    T Get (Expression<Func<T, bool>> filterExpression);
    IEnumerable<T> GetRange(Expression<Func<T, bool>> filterExpression);
    void Add(T item);
    void Remove (T item);
    void RemoveRange(IEnumerable<T> items);
    void Modify(T item);
}