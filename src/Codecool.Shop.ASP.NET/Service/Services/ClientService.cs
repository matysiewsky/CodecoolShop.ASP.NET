using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class ClientService : IClientService
{
    private IUnitOfWork UnitOfWork { get; }

    public ClientService(IUnitOfWork unitOfWork)
        => UnitOfWork = unitOfWork;


    public Client GetClient(string userId)
        => UnitOfWork.Clients.Get(x => x.UserId == userId);

    public void AddClient(Client client)
    {
        UnitOfWork.Clients.Add(client);
        UnitOfWork.Save();
    }
}