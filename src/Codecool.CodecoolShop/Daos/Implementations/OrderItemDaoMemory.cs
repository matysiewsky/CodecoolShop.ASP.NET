using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.DbContext;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderItemDaoMemory: IOrderItemDao
    {
        private readonly AppDbContext _appDbContext;

        public OrderItemDaoMemory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(OrderItem item)
        {
            // item.Id = _appDbContext.Count + 1;
            _appDbContext.Orders.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(OrderItem item)
        {
            _appDbContext.Orders.Remove(item);
            _appDbContext.SaveChanges();
        }

        public OrderItem Get(int id)
        {
            return _appDbContext.Orders.FirstOrDefault(x => x.OrderId == id);
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _appDbContext.Orders;
        }
    }
}