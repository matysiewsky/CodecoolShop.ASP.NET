using System.Linq.Expressions;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IGenericDbRepository<TEntity> where TEntity : class
{
    TEntity Get (Expression<Func<TEntity, bool>> filterExpression);
    IEnumerable<TEntity> GetRange(Expression<Func<TEntity, bool>> filterExpression);
    IEnumerable<TEntity> GetAll();
    void Add(TEntity item);
    void Remove (TEntity item);
    void RemoveRange(IEnumerable<TEntity> items);
    void Modify(TEntity item);
}