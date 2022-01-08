using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao _productDao;
        private readonly IProductCategoryDao _productCategoryDao;
        private readonly SupplierService _supplierService;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, SupplierService supplierService)
        {
            _productDao = productDao;
            _productCategoryDao = productCategoryDao;
            _supplierService = supplierService;
        }

        public ProductCategory GetProductCategory(int categoryId) => _productCategoryDao.Get(categoryId);

        public IEnumerable<Product> GetAllProducts() => _productDao.GetAll();

        public IEnumerable<ProductCategory> GetAllProductCategories() => _productCategoryDao.GetAll();

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {

            if (_productCategoryDao.Get(categoryId) != null)
            {
                ProductCategory category = _productCategoryDao.Get(categoryId);

                return _productDao.GetBy(category);
            }

            return Enumerable.Empty<Product>();
        }


        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {

            if (_supplierService.GetSupplier(supplierId) != null)
            {
                Supplier supplier = _supplierService.GetSupplier(supplierId);

                return _productDao.GetBy(supplier);
            }

            return Enumerable.Empty<Product>();
        }

        public Product GetProduct(int productId) => _productDao.Get(productId);
    }
}
