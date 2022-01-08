using System.ComponentModel.DataAnnotations;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.ViewModels
{
    public class OrderViewModel
    {
        public OrderItem Order {get; set;}

        public Client ValidatorClientModel { get; set; }
        public CreditCardValidator ValidatorCreditCardModel { get; set; }

        public struct CreditCardValidator
        {
            [StringLength(100, MinimumLength = 10, ErrorMessage = "Please enter a valid Card Holder Name.")]
            public string CreditCardHolder { get; set; }
            [CreditCard(ErrorMessage = "Please enter a valid Card Number")]
            public string CreditCardNumber { get; set; }
            [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{4})", ErrorMessage = "Please enter a valid Card Number")]
            public string CreditCardExpiration { get; set; }
            [Range(100, 999, ErrorMessage = "Please enter a valid CVC number")]
            public string CreditCardCvc { get; set; }
        }

    }
}