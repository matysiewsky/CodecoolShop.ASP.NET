using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class ClientDbRepository: IClientRepository
{
    public ShopDbContext ShopDbContext { get; init; }

    public Client Add(Client item)
    {
        ShopDbContext.Clients.Add(item);
        ShopDbContext.SaveChanges();

        return item;
    }

    public void Remove(Client item)
    {
        ShopDbContext.Clients.Remove(item);
        ShopDbContext.SaveChanges();
    }

    public Client Get(int id) => ShopDbContext.Clients.FirstOrDefault(x => x.ClientId == id);

    public Client Get(string userId) => ShopDbContext.Clients.FirstOrDefault(x => x.UserId == userId);
    public IEnumerable<Client> GetAll() => ShopDbContext.Clients;
}