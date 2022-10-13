using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class User:IdentityUser
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public bool receive_promotions { get; set; }
    }
}
