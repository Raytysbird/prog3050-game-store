using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public partial class Merchandise
    {
        public Merchandise()
        {
            CartMerchandise = new HashSet<CartMerchandise>();
        }

        public int? GameId { get; set; }
        [Required]
        public int MerchandiseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }

        public Game Game { get; set; }
        public ICollection<CartMerchandise> CartMerchandise { get; set; }
    }
}
