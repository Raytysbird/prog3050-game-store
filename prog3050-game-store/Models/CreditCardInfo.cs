using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class CreditCardInfo
    {
        public int CreditCardId { get; set; }
        public string UserId { get; set; }
        public int? Number { get; set; }
        public string ExpDate { get; set; }
        public int? Ccc { get; set; }

        public AspNetUsers User { get; set; }
    }
}
