using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class GameCategory
    {
        public int CategoryId { get; set; }
        public int GameId { get; set; }

        public Category Category { get; set; }
        public Game Game { get; set; }
    }
}
