using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.DbContext;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoMemory : IProductDao
    {
        // private List<Product> data = new List<Product>();
        // private static ProductDaoMemory instance = null;
        //
        // private ProductDaoMemory()
        // {
        // }
        //
        // public static ProductDaoMemory GetInstance()
        // {
        //     if (instance == null)
        //     {
        //         instance = new ProductDaoMemory();
        //     }
        //
        //     return instance;
        // }

        private readonly AppDbContext _appDbContext;

        public ProductDaoMemory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Add(Product item)
        {
            // item.Id = _appDbContext.Products.Count + 1;
            _appDbContext.Products.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(Product item)
        {
            _appDbContext.Products.Remove(item);
            _appDbContext.SaveChanges();
        }

        public Product Get(int id)
        {
            return _appDbContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _appDbContext.Products;
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            return _appDbContext.Products.Where(x => x.Supplier.Id == supplier.Id);
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            return _appDbContext.Products.Where(x => x.ProductCategory.Id == productCategory.Id);
        }

    }
}
