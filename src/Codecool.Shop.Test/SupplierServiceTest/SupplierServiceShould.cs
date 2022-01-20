using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace Codecool.Shop.Test.SupplierServiceTest;

[TestFixture]
public class SupplierServiceShould
{
    [Test]
    public void ServiceGetsAll()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked.Setup(x => x.Suppliers.GetAll()).Returns(new Supplier[]
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
        unitOfWorkMocked.Setup(x => x.Suppliers.GetAll()).Returns(Array.Empty<Supplier>());
        ISupplierService sut = new SupplierService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetAllSuppliers().Count());
    }

    [Test]
    public void ServiceGetsOne()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked.Setup(x => x.Suppliers.Get(It.IsAny<Func<Supplier, bool>>())).Returns(new Supplier {Id = 1});
        ISupplierService sut = new SupplierService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(1, sut.GetSupplier(1).Id);
    }

    [Test]
    public void ServiceGetsOneWhenThereIsNoSuch()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked.Setup(x => x.Suppliers.Get(It.IsAny<Func<Supplier, bool>>())).Returns((Supplier)null!);
        ISupplierService sut = new SupplierService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.IsNull(sut.GetSupplier(1));
    }
}