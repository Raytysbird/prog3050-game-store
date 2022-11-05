using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string AptNumber { get; set; }
        public string UnitNumber { get; set; }
        public string Building { get; set; }
        public bool? IsShipping { get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }

        public AspNetUsers User { get; set; }
    }
}
