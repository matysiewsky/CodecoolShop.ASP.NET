using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.DbContext;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoMemory : ISupplierDao
    {
        // private List<Supplier> _appDbContext = new List<Supplier>();
        // private static SupplierDaoMemory instance = null;
        //
        // private SupplierDaoMemory()
        // {
        // }
        //
        // public static SupplierDaoMemory GetInstance()
        // {
        //     if (instance == null)
        //     {
        //         instance = new SupplierDaoMemory();
        //     }
        //
        //     return instance;
        // }
        private readonly AppDbContext _appDbContext;

        public SupplierDaoMemory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Supplier item)
        {
            // item.Id = _appDbContext.Count + 1;
            _appDbContext.Suppliers.Add(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(Supplier item)
        {
            _appDbContext.Suppliers.Remove(item);
            _appDbContext.SaveChanges();
        }

        public Supplier Get(int id)
        {
            return _appDbContext.Suppliers.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _appDbContext.Suppliers;
        }

    }
}
