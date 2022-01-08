using System;
using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        [Key]
        public int ItemId { get; set; }

        public string Name { get; set; }

        public int ShoppingCartId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

    }
}