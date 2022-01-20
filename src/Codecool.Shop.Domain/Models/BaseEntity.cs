using System.ComponentModel.DataAnnotations;

namespace Codecool.Shop.Domain.Models;

public abstract class BaseEntity
{
    [Required] [Key]
    public int Id { get; set; }

}