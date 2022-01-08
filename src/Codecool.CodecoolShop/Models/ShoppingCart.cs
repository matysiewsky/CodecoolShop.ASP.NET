using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Logic
{
    public class ShoppingCart
        //: IDisposable
    {
        // private readonly IProductDao productDao;
        // private readonly IShoppingCartDao shoppingCartDao;
        // private readonly IShoppingCartItemDao shoppingCartItemDao;
        // private readonly IHttpContextAccessor httpContextAccessor;
        //
        [Key]
        public int ShoppingCartId { get; set; }
        public string UserId { get; set; }

        //
        // public ShoppingCart(IProductDao productDao, IShoppingCartDao shoppingCartDao,
        //     IShoppingCartItemDao shoppingCartItemDao, IHttpContextAccessor httpContextAccessor)
        // {
        //     this.productDao = productDao;
        //     this.shoppingCartDao = shoppingCartDao;
        //     this.shoppingCartItemDao = shoppingCartItemDao;
        //     this.httpContextAccessor = httpContextAccessor;
        // }

        // public void AddToCart(int id)
        // {
        //     // Retrieve the product from the database.
        //     UserId = GetCartId();
        //
        //     var cartItem = _db.ShoppingCartItems.SingleOrDefault(
        //         c => c.CartId == UserId
        //              && c.ProductId == id);
        //     if (cartItem == null)
        //     {
        //         // Create a new cart item if no cart item exists.
        //         cartItem = new CartItem
        //         {
        //             ItemId = Guid.NewGuid().ToString(),
        //             ProductId = id,
        //             CartId = UserId,
        //             Product = _db.Products.SingleOrDefault(
        //                 p => p.ProductID == id),
        //             Quantity = 1,
        //             DateCreated = DateTime.Now
        //         };
        //
        //         _db.ShoppingCartItems.Add(cartItem);
        //     }
        //     else
        //     {
        //         // If the item does exist in the cart,
        //         // then add one to the quantity.
        //         cartItem.Quantity++;
        //     }
        //
        //     _db.SaveChanges();
        // }
        //
        // // public void Dispose()
        // // {
        // //     if (_db != null)
        // //     {
        // //         _db.Dispose();
        // //         _db = null;
        // //     }
        // // }
        //
        // public string GetCartId()
        // {
        //     if (httpContextAccessor.HttpContext.Session.GetString(CartSessionKey) == null)
        //     {
        //         if (string.IsNullOrWhiteSpace(httpContextAccessor.HttpContext.User.Identity.Name))
        //         {
        //             httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, httpContextAccessor.HttpContext.User.Identity.Name);
        //             //Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
        //         }
        //         else
        //         {
        //             // Generate a new random GUID using System.Guid class.
        //             httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, Guid.NewGuid().ToString());
        //
        //         }
        //     }
        //
        //     return httpContextAccessor.HttpContext.Session.GetString(CartSessionKey);
        // }
        //
        // public List<CartItem> GetCartItems()
        // {
        //     UserId = GetCartId();
        //
        //     return _db.ShoppingCartItems.Where(
        //         c => c.CartId == UserId).ToList();
        // }
    }
}

