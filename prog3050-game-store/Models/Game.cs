using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Game
    {
        public Game()
        {
            GameCategory = new HashSet<GameCategory>();
            GamePlatform = new HashSet<GamePlatform>();
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public ICollection<GameCategory> GameCategory { get; set; }
        public ICollection<GamePlatform> GamePlatform { get; set; }
    }
}
