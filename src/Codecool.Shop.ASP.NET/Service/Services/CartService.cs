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
    public IProductService ProductService { get; init; }
    public ICartRepository CartRepository { get; init; }
    public ICartItemRepository CartItemRepository { get; init; }
    public IHttpContextAccessor HttpContextAccessor { get; init; }

    private const string CartSessionKey = "CartId";

    public Cart ReturnNewCart(string userId) =>
        CartRepository.Add(new Cart
        {
            UserId = userId,
        });

    public void AddToCart(string userId, int productId)
    {

        Product productToAdd = ProductService.GetProduct(productId);
        Cart cart = CartRepository.Get(userId) ?? CartRepository.Add(new Cart
        {
            UserId = userId,
        });
        IEnumerable<CartItem> shoppingCartItems = CartItemRepository.GetAll(cart.Id);

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

            CartItemRepository.Update(cartItem.Id);
        }
    }

    public string GetCartId()
    {
        return HttpContextAccessor.HttpContext.Session.Id;
    }

    public Cart GetCart(int id) => CartRepository.Get(id);
    public CartItem GetCartItem(int id) => CartItemRepository.Get(id);
    public Cart GetCartByUserId(string userId) => CartRepository.Get(userId);

    public IEnumerable<CartItem> GetCartItemsByShoppingCartId(int shoppingCartId) =>
        CartItemRepository.GetAll(shoppingCartId);

    public int GetCartItemsCount(string userId)
    {
        int id = GetCartByUserId(userId).Id;
        return GetCartItemsByShoppingCartId(id).Count();
    }

    public void ClearCartAndCartItems(Cart cartToClear)
    {
        CartItemRepository.RemoveCartItems(cartToClear.Id);
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
