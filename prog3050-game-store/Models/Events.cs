using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.CustomValidation;
namespace GameStore.Models
{
    public partial class Events
    {
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [PastDateValidation]
        public DateTime? StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
         [PastDateValidation]
        public DateTime? EndDate { get; set; }
    }
}
