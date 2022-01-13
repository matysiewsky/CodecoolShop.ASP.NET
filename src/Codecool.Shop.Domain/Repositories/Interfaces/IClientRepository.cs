using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IClientRepository: IRepository<Client>
{
    public Client Get(string userId);
}