using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class WishlistItem
    {
        public int WishlistId { get; set; }
        public int GameId { get; set; }

        public Game Game { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
