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
    public class FavouriteCategoriesController : Controller
    {
        private readonly GameContext _context;

        private readonly UserManager<User> _userManager;

        public FavouriteCategoriesController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FavouriteCategories
        public async Task<IActionResult> Index()
        {
            //var gameContext = _context.FavouriteCategory.Include(f => f.Category).Include(f => f.User);
            var categories = _context.Category;
            var favCategory = await _context.FavouriteCategory.Include(x=> x.Category).FirstOrDefaultAsync();
            ViewData["FavouriteCategory"] = new SelectList(categories, "Name", "Name", favCategory.Category.Name);
            //ViewData["FavouriteCategory"] = new SelectList(categories, "CategoryId", "Name", favCategory.Category.Name);
            return View(favCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FavouriteCategory favouriteCategory)
        {
            var id = _userManager.GetUserId(HttpContext.User);
            var faveCategory = _context.FavouriteCategory.FirstOrDefault();
            var categoryId = _context.Category.Where(x => x.Name == favouriteCategory.Category.Name).FirstOrDefault().CategoryId;
            _context.FavouriteCategory.Remove(faveCategory);
            _context.SaveChanges();
            faveCategory.CategoryId = categoryId;
            faveCategory.UserId = id;
            if (id != favouriteCategory.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.FavouriteCategory.Add(faveCategory);
                    //_context.Update(favouriteCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavouriteCategoryExists(favouriteCategory.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["message"] = "Your profile has been updated!!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["FavouriteCategory"] = new SelectList(_context.Platform, "PlatforrmId", "Name", favouriteCategory.CategoryId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favouriteCategory.UserId);
            return View(favouriteCategory);
        }

        private bool FavouriteCategoryExists(int id)
        {
            return _context.FavouriteCategory.Any(e => e.CategoryId == id);
        }
    }
}
