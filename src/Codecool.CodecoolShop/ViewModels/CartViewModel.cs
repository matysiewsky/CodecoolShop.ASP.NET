using System.Collections.Generic;
using Codecool.CodecoolShop.Logic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.ViewModels
{
    public class CartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }


    }
}