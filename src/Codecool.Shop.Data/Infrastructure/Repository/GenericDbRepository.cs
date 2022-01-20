using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codecool.Shop.Data.Infrastructure.Repository;

public class GenericDbRepository<TEntity>: IGenericDbRepository<TEntity>
    where TEntity: BaseEntity
{
    public ShopDbContext DbContext { private get; init; }
    public DbSet<TEntity> DbSet { private get; init; }

    public TEntity Get(Func<TEntity, bool> filterExpression)
        => DbSet.FirstOrDefault(filterExpression);

    public IEnumerable<TEntity> GetRange(Func<TEntity, bool> filterExpression)
        => DbSet.Where(filterExpression);

    public IEnumerable<TEntity> GetAll()
        => DbSet;

    public void Add(TEntity entity)
        => DbSet.Add(entity);

    public void Remove(TEntity entity)
        => DbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities)
        => DbSet.RemoveRange(entities);

    public void Modify(TEntity entity)
    {
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }
}