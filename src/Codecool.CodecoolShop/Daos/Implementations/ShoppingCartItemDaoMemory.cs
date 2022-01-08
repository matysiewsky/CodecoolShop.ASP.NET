using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.DbContext;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ShoppingCartItemDaoMemory: IShoppingCartItemDao
    {
        private readonly AppDbContext _appDbContext;

        public ShoppingCartItemDaoMemory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(CartItem item)
        {
            // item.Id = _appDbContext.CartItems.Count + 1;
            _appDbContext.CartItems.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(CartItem item)
        {
            _appDbContext.CartItems.Remove(item);
            _appDbContext.SaveChanges();
        }

        public CartItem Get(int id)
        {
            return _appDbContext.CartItems.FirstOrDefault(x => x.ItemId == id);
        }

        public IEnumerable<CartItem> GetAllForUser(int shoppingCartId)
        {
            return _appDbContext.CartItems.Where(
                c => c.ShoppingCartId == shoppingCartId);
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _appDbContext.CartItems;
        }

        public void Update(int itemId)
        {
            CartItem itemToUpdate = Get(itemId);
            itemToUpdate.Quantity++;
            _appDbContext.SaveChanges();
        }
    }
}