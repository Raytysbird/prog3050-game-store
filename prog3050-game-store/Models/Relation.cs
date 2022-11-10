using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Relation
    {
        public int RelationId { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public bool? AreFriends { get; set; }

        public AspNetUsers FromUserNavigation { get; set; }
        public AspNetUsers ToUserNavigation { get; set; }
    }
}
