using System.ComponentModel.DataAnnotations;

namespace Codecool.Shop.Domain.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }

}