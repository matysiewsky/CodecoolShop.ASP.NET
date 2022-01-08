using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IProductDao _productDao;

        private readonly ShoppingCartService _shoppingCartService;

        public CartController(ILogger<CartController> logger, IProductDao productDao, IShoppingCartDao shoppingCartDao,
            IShoppingCartItemDao shoppingCartItemDao, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _productDao = productDao;
            _shoppingCartService = new ShoppingCartService(productDao, shoppingCartDao, shoppingCartItemDao, httpContextAccessor);
        }
        // GET
        public ViewResult CartSummary()
        {
            bool existingUser = Request.Cookies.ContainsKey("userId");
            string userId = (existingUser)
                ? Request.Cookies["userId"]
                : _shoppingCartService.GetCartId();

            if (!existingUser) Response.Cookies.Append("userId", userId);

            CartViewModel cartViewModel = new CartViewModel
            {
                ShoppingCart = _shoppingCartService.GetCartByUserId(userId) ?? _shoppingCartService.ReturnNewCart(userId),
            };
            cartViewModel.CartItems =
                _shoppingCartService.GetCartItemsByShoppingCartId(cartViewModel.ShoppingCart.ShoppingCartId);

            foreach (CartItem shoppingCartItem in cartViewModel.CartItems)
            {
                shoppingCartItem.Product = _productDao.Get(shoppingCartItem.ProductId);
            }

            return View(cartViewModel);
        }

        public IActionResult AddToCart(int productId)
        {
            bool existingUser = Request.Cookies.ContainsKey("userId");
            string userId = (existingUser)
                ? Request.Cookies["userId"]
                : _shoppingCartService.GetCartId();

            if (!existingUser) Response.Cookies.Append("userId", userId);

            _shoppingCartService.AddToCart(userId, productId);
            return RedirectToAction("CartSummary", "Cart");
        }
    }
}