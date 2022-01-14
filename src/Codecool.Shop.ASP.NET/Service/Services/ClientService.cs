using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class ClientService : IClientService
{
    public IGenericDbRepository<Client> ClientRepository { get; init; }

    public Client GetClient(string userId)
        => ClientRepository.Get(x => x.UserId == userId);

    public void AddClient(Client client)
        => ClientRepository.Add(client);
}