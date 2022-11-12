using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class UserEvent
    {
        public int Id { get; set; }
        public string AspUserId { get; set; }
        public int EventId { get; set; }

        public AspNetUsers AspUser { get; set; }
        public Events Event { get; set; }
    }
}
