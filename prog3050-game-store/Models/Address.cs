using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "Appartment Number")]
        public string AptNumber { get; set; }
        [Display(Name = "Unit Number")]
        public string UnitNumber { get; set; }

        public string Building { get; set; }
        [Display(Name = "Do you have different shipping address?")]
        public bool IsShipping { get; set; }
        public string UserId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"([A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d)", ErrorMessage = "Please Enter postal code in A1A 1A1 format")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        public string Province { get; set; }

        public AspNetUsers User { get; set; }

        [NotMapped]
        public string FullAddress { get; set; }


    }
}