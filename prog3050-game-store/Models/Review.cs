using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string AspUserId { get; set; }
        public string Title { get; set; }
        public string Review1 { get; set; }
        public int? Rating { get; set; }
        public int? GameId { get; set; }

        public AspNetUsers AspUser { get; set; }
        public Game Game { get; set; }
    }
}
