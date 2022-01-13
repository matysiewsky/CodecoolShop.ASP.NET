using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.ViewModels
{
    public class ProductsIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public int ItemsInCart { get; set; }

        public Dictionary<ProductCategory, IEnumerable<Product>> ProductsByCategories { get; set; } =
            new Dictionary<ProductCategory, IEnumerable<Product>>();
        public Dictionary<Supplier, IEnumerable<Product>> ProductsBySuppliers { get; set; } =
            new Dictionary<Supplier, IEnumerable<Product>>();
    }
}