using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GameStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string AspUserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Review")]
        public string Review1 { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Please select rating")]
        public int? Rating { get; set; }
        public int? GameId { get; set; }
        public bool? IsApproved { get; set; }
        public AspNetUsers AspUser { get; set; }
        public Game Game { get; set; }
    }
}
