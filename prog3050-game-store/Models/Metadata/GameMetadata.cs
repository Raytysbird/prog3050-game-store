using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    [ModelMetadataType(typeof(GameMetadata))]
    public partial class Game
    {
       
    }
    public class GameMetadata
    {
        public int GameId { get; set; }

        [Display(Name="Game Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public IFormFile GameImage { get; set; }



        public ICollection<GameCategory> GameCategory { get; set; }
        public ICollection<GamePlatform> GamePlatform { get; set; }
    }
}
