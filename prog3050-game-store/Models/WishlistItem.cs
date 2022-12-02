using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class WishlistItem
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public int? GameId { get; set; }
        public int? MerchandiseId { get; set; }

        public Game Game { get; set; }
        public Merchandise Merchandise { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
