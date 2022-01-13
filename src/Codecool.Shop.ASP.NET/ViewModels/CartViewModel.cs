using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.ViewModels;

public class CartViewModel
{
    public Cart Cart { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }


}