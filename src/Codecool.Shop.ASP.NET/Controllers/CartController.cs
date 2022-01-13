using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.ASP.NET.ViewModels;
using Codecool.Shop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codecool.Shop.ASP.NET.Controllers;

public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public CartController(ILogger<CartController> logger, IProductService productService, ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }
    // GET
    public ViewResult CartSummary()
    {
        bool existingUser = Request.Cookies.ContainsKey("userId");
        string userId = (existingUser)
            ? Request.Cookies["userId"]
            : _cartService.GetCartId();

        if (!existingUser) Response.Cookies.Append("userId", userId);

        CartViewModel cartViewModel = new CartViewModel
        {
            Cart = _cartService.GetCartByUserId(userId) ?? _cartService.ReturnNewCart(userId),
        };
        cartViewModel.CartItems =
            _cartService.GetCartItemsByShoppingCartId(cartViewModel.Cart.Id);

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
            : _cartService.GetCartId();

        if (!existingUser) Response.Cookies.Append("userId", userId);

        _cartService.AddToCart(userId, productId);
        return RedirectToAction("CartSummary", "Cart");
    }

    public IActionResult AddQuantity(int cartItemId)
    {
        CartItem itemToUpdate = _cartService.GetCartItem(cartItemId);
        itemToUpdate.Quantity++;
        _cartService.Modify(itemToUpdate);
        return RedirectToAction("CartSummary", "Cart");
    }

    public IActionResult SubtractQuantity(int cartItemId)
    {
        CartItem itemToUpdate = _cartService.GetCartItem(cartItemId);
        if (itemToUpdate.Quantity <= 1)
        {
            _cartService.ClearCartItem(itemToUpdate);
        }
        else
        {
            itemToUpdate.Quantity--;
            _cartService.Modify(itemToUpdate);
        }
        return RedirectToAction("CartSummary", "Cart");
    }

    public IActionResult DeleteCartItem(int cartItemId)
    {
        CartItem itemToRemove = _cartService.GetCartItem(cartItemId);
        _cartService.ClearCartItem(itemToRemove);

        return RedirectToAction("CartSummary", "Cart");
    }
}