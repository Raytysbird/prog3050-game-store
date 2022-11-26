using GameStore.CustomValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class User:IdentityUser
    {
        //[Required]
        //[Display(Name = "Id")]
        //public string Id { get; set; }

        

        [Required]
        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        [FutureDateValidation]
        public DateTime dob { get; set; }

        [Display(Name = "I consent for receiving promotional emails from CVGS")]
        [Required]
        public bool receive_promotions { get; set; }

    }

   
}
