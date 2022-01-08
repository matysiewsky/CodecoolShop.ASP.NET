using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.ViewModels
{
    public class ProductsIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }

        public Dictionary<ProductCategory, IEnumerable<Product>> ProductsByCategories { get; set; } =
            new Dictionary<ProductCategory, IEnumerable<Product>>();
        public Dictionary<Supplier, IEnumerable<Product>> ProductsBySuppliers { get; set; } =
            new Dictionary<Supplier, IEnumerable<Product>>();
    }
}