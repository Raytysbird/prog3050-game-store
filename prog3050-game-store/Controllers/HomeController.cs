using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using prog3050_game_store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly GameContext _context;
        public HomeController(GameContext context,UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var id = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            if (id!=null)
            {
                var wishList = _context.Wishlist.FirstOrDefault(x => x.UserId == id);

                if (wishList == null)
                {
                    Wishlist wishlist = new Wishlist();
                    wishlist.UserId = id;
                    _context.Wishlist.Add(wishlist);
                    _context.SaveChanges();
                }
            }
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
       
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
