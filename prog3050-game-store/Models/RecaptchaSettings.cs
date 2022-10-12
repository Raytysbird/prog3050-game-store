using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class RecaptchaSettings
    {
        public String Recaptcha_SiteKey { get; set; }
        public String Recaptcha_SecretKey { get; set; }
    }
}
