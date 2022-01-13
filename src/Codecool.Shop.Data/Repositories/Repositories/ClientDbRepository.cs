using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class ClientDbRepository: IClientRepository
{
    public AppDbContext AppDbContext { get; init; }

    public Client Add(Client item)
    {
        AppDbContext.Clients.Add(item);
        AppDbContext.SaveChanges();

        return item;
    }

    public void Remove(Client item)
    {
        AppDbContext.Clients.Remove(item);
        AppDbContext.SaveChanges();
    }

    public Client Get(int id) => AppDbContext.Clients.FirstOrDefault(x => x.ClientId == id);

    public Client Get(string userId) => AppDbContext.Clients.FirstOrDefault(x => x.UserId == userId);
    public IEnumerable<Client> GetAll() => AppDbContext.Clients;
}