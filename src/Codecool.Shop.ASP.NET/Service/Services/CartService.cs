using System;
using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class CartService : ICartService
{
    public IUnitOfWork UnitOfWork { private get; init; }
    public IHttpContextAccessor HttpContextAccessor { private get; init; }

    public Cart ReturnNewCart(string userId)
    {
        Cart newCart = new Cart { UserId = userId };

        UnitOfWork.Carts.Add(newCart);
        UnitOfWork.Save();

        return newCart;
    }

    public void AddToCart(string userId, int productId)
    {
        Product productToAdd = UnitOfWork.Products.Get(x => x.Id == productId);
        Cart cart = UnitOfWork.Carts.Get(x => x.UserId == userId);

        if (cart == null)
        {
            Cart newCart = new Cart { UserId = userId };
            cart = newCart;

            UnitOfWork.Carts.Add(newCart);
            UnitOfWork.Save();
        }

        IEnumerable<CartItem> shoppingCartItems = UnitOfWork.CartItems.GetRange
            (x=> x.CartId == cart.Id);

        foreach (CartItem shoppingCartItem in shoppingCartItems)
        {
            shoppingCartItem.Product = UnitOfWork.Products.Get
                (x => x.Id == shoppingCartItem.ProductId);
        }

        CartItem cartItem = shoppingCartItems.SingleOrDefault(
            cartItem => cartItem.Product.Id == productId);

        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                Product = productToAdd,
                CartId = cart.Id,
                Quantity = 1,
                DateCreated = DateTime.Now,
                ProductId = productToAdd.Id,
            };

            UnitOfWork.CartItems.Add(cartItem);
            UnitOfWork.Save();
        }
        else
        {
            UnitOfWork.CartItems.Modify(cartItem);
            UnitOfWork.Save();
        }
    }

    public string GetCartId()
        => HttpContextAccessor.HttpContext!.Session.Id;

    public Cart GetCart(int id)
        => UnitOfWork.Carts.Get(x=> x.Id == id);
    public CartItem GetCartItem(int id)
        => UnitOfWork.CartItems.Get(x => x.Id == id);
    public Cart GetCartByUserId(string userId)
        => UnitOfWork.Carts.Get(x => x.UserId == userId);

    public IEnumerable<CartItem> GetCartItemsByShoppingCartId(int shoppingCartId)
        => UnitOfWork.CartItems.GetRange(x => x.CartId == shoppingCartId);

    public int GetCartItemsCount(string userId)
    {
        int id = GetCartByUserId(userId).Id;
        return GetCartItemsByShoppingCartId(id).Count();
    }

    public void ClearCartItem(CartItem item)
    {
        UnitOfWork.CartItems.Remove(item);
        UnitOfWork.Save();
    }
    public void Modify(CartItem orderToUpdate)
    {
        UnitOfWork.CartItems.Modify(orderToUpdate);
        UnitOfWork.Save();
    }
}
