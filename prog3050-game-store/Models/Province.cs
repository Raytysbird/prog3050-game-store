using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class Province
    {
        public string ProvinceCode { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string FirstPostalLetter { get; set; }

        public Country CountryCodeNavigation { get; set; }
    }
}
