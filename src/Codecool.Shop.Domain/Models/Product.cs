namespace Codecool.Shop.Domain.Models;

public class Product: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public double DefaultPrice { get; set; }
    public ProductCategory ProductCategory { get; set; }

    public int ProductCategoryId {get; set; }
    public Supplier Supplier { get; set; }
    public int SupplierId {get; set; }
}