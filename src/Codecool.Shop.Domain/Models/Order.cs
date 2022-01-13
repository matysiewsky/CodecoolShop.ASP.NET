using System.ComponentModel.DataAnnotations;

namespace Codecool.Shop.Domain.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string UserId { get; set; }
    public Client Client {get; set;}
    public string ProductsIds { get; set; }
    public string Currency { get; set; }
    public double TotalPrice { get; set; }
    public bool IsPaid { get; set; }
}