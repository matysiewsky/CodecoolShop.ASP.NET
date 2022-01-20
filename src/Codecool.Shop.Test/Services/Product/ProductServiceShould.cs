using System;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace Codecool.Shop.Test.Services.Product;

[TestFixture]
public class ProductServiceShould
{
    [Test]
    public void ServiceGetsAllProducts()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Products.GetAll())
            .Returns(new Domain.Models.Product[]
                {
                    new() {Id = 1},
                    new() {Id = 2},
                    new() {Id = 3}
                });
        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(3, sut.GetAllProducts().Count());
    }

    [Test]
    public void ServiceGetsAllProductsWhenCollectionEmpty()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Products.GetAll())
            .Returns(Array.Empty<Domain.Models.Product>());
        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetAllProducts().Count());
    }

    [Test]
    public void ServiceGetsOneProduct()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Products.Get(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns(new Domain.Models.Product {Id = 1});
        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(1, sut.GetProduct(1).Id);
    }

    [Test]
    public void ServiceGetsOneProductWhenThereIsNoSuch()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Products.Get(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns((Domain.Models.Product)null!);
        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.IsNull(sut.GetProduct(1));
    }

    [Test]
    public void ServiceGetsAllProductCategories()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Categories.GetAll())
            .Returns(new ProductCategory[]
            {
                new() {Id = 1},
                new() {Id = 2},
                new() {Id = 3}
            });
        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(3, sut.GetAllProductCategories().Count());
    }

    [Test]
    public void ServiceGetsAllProductCategoriesWhenCollectionEmpty()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Categories.GetAll())
            .Returns(Array.Empty<ProductCategory>());
        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetAllProductCategories().Count());
    }

    [Test]
    public void ServiceGetsAllProductsForCategory()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Categories.Get(It.IsAny<Func<ProductCategory, bool>>()))
            .Returns(new ProductCategory {Id = 1});

        unitOfWorkMocked
            .Setup(x => x.Products.GetRange(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns(new Domain.Models.Product[]
                {
                    new() {Id = 1, ProductCategoryId = 1},
                    new() {Id = 2, ProductCategoryId = 1},
                    new() {Id = 3, ProductCategoryId = 1}
                });

        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(3, sut.GetProductsForCategory(1).Count());
    }

    [Test]
    public void ServiceGetsAllProductsForCategoryWhenCollectionEmpty()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Categories.Get(It.IsAny<Func<ProductCategory, bool>>()))
            .Returns(new ProductCategory {Id = 1});

        unitOfWorkMocked
            .Setup(x => x.Products.GetRange(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns(Array.Empty<Domain.Models.Product>);

        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetProductsForCategory(1).Count());
    }

    [Test]
    public void ServiceGetsAllProductsForCategoryWhenThereIsNoSuch()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Categories.Get(It.IsAny<Func<ProductCategory, bool>>()))
            .Returns((ProductCategory)null!);

        unitOfWorkMocked
            .Setup(x => x.Products.GetRange(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns(Array.Empty<Domain.Models.Product>);

        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetProductsForCategory(1).Count());
    }

    [Test]
    public void ServiceGetsProductsForSupplier()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Suppliers.Get(It.IsAny<Func<Domain.Models.Supplier, bool>>()))
            .Returns(new Domain.Models.Supplier {Id = 1});

        unitOfWorkMocked
            .Setup(x => x.Products.GetRange(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns(new Domain.Models.Product[]
                {
                    new() {Id = 1, SupplierId = 1},
                    new() {Id = 2, SupplierId = 1},
                    new() {Id = 3, SupplierId = 1}
                });

        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(3, sut.GetProductsForSupplier(1).Count());
    }

    [Test]
    public void ServiceGetsProductsForSupplierWhenCollectionEmpty()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Suppliers.Get(It.IsAny<Func<Domain.Models.Supplier, bool>>()))
            .Returns(new Domain.Models.Supplier {Id = 1});

        unitOfWorkMocked
            .Setup(x => x.Products.GetRange(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns(Array.Empty<Domain.Models.Product>);

        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetProductsForSupplier(1).Count());
    }

    [Test]
    public void ServiceGetsProductsForSupplierWhenThereIsNoSuch()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWorkMocked = new();
        unitOfWorkMocked
            .Setup(x => x.Suppliers.Get(It.IsAny<Func<Domain.Models.Supplier, bool>>()))
            .Returns((Domain.Models.Supplier)null!);

        unitOfWorkMocked
            .Setup(x => x.Products.GetRange(It.IsAny<Func<Domain.Models.Product, bool>>()))
            .Returns(Array.Empty<Domain.Models.Product>);

        IProductService sut = new ProductService(unitOfWorkMocked.Object);

        // Act & Assert
        Assert.AreEqual(0, sut.GetProductsForSupplier(1).Count());
    }
}