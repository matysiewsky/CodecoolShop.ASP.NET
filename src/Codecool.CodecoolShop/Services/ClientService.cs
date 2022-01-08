using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ClientService
    {
        private readonly IClientDao _clientDao;

        public ClientService(IClientDao clientDao)
        {
            _clientDao = clientDao;
        }

        public void AddClient(Client client) => _clientDao.Add(client);
    }
}