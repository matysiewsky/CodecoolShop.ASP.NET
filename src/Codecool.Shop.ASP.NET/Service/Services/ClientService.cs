using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class ClientService : IClientService
{
    public IUnitOfWork UnitOfWork { private get; init; }

    public Client GetClient(string userId)
        => UnitOfWork.Clients.Get(x => x.UserId == userId);

    public void AddClient(Client client)
        => UnitOfWork.Clients.Add(client);
}