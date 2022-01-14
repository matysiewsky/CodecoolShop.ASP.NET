using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class CartService : ICartService
{
    public IProductService ProductService { get; init; }
    public IGenericDbRepository<Cart> CartRepository { get; init; }
    public IGenericDbRepository<CartItem> CartItemRepository { get; init; }
    public IHttpContextAccessor HttpContextAccessor { get; init; }

    private const string CartSessionKey = "CartId";

    public Cart ReturnNewCart(string userId)
    {
        CartRepository.Add(new Cart
        {
            UserId = userId,
        });

        return CartRepository.Get(x => x.UserId == userId);
    }

    public void AddToCart(string userId, int productId)
    {

        Product productToAdd = ProductService.GetProduct(productId);
        Cart cart = CartRepository.Get(x => x.UserId == userId);

        if (cart == null)
        {
            CartRepository.Add(new Cart
            {
                UserId = userId,
            });

            cart = CartRepository.Get(x => x.UserId == userId);
        }

        IEnumerable<CartItem> shoppingCartItems = CartItemRepository.GetRange
            (x=> x.CartId == cart.Id);

        foreach (CartItem shoppingCartItem in shoppingCartItems)
        {
            shoppingCartItem.Product = ProductService.GetProduct(shoppingCartItem.ProductId);
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

            CartItemRepository.Add(cartItem);
        }
        else
        {

            CartItemRepository.Modify(cartItem);
        }
    }

    public string GetCartId()
        => HttpContextAccessor.HttpContext.Session.Id;

    public Cart GetCart(int id)
        => CartRepository.Get(x=> x.Id == id);
    public CartItem GetCartItem(int id)
        => CartItemRepository.Get(x => x.Id == id);
    public Cart GetCartByUserId(string userId)
        => CartRepository.Get(x => x.UserId == userId);

    public IEnumerable<CartItem> GetCartItemsByShoppingCartId(int shoppingCartId)
        => CartItemRepository.GetRange(x => x.CartId == shoppingCartId);

    public int GetCartItemsCount(string userId)
    {
        int id = GetCartByUserId(userId).Id;
        return GetCartItemsByShoppingCartId(id).Count();
    }

    public void ClearCartAndCartItems(Cart cartToClear)
    {
        IEnumerable<CartItem> cartItemsToClear = GetCartItemsByShoppingCartId(cartToClear.Id);
        CartItemRepository.RemoveRange(cartItemsToClear);
        CartRepository.Remove(cartToClear);
    }

    public void ClearCartItem(CartItem item)
    {
        CartItemRepository.Remove(item);
    }
    public void Modify(CartItem orderToUpdate)
    {
        CartItemRepository.Modify(orderToUpdate);
    }
}
