using System;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace Codecool.Shop.Test.Services.Supplier;

[TestFixture]
public class SupplierServiceShould
{
    [Test]
    public void ServiceGetsAll()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked.Setup(x => x.Suppliers.GetAll()).Returns(new Domain.Models.Supplier[]
        {
            new() {Id = 1},
            new() {Id = 2},
            new() {Id = 3}
        });
        ISupplierService sut = new SupplierService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(3, sut.GetAllSuppliers().Count());
    }

    [Test]
    public void ServiceGetsAllWhenCollectionEmpty()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked.Setup(x => x.Suppliers.GetAll()).Returns(Array.Empty<Domain.Models.Supplier>());
        ISupplierService sut = new SupplierService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetAllSuppliers().Count());
    }

    [Test]
    public void ServiceGetsOne()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked.Setup(x => x.Suppliers.Get(It.IsAny<Func<Domain.Models.Supplier, bool>>())).Returns(new Domain.Models.Supplier {Id = 1});
        ISupplierService sut = new SupplierService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(1, sut.GetSupplier(1).Id);
    }

    [Test]
    public void ServiceGetsOneWhenThereIsNoSuch()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked.Setup(x => x.Suppliers.Get(It.IsAny<Func<Domain.Models.Supplier, bool>>())).Returns((Domain.Models.Supplier)null!);
        ISupplierService sut = new SupplierService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.IsNull(sut.GetSupplier(1));
    }
}