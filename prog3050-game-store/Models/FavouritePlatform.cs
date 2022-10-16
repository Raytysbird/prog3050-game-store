using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public partial class FavouritePlatform
    {
        [Display(Name = "Platforms")]
        public int PlatformId { get; set; }
        public string UserId { get; set; }

        public Platform Platform { get; set; }
        public AspNetUsers User { get; set; }
    }
}
