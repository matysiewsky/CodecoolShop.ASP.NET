using System.Collections.Generic;
using System.Diagnostics;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.ASP.NET.ViewModels;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codecool.Shop.ASP.NET.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ISupplierService _supplierService;
    private readonly ICartService _cartService;

    public HomeController(IProductService productService, ISupplierService supplierService,
        ICartService cartService)
        {
            _productService = productService;
            _supplierService = supplierService;
            _cartService = cartService;
        }


    public ViewResult Products()
    {
        ProductsIndexViewModel productsIndexViewModel = new()
        {
            Products = _productService.GetAllProducts(),
            Suppliers = _supplierService.GetAllSuppliers(),
            Categories = _productService.GetAllProductCategories(),
        };

        foreach (ProductCategory category in productsIndexViewModel.Categories)
        {
            IEnumerable<Product> productsForCategory = _productService.GetProductsForCategory(category.Id);

            foreach (Product product in productsForCategory)
            {
                product.Supplier = _supplierService.GetSupplier(product.SupplierId);
            }
            productsIndexViewModel.ProductsByCategories.Add(category, productsForCategory);
        }

        foreach (Supplier supplier in productsIndexViewModel.Suppliers)
        {
            IEnumerable<Product> productsForSupplier = _productService.GetProductsForSupplier(supplier.Id);

            foreach (Product product in productsForSupplier)
            {
                product.Supplier = _supplierService.GetSupplier(product.SupplierId);
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