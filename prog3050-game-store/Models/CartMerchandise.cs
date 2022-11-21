using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class CartMerchandise
    {
        public int CartId { get; set; }
        public int MerchandiseId { get; set; }

        public Cart Cart { get; set; }
        public Merchandise Merchandise { get; set; }
    }
}
