using GameStore.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class GoogleCaptchaService
    {
        private RecaptchaSettings _settings;
        public GoogleCaptchaService(IOptions<RecaptchaSettings> settings)
        {
            _settings = settings.Value;
        }
        public virtual async Task<GoogleResponse> ValidateCaptcha(string _token)
        {
            GoogleCaptchaData keys = new GoogleCaptchaData
            {
                token = _token,
                secret = _settings.Recaptcha_SecretKey
            };
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={keys.secret}&response={keys.token}");
            var captchaResponse = JsonConvert.DeserializeObject<GoogleResponse>(response);
            return captchaResponse;
        }
    }
    public class GoogleCaptchaData
    {
        public string token { get; set; }
        public string secret { get; set; }
    }
    public class GoogleResponse
    {
        public bool success { get; set; }
    }
}
