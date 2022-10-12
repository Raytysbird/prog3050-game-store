using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class GamePlatform
    {
        public int PlatformId { get; set; }
        public int GameId { get; set; }

        public Game Game { get; set; }
        public Platform Platform { get; set; }
    }
}
