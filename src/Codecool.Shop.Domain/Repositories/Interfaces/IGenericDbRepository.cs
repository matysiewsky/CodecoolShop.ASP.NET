using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IGenericDbRepository<TEntity>
    where TEntity: BaseEntity
{
    TEntity Get (Func<TEntity, bool> filterExpression);
    IEnumerable<TEntity> GetRange(Func<TEntity, bool> filterExpression);
    IEnumerable<TEntity> GetAll();
    void Add(TEntity item);
    void Remove (TEntity item);
    void RemoveRange(IEnumerable<TEntity> items);
    void Modify(TEntity item);
}