using System;
using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public Client Client {get; set;}
        public string ProductsIds { get; set; }
        public string Currency { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
    }
}