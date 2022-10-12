using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class FavouritePlatform
    {
        public int PlatformId { get; set; }
        public string UserId { get; set; }

        public Platform Platform { get; set; }
        public AspNetUsers User { get; set; }
    }
}
