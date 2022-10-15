using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;

namespace GameStore.Controllers
{
    public class FavouriteCategoriesController : Controller
    {
        private readonly GameContext _context;

        public FavouriteCategoriesController(GameContext context)
        {
            _context = context;
        }

        // GET: FavouriteCategories
        public async Task<IActionResult> Index()
        {
            var gameContext = _context.FavouriteCategory.Include(f => f.Category).Include(f => f.User);
            return View(await gameContext.ToListAsync());
        }

        // GET: FavouriteCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favouriteCategory = await _context.FavouriteCategory
                .Include(f => f.Category)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (favouriteCategory == null)
            {
                return NotFound();
            }

            return View(favouriteCategory);
        }

        // GET: FavouriteCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: FavouriteCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,UserId")] FavouriteCategory favouriteCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favouriteCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", favouriteCategory.CategoryId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favouriteCategory.UserId);
            return View(favouriteCategory);
        }

        // GET: FavouriteCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favouriteCategory = await _context.FavouriteCategory.FindAsync(id);
            if (favouriteCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", favouriteCategory.CategoryId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favouriteCategory.UserId);
            return View(favouriteCategory);
        }

        // POST: FavouriteCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,UserId")] FavouriteCategory favouriteCategory)
        {
            if (id != favouriteCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favouriteCategory);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", favouriteCategory.CategoryId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favouriteCategory.UserId);
            return View(favouriteCategory);
        }

        // GET: FavouriteCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favouriteCategory = await _context.FavouriteCategory
                .Include(f => f.Category)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (favouriteCategory == null)
            {
                return NotFound();
            }

            return View(favouriteCategory);
        }

        // POST: FavouriteCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favouriteCategory = await _context.FavouriteCategory.FindAsync(id);
            _context.FavouriteCategory.Remove(favouriteCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavouriteCategoryExists(int id)
        {
            return _context.FavouriteCategory.Any(e => e.CategoryId == id);
        }
    }
}
