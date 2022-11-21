using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class CartGame
    {
        public int CartId { get; set; }
        public int GameId { get; set; }

        public Cart Cart { get; set; }
        public Game Game { get; set; }
    }
}
