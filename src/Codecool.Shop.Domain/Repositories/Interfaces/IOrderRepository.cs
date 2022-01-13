using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IOrderRepository: IRepository<Order>
{
    public Order GetOrder(string userId);
    public void Modify(Order orderToUpdate);

    // public void UpdateFinalised(string userId);
    //
    // public void UpdatePaid(string userId);

}