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
            var user_id = _userManager.GetUserId(HttpContext.User);
            var id = _userManager.GetUserId(HttpContext.User);
            var favPlatform = await _context.FavouritePlatform.Include(x => x.Platform).Where(x => x.UserId == id).ToListAsync();
            var favCategory = await _context.FavouriteCategory.Include(x => x.Category).Where(x => x.UserId == id).ToListAsync();
            ViewBag.FavouriteCategory = favCategory;
            ViewBag.FavouritePlatform = favPlatform;
            var currentCategory = _context.FavouriteCategory.Where(c => c.UserId == user_id).Select(x => x.CategoryId).ToList();
            var categoryExcludingCurrent = _context.Category.Where(x => !currentCategory.Contains(x.CategoryId)).ToList();
            if (categoryExcludingCurrent.Count == 0)
            {
                TempData["category"] = "No more categories available to select. Looks like you love all categories we have!!";
            }
            var currentPlatform = _context.FavouritePlatform.Where(c => c.UserId == user_id).Select(x => x.PlatformId).ToList();
            var platformExcludingCurrent = _context.Platform.Where(x => !currentPlatform.Contains(x.PlatforrmId)).ToList();
            if (platformExcludingCurrent.Count == 0)
            {
                TempData["platform"] = "No more platforms available to select. Looks like you love all platforms we have!!";
            }
            ViewData["Platform"] = new SelectList(platformExcludingCurrent, "PlatforrmId", "Name");
            ViewData["Category"] = new SelectList(categoryExcludingCurrent, "CategoryId", "Name");
            return View();
        }
       
        public IActionResult Create()
        {
            ViewData["UserId"] = _userManager.GetUserId(HttpContext.User);
            var user_id = _userManager.GetUserId(HttpContext.User);
            var currentPlatform = _context.FavouritePlatform.Where(c => c.UserId == user_id).Select(x => x.PlatformId).ToList();
            var platformExcludingCurrent = _context.Platform.Where(x => !currentPlatform.Contains(x.PlatforrmId)).ToList();
            if (platformExcludingCurrent.Count == 0)
            {
                TempData["message"] = "No more platforms available to select. Looks like you love all platforms we have!!";
            }
            ViewData["Platform"] = new SelectList(platformExcludingCurrent, "PlatforrmId", "Name");

            return View();
        }
        // POST: FavouritePlatforms
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlatformId,UserId")] FavouritePlatform favouritePlatform)
        {
            if (ModelState.IsValid)
            {
                favouritePlatform.UserId = _userManager.GetUserId(HttpContext.User);
                TempData["message"] = "Platform added to your favorites";
                _context.Add(favouritePlatform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favouritePlatform);
        }
        

        // POST: CreditCardInfo/Delete/5
       
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var favPlatform = await _context.FavouritePlatform.Where(X => X.PlatformId == id).FirstOrDefaultAsync(x => x.UserId == user_id);
            TempData["message"] = "Platform removed from your favorites";
            _context.FavouritePlatform.Remove(favPlatform);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool FavouriteCategoryExists(int id)
        {
            return _context.FavouriteCategory.Any(e => e.CategoryId == id);
        }

        private bool FavouritePlatformExists(int id)
        {
            return _context.FavouritePlatform.Any(e => e.PlatformId == id);
        }
    }
}
