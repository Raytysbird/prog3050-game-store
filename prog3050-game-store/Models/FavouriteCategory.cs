using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class FavouriteCategory
    {
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public Category Category { get; set; }
        public AspNetUsers User { get; set; }
    }
}
