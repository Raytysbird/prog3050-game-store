using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public partial class FavouriteCategory
    {
        [Display(Name = "Categories")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public Category Category { get; set; }
        public AspNetUsers User { get; set; }
    }
}
