using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public partial class FavouritePlatform
    {
        [Display(Name = "Platforms")]
        public int PlatformId { get; set; }
        [NotMapped]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public Platform Platform { get; set; }
        public AspNetUsers User { get; set; }
    }
}
