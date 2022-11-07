using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public partial class Game
    {
        public Game()
        {
            GameCategory = new HashSet<GameCategory>();
            GamePlatform = new HashSet<GamePlatform>();
            Review = new HashSet<Review>();
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile GameImage { get; set; }
        public ICollection<GameCategory> GameCategory { get; set; }
        public ICollection<GamePlatform> GamePlatform { get; set; }
        public ICollection<Review> Review { get; set; }
    }
}
