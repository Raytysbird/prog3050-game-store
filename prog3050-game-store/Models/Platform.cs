using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Platform
    {
        public Platform()
        {
            FavouritePlatform = new HashSet<FavouritePlatform>();
            GamePlatform = new HashSet<GamePlatform>();
        }

        public int PlatforrmId { get; set; }
        public string Name { get; set; }

        public ICollection<FavouritePlatform> FavouritePlatform { get; set; }
        public ICollection<GamePlatform> GamePlatform { get; set; }
    }
}
