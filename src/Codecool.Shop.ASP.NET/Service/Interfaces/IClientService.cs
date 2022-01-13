using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.Service.Interfaces;

public interface IClientService
{
    Client GetClient(string userId);
    void AddClient(Client client);
}