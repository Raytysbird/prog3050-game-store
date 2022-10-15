using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Controllers
{
    public class FavouritePlatformsController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public FavouritePlatformsController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FavouritePlatforms
        public async Task<IActionResult> Index()
        {
            var userPlatfrom = await _context.FavouritePlatform.Include(x=>x.Platform).FirstOrDefaultAsync() ;
            var gamePlatfrom = _context.Platform.OrderBy(x => x.Name);
            ViewData["PlatformId"] = new SelectList(gamePlatfrom, "Name", "Name", userPlatfrom.Platform.Name);
            return View(userPlatfrom);
        }

        // POST: FavouritePlatforms
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FavouritePlatform favouritePlatform)
        {
            var id = _userManager.GetUserId(HttpContext.User);
            var favePlatform = _context.FavouritePlatform.FirstOrDefault();
            var platformId = _context.Platform.Where(x => x.Name == favouritePlatform.Platform.Name).FirstOrDefault().PlatforrmId;
            _context.FavouritePlatform.Remove(favePlatform);
            _context.SaveChanges();
            favePlatform.PlatformId = platformId;
            favePlatform.UserId = id;
            if (id != favouritePlatform.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.FavouritePlatform.Add(favePlatform);
                    //_context.Update(favouritePlatform);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavouritePlatformExists(favouritePlatform.PlatformId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatforrmId", "Name", favouritePlatform.PlatformId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favouritePlatform.UserId);
            return View(favouritePlatform);
        }

      

        private bool FavouritePlatformExists(int id)
        {
            return _context.FavouritePlatform.Any(e => e.PlatformId == id);
        }
    }
}
