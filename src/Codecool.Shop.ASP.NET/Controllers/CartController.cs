using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.ASP.NET.ViewModels;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codecool.Shop.ASP.NET.Controllers;

public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public CartController(ILogger<CartController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _productService = new ProductService(unitOfWork);
        _cartService = new CartService(unitOfWork, httpContextAccessor);
    }


    public ViewResult CartSummary()
    {
        bool existingUser = Request.Cookies.ContainsKey("userId");
        string userId = (existingUser)
            ? Request.Cookies["userId"]
            : _cartService.GetSessionId();

        if (!existingUser) Response.Cookies.Append("userId", userId);

        CartViewModel cartViewModel = new()
        {
            Cart = _cartService.GetCart(userId) ?? _cartService.AddCart(userId),
        };
        cartViewModel.CartItems =
            _cartService.GetCartItems(cartViewModel.Cart.Id);

        foreach (CartItem shoppingCartItem in cartViewModel.CartItems)
        {
            shoppingCartItem.Product = _productService.GetProduct(shoppingCartItem.ProductId);
        }

        return View(cartViewModel);
    }

    public IActionResult AddToCart(int productId)
    {
        bool existingUser = Request.Cookies.ContainsKey("userId");
        string userId = (existingUser)
            ? Request.Cookies["userId"]
            : _cartService.GetSessionId();

        if (!existingUser) Response.Cookies.Append("userId", userId);

        _cartService.AddCartItem(userId, productId);
        return RedirectToAction("CartSummary", "Cart");
    }

    public IActionResult AddQuantity(int cartItemId)
    {
        CartItem itemToUpdate = _cartService.GetCartItem(cartItemId);
        itemToUpdate.Quantity++;
        _cartService.ModifyCartItem(itemToUpdate);
        return RedirectToAction("CartSummary", "Cart");
    }

    public IActionResult SubtractQuantity(int cartItemId)
    {
        CartItem itemToUpdate = _cartService.GetCartItem(cartItemId);
        if (itemToUpdate.Quantity <= 1)
        {
            _cartService.RemoveCartItem(itemToUpdate);
        }
        else
        {
            itemToUpdate.Quantity--;
            _cartService.ModifyCartItem(itemToUpdate);
        }
        return RedirectToAction("CartSummary", "Cart");
    }

    public IActionResult DeleteCartItem(int cartItemId)
    {
        CartItem itemToRemove = _cartService.GetCartItem(cartItemId);
        _cartService.RemoveCartItem(itemToRemove);

        return RedirectToAction("CartSummary", "Cart");
    }
}