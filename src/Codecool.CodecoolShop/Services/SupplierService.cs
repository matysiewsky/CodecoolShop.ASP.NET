using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class SupplierService
    {
        private readonly ISupplierDao _supplierDao;

        public SupplierService(ISupplierDao supplierDao)
        {
            _supplierDao = supplierDao;
        }

        public Supplier GetSupplier(int id)
        {
            return _supplierDao.Get(id);
        }
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _supplierDao.GetAll();
        }


    }
}