using System.Collections.Generic;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class SupplierService : ISupplierService
{
    private IUnitOfWork UnitOfWork { get; }

    public SupplierService(IUnitOfWork unitOfWork)
        => UnitOfWork = unitOfWork;


    public Supplier GetSupplier(int id)
        => UnitOfWork.Suppliers.Get(x => x.Id == id);
    public IEnumerable<Supplier> GetAllSuppliers()
        => UnitOfWork.Suppliers.GetAll();
}