using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.Service.Interfaces;

public interface ISupplierService
{
    Supplier GetSupplier(int id);
    IEnumerable<Supplier> GetAllSuppliers();
}