﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameStore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class EmailSent : PageModel
    {
        public void OnGet()
        {

        }
    }
}
