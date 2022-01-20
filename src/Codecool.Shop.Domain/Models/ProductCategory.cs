namespace Codecool.Shop.Domain.Models;

public class ProductCategory: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; }
    public string Department { get; set; }

    public override bool Equals(object obj)
    {
        return Equals(obj as ProductCategory);
    }

    public bool Equals(ProductCategory obj)
    {
        return obj != null && obj.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}