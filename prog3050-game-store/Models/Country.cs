﻿using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Country
    {
        public Country()
        {
            Province = new HashSet<Province>();
        }

        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string PostalPattern { get; set; }
        public string PhonePattern { get; set; }

        public ICollection<Province> Province { get; set; }
    }
}
