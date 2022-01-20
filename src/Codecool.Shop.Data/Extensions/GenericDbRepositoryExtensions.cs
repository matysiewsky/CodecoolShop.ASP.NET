using Codecool.Shop.Data.Infrastructure;
using Codecool.Shop.Data.Infrastructure.Repository;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Data.Extensions;

public static class GenericDbRepositoryExtensions
{
    public static GenericDbRepository<TEntity> GenericDbRepoFactory<TEntity>
        (ShopDbContext dbContext)
        where TEntity : BaseEntity
        => new()
        {
            DbContext = dbContext,
            DbSet = dbContext.Set<TEntity>()
        };

}