using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codecool.Shop.ASP.NET.Components;

public class CartItemsCount: ViewComponent
{
    private readonly ICartService _cartService;

    public CartItemsCount(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IViewComponentResult Invoke()
    {

        bool existingUser = Request.Cookies.ContainsKey("userId");
        string userId = Request.Cookies["userId"];
        int itemsInCartCount = (existingUser) ? _cartService.GetCartItemsCount(userId) : 0;

        return View(itemsInCartCount);
    }
}