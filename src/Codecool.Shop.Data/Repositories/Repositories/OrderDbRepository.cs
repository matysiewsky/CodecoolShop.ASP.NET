using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class OrderDbRepository: IOrderRepository
{
    public AppDbContext AppDbContext { get; init; }

    public Order Add(Order item)
    {
        AppDbContext.Orders.Add(item);
        AppDbContext.SaveChanges();

        return item;
    }

    public void Remove(Order item)
    {
        AppDbContext.Orders.Remove(item);
        AppDbContext.SaveChanges();
    }

    public Order Get(int id) => AppDbContext.Orders.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Order> GetAll() => AppDbContext.Orders;
    public Order GetOrder(string userId) => AppDbContext.Orders.FirstOrDefault(x => x.UserId == userId);



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
        AppDbContext.Attach(orderToUpdate);
        AppDbContext.Entry(orderToUpdate).State = EntityState.Modified;
        AppDbContext.SaveChanges();
    }
    // public void UpdatePaid(string userId)
    // {
    //     Order itemToUpdate = GetOrder(userId);
    //     itemToUpdate.Quantity++;
    //     AppDbContext.SaveChanges();
    // }

}