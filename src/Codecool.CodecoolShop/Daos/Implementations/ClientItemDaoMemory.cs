using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.DbContext;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ClientItemDaoMemory: IClientDao
    {
        private readonly AppDbContext _appDbContext;

        public ClientItemDaoMemory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Client item)
        {
            // item.Id = _appDbContext.Count + 1;
            _appDbContext.Clients.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(Client item)
        {
            _appDbContext.Clients.Remove(item);
            _appDbContext.SaveChanges();
        }

        public Client Get(int id)
        {
            return _appDbContext.Clients.FirstOrDefault(x => x.ClientId == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _appDbContext.Clients;
        }
    }
}