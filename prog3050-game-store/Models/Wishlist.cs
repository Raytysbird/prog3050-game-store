using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Wishlist
    {
        public Wishlist()
        {
            WishlistItem = new HashSet<WishlistItem>();
        }

        public int WishlistId { get; set; }
        public string UserId { get; set; }

        public AspNetUsers User { get; set; }
        public ICollection<WishlistItem> WishlistItem { get; set; }
    }
}
