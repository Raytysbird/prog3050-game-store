using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Category
    {
        public Category()
        {
            FavouriteCategory = new HashSet<FavouriteCategory>();
            GameCategory = new HashSet<GameCategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<FavouriteCategory> FavouriteCategory { get; set; }
        public ICollection<GameCategory> GameCategory { get; set; }
    }
}
