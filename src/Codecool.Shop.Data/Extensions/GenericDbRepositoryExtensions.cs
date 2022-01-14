using Codecool.Shop.Data.Infrastructure;
using Codecool.Shop.Data.Infrastructure.Repository;

namespace Codecool.Shop.Data.Extensions;

public static class GenericDbRepositoryExtensions
{
    public static GenericDbRepository<TEntity> GenericDbRepoFactory<TEntity>
        (ShopDbContext dbContext)
        where TEntity : class
        => new GenericDbRepository<TEntity>
        {
            DbContext = dbContext,
            DbSet = dbContext.Set<TEntity>()
        };

}