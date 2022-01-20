namespace Codecool.Shop.Domain.Models;

public class Supplier: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public override string ToString()
    {
        return new string($"Id: {Id} Name: {Name} Description: {Description}");
    }
}