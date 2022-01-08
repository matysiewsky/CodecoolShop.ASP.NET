using System;
using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Logic;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;

namespace Codecool.CodecoolShop.Services
{
    public class ShoppingCartService
    {
        private readonly IProductDao _productDao;
        private readonly IShoppingCartDao _shoppingCartDao;
        private readonly IShoppingCartItemDao _shoppingCartItemDao;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartId";


        public ShoppingCartService(IProductDao productDao, IShoppingCartDao shoppingCartDao,
            IShoppingCartItemDao shoppingCartItemDao, IHttpContextAccessor httpContextAccessor)
        {
            _productDao = productDao;
            _shoppingCartDao = shoppingCartDao;
            _shoppingCartItemDao = shoppingCartItemDao;
            _httpContextAccessor = httpContextAccessor;
        }

        public ShoppingCart ReturnNewCart(string userId) =>
            _shoppingCartDao.AddAndReturn(new ShoppingCart
            {
                UserId = userId,
            });

        public void AddToCart(string userId, int productId)
        {

            Product productToAdd = _productDao.Get(productId);
            ShoppingCart shoppingCart = _shoppingCartDao.GetByUserId(userId) ?? _shoppingCartDao.AddAndReturn(new ShoppingCart
            {
                UserId = userId,
            });
            IEnumerable<CartItem> shoppingCartItems = _shoppingCartItemDao.GetAllForUser(shoppingCart.ShoppingCartId);

            foreach (CartItem shoppingCartItem in shoppingCartItems)
            {
                shoppingCartItem.Product = _productDao.Get(shoppingCartItem.ProductId);
            }

            CartItem cartItem = shoppingCartItems.SingleOrDefault(
                cartItem => cartItem.Product.Id == productId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.
                cartItem = new CartItem
                {
                    Product = productToAdd,
                    ShoppingCartId = shoppingCart.ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now,
                    ProductId = productToAdd.Id,
                };

                _shoppingCartItemDao.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,
                // then add one to the quantity.
                _shoppingCartItemDao.Update(cartItem.ItemId);
            }

            // SaveChanges();
        }

        // public void Dispose()
        // {
        //     if (_db != null)
        //     {
        //         _db.Dispose();
        //         _db = null;
        //     }
        // }

        public string GetCartId()
        {

            // if (_httpContextAccessor.HttpContext.Session.Id == null)
            // {
            //     if (!string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.User.Identity.Name))
            //     {
            //         _httpContextAccessor.HttpContext.Session.Id = _httpContextAccessor.HttpContext.User.Identity.Name);
            //         //Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
            //     }
            //     else
            //     {
            //         // Generate a new random GUID using System.Guid class.
            //         _httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, Guid.NewGuid().ToString());
            //
            //     }
            // }
            return _httpContextAccessor.HttpContext.Session.Id;
        }

        public ShoppingCart GetCart(int id) => _shoppingCartDao.Get(id);
        public ShoppingCart GetCartByUserId(string userId) => _shoppingCartDao.GetByUserId(userId);

        public IEnumerable<CartItem> GetCartItemsByShoppingCartId(int shoppingCartId) =>
            _shoppingCartItemDao.GetAllForUser(shoppingCartId);

        public void ClearCartItems(string userId) => _shoppingCartDao.Remove(GetCartByUserId(userId));

        // public void SaveChanges()
        // {
        //     _shoppingCartDao.SaveChanges();
        //     _shoppingCartItemDao.SaveChanges();
        // }

        // public List<CartItem> GetCartItems()
        // {
        //     UserId = GetCartId();
        //
        //     return _db.ShoppingCartItems.Where(
        //         c => c.CartId == UserId).ToList();
        // }
    }
}