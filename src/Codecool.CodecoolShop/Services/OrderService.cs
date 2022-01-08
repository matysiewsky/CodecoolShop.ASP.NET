using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Logic;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;

namespace Codecool.CodecoolShop.Services
{
    public class OrderService
    {
        private readonly IOrderItemDao _orderItemDao;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly ProductService _productService;


        public OrderService(IOrderItemDao orderItemDao, IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao, IShoppingCartDao shoppingCartDao,
            IShoppingCartItemDao shoppingCartItemDao, IHttpContextAccessor httpContextAccessor)
        {
            _orderItemDao = orderItemDao;
            _shoppingCartService = new ShoppingCartService(productDao, shoppingCartDao,
                shoppingCartItemDao, httpContextAccessor);
            _productService = new ProductService(productDao, productCategoryDao, new SupplierService(supplierDao));

        }


        public ShoppingCart GetShoppingCartByUserId(string userId) => _shoppingCartService.GetCartByUserId(userId);

        public IEnumerable<int> GetProductsIdsByUserId(string userId) =>
            GetCartItemsByUserId(userId).Select(cartItem => cartItem.ProductId);


        public double GetTotalPrice(string userId) =>
            GetCartItemsByUserId(userId).Select(cartItem => cartItem.Product.DefaultPrice).Sum();


        private IEnumerable<CartItem> GetCartItemsByUserId(string userId)
        {
            ShoppingCart shoppingCart = GetShoppingCartByUserId(userId);

            IEnumerable<CartItem> cartItems =
                _shoppingCartService.GetCartItemsByShoppingCartId(GetShoppingCartByUserId(userId).ShoppingCartId);

            return GetProductsForImportedCartItems(cartItems);
        }

        private IEnumerable<CartItem> GetProductsForImportedCartItems(IEnumerable<CartItem> cartItems)
        {
            foreach (CartItem cartItem in cartItems)
            {
                cartItem.Product = _productService.GetProduct(cartItem.ProductId);
            }

            return cartItems;
        }

        public void AddFinalizedOrder(OrderItem item)
        {
            _orderItemDao.Add(item);
        }

        public void ClearCartItems(string userId)
        {

        }
    }
}