using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace GameStore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly GoogleCaptchaService _googleCaptchaService;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly EmailSender _sender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            GoogleCaptchaService googleCaptchaService, 
            EmailSender sender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _googleCaptchaService = googleCaptchaService;
            _emailSender = emailSender;
            _sender = sender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            
            [Display(Name = "First Name")]
            public string FistName { get; set; }

            
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }


            [Required(ErrorMessage = "Please check recaptcha.")]
            [NotMapped]
            public string Token { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            
            if (ModelState.IsValid)
            {
                var captchaResult = _googleCaptchaService.ValidateCaptcha(Input.Token);
                if (captchaResult.Result.success)
                {
                    var user = new User { UserName = Input.UserName, Email = Input.Email };
                    user.dob = DateTime.Today;
                    var userExists = await _userManager.FindByEmailAsync(Input.Email);
                    if (userExists!=null)
                    {
                        TempData["message"] = "User alredy exist with same email.";
                        
                        return RedirectToPage("./Register");
                    }
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _sender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");

                        //TempData["message"] = "Thank you for joining. Please confirm your email, We have sent you email with link.";
                        return RedirectToPage("./EmailSent");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        


    }
}
