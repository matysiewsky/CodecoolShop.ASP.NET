using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class OrderDbRepository: IOrderRepository
{
    public ShopDbContext ShopDbContext { get; init; }

    public Order Add(Order item)
    {
        ShopDbContext.Orders.Add(item);
        ShopDbContext.SaveChanges();

        return item;
    }

    public void Remove(Order item)
    {
        ShopDbContext.Orders.Remove(item);
        ShopDbContext.SaveChanges();
    }

    public Order Get(int id) => ShopDbContext.Orders.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Order> GetAll() => ShopDbContext.Orders;
    public Order GetOrder(string userId) => ShopDbContext.Orders.FirstOrDefault(x => x.UserId == userId);



    // public void Update<T>(string userId, Dictionary<string, T> valuesToUpdate)
    // {
    //     Order orderToUpdate = GetOrder(userId);
    //
    //     foreach (KeyValuePair<string, T> item in valuesToUpdate)
    //     {
    //         orderToUpdate.item.Key) = item.Value;
    //     }
    // }

    public void Modify(Order orderToUpdate)
    {
        ShopDbContext.Attach(orderToUpdate);
        ShopDbContext.Entry(orderToUpdate).State = EntityState.Modified;
        ShopDbContext.SaveChanges();
    }
    // public void UpdatePaid(string userId)
    // {
    //     Order itemToUpdate = GetOrder(userId);
    //     itemToUpdate.Quantity++;
    //     AppDbContext.SaveChanges();
    // }

}