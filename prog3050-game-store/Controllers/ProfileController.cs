using Microsoft.AspNetCore.Mvc;
using prog3050_game_store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace prog3050_game_store.Controllers
{
    public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}
