using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface ICartRepository: IRepository<Cart>
{
    public Cart Get(string userId);

}