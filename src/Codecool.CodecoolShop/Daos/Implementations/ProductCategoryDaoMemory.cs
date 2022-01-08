using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.DbContext;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoMemory : IProductCategoryDao
    {
        // private List<ProductCategory> data = new List<ProductCategory>();
        // private static ProductCategoryDaoMemory instance = null;

        // private ProductCategoryDaoMemory()
        // {
        // }
        //
        // public static ProductCategoryDaoMemory GetInstance()
        // {
        //     if (instance == null)
        //     {
        //         instance = new ProductCategoryDaoMemory();
        //     }
        //
        //     return instance;
        // }

        private readonly AppDbContext _appDbContext;

        public ProductCategoryDaoMemory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Add(ProductCategory item)
        {
            // item.Id = _appDbContext.Categories.Count + 1;
            _appDbContext.Categories.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(ProductCategory item)
        {
            _appDbContext.Categories.Remove(item);
            _appDbContext.SaveChanges();
        }

        public ProductCategory Get(int id)
        {
            return _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _appDbContext.Categories;
        }
    }
}
