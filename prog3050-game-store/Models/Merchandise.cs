using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Merchandise
    {
        public Merchandise()
        {
            CartMerchandise = new HashSet<CartMerchandise>();
            WishlistItem = new HashSet<WishlistItem>();
        }

        public int MerchandiseId { get; set; }
        public int? GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public Game Game { get; set; }
        public ICollection<CartMerchandise> CartMerchandise { get; set; }
        public ICollection<WishlistItem> WishlistItem { get; set; }
    }
}
