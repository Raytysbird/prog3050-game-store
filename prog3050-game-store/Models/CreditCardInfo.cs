using GameStore.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public partial class CreditCardInfo
    {
        public CreditCardInfo()
        {
            Cart = new HashSet<Cart>();
        }

        [Required]
        public int CreditCardId { get; set; }
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Card Number")]
        [RegularExpression(@"([0-9]{4}\-[0-9]{4}\-[0-9]{4}\-[0-9]{4}$)", ErrorMessage = "Please enter 16 digit number in format XXXX-XXXX-XXXX-XXXX")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Expiry Date")]
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}$", ErrorMessage = "Please enter valid expiry date in format MM/YY")]
        [ExpiryDateAttribute]
        public string ExpDate { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "CCC is invalid. Must be of 3 digits")]
        [Display(Name = "CCC Number")]
        public int? Ccc { get; set; }

        public AspNetUsers User { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
}
