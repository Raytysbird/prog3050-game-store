using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartGame = new HashSet<CartGame>();
            CartMerchandise = new HashSet<CartMerchandise>();
        }

        public int CartId { get; set; }
        public int CreditCardId { get; set; }
        public float? TotalCost { get; set; }
        public string StateOfOrder { get; set; }
        public string UserId { get; set; }

        public CreditCardInfo CreditCard { get; set; }
        public AspNetUsers User { get; set; }
        public ICollection<CartGame> CartGame { get; set; }
        public ICollection<CartMerchandise> CartMerchandise { get; set; }
    }
}
