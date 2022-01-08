using System.Collections.Generic;
using System.Diagnostics;
using Codecool.CodecoolShop.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.ViewModels;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private ProductService ProductService { get; set; }
        private SupplierService SupplierService { get; set; }

        public ProductController(ILogger<ProductController> logger, IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao)
        {
            _logger = logger;
            SupplierService = new SupplierService(supplierDao);
            ProductService = new ProductService(productDao, productCategoryDao, SupplierService);
        }

        public ViewResult Index()
        {
            ProductsIndexViewModel productsIndexViewModel = new ProductsIndexViewModel
            {
                Products = ProductService.GetAllProducts(),
                Suppliers = SupplierService.GetAllSuppliers(),
                Categories = ProductService.GetAllProductCategories()
            };

            foreach (ProductCategory category in productsIndexViewModel.Categories)
            {
                IEnumerable<Product> productsForCategory = ProductService.GetProductsForCategory(category.Id);

                foreach (Product product in productsForCategory)
                {
                    product.Supplier = SupplierService.GetSupplier(product.SupplierId);
                }
                productsIndexViewModel.ProductsByCategories.Add(category, productsForCategory);
            }

            foreach (Supplier supplier in productsIndexViewModel.Suppliers)
            {
                IEnumerable<Product> productsForSupplier = ProductService.GetProductsForSupplier(supplier.Id);

                foreach (Product product in productsForSupplier)
                {
                    product.Supplier = SupplierService.GetSupplier(product.SupplierId);
                }
                productsIndexViewModel.ProductsBySuppliers.Add(supplier, productsForSupplier);
            }
            return View(productsIndexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
