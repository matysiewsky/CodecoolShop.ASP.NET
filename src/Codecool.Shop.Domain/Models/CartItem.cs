using System.ComponentModel.DataAnnotations;

namespace Codecool.Shop.Domain.Models;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    public int CartId { get; set; }
    public int Quantity { get; set; }
    public DateTime DateCreated { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }

}