using System.Linq.Expressions;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class GenericDbRepository<TEntity>: IGenericDbRepository<TEntity> where TEntity: class
{
    private readonly ShopDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericDbRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filterExpression)
        => _dbSet.FirstOrDefault(filterExpression);

    public IEnumerable<TEntity> GetRange(Expression<Func<TEntity, bool>> filterExpression)
        => _dbSet.Where(filterExpression);

    public void Add(TEntity entity)
        => _dbSet.Add(entity);

    public void Remove(TEntity entity)
        => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities)
        => _dbSet.RemoveRange(entities);

    public void Modify(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}