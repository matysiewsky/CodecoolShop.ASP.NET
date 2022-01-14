using System.Collections.Generic;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class SupplierService : ISupplierService
{
    public IGenericDbRepository<Supplier> SupplierRepository { get; init; }

    public Supplier GetSupplier(int id)
        => SupplierRepository.Get(x => x.Id == id);
    public IEnumerable<Supplier> GetAllSuppliers()
        => SupplierRepository.GetAll();
}