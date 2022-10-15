using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public partial class CreditCardInfo
    {
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
        public string ExpDate { get; set; }
        [Required]
        [Range(000,999, ErrorMessage = "CCC is invalid. Must be of 3 digits")]
        [Display(Name = "CCC Number")]
        public int? Ccc { get; set; }

        public AspNetUsers User { get; set; }
    }
}
