﻿using System;
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

            var id = _userManager.GetUserId(HttpContext.User);
            var favCategory = await _context.FavouriteCategory.Include(x => x.Category).Where(x => x.UserId == id).ToListAsync();
            ViewBag.FavoriteCategories = favCategory;
            return View(favCategory);
        }
      
        public IActionResult Create()
        {
            ViewData["UserId"] = _userManager.GetUserId(HttpContext.User);
            var user_id = _userManager.GetUserId(HttpContext.User);
            var currentCategory = _context.FavouriteCategory.Where(c=>c.UserId==user_id).Select(x => x.CategoryId).ToList();
            var categoryExcludingCurrent = _context.Category.Where(x => !currentCategory.Contains(x.CategoryId)).ToList();
            if (categoryExcludingCurrent.Count==0)
            {
                TempData["message"] = "No more categories available to select. Looks like you love all categories we have!!";
            }
            ViewData["Category"] = new SelectList(categoryExcludingCurrent, "CategoryId", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,UserId")] FavouriteCategory favouriteCategory)
        {
            if (ModelState.IsValid)
            {
                favouriteCategory.UserId = _userManager.GetUserId(HttpContext.User);
                TempData["message"] = "Category added to your favorites";
                _context.Add(favouriteCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "FavouritePlatforms");
            }
            return RedirectToAction("Index", "FavouritePlatforms");
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var favCategory = await _context.FavouriteCategory.Where(X => X.CategoryId == id).FirstOrDefaultAsync(x => x.UserId == user_id);
            TempData["message"] = "Category removed from your favorites";
            _context.FavouriteCategory.Remove(favCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "FavouritePlatforms");
        }
        private bool FavouriteCategoryExists(int id)
        {
            return _context.FavouriteCategory.Any(e => e.CategoryId == id);
        }
    }
}
