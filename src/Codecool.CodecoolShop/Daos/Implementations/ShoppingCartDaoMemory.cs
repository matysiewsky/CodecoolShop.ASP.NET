using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.DbContext;
using Codecool.CodecoolShop.Logic;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ShoppingCartDaoMemory: IShoppingCartDao
    {
        private readonly AppDbContext _appDbContext;

        public ShoppingCartDaoMemory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // public void Add(ShoppingCart item)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void Remove(ShoppingCart item)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public ShoppingCart Get(int id)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public IEnumerable<ShoppingCart> GetAll()
        // {
        //     throw new System.NotImplementedException();
        // }

        public void Add(ShoppingCart item)
        {
            // item.Id = _appDbContext.ShoppingCarts.Count + 1;
            _appDbContext.ShoppingCarts.Add(item);
            _appDbContext.SaveChanges();
        }

        public ShoppingCart AddAndReturn(ShoppingCart item)
        {
            // item.Id = _appDbContext.ShoppingCarts.Count + 1;
            _appDbContext.ShoppingCarts.Add(item);
            _appDbContext.SaveChanges();
            return item;
        }

        public void Remove(ShoppingCart item)
        {
            _appDbContext.ShoppingCarts.Remove(item);
            _appDbContext.SaveChanges();
        }

        public ShoppingCart Get(int id)
        {
            return _appDbContext.ShoppingCarts.FirstOrDefault(x => x.ShoppingCartId == id);
        }

        public ShoppingCart GetByUserId(string userId)
        {
            return _appDbContext.ShoppingCarts.FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _appDbContext.ShoppingCarts;
        }


    }
}