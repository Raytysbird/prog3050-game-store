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
    public class FavouritePlatformsController : Controller
    {
        private readonly GameContext _context;

        public FavouritePlatformsController(GameContext context)
        {
            _context = context;
        }

        // GET: FavouritePlatforms
        public async Task<IActionResult> Index()
        {
            var gameContext = _context.FavouritePlatform.Include(f => f.Platform).Include(f => f.User);
            var gameCategory = _context.Category.OrderBy(x => x.Name);
            ViewData["GameCategory"] = new SelectList(gameCategory, "Name", "Name");

            var platformCategory = _context.Platform.OrderBy(x => x.Name);
            //ViewData["PlatformCategory"] = new SelectList(platformCategory, "Name", "Name", gameContext.);
            return View(await gameContext.ToListAsync());
        }

        // GET: FavouritePlatforms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favouritePlatform = await _context.FavouritePlatform
                .Include(f => f.Platform)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.PlatformId == id);
            if (favouritePlatform == null)
            {
                return NotFound();
            }

            return View(favouritePlatform);
        }

        // GET: FavouritePlatforms/Create
        public IActionResult Create()
        {
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatforrmId", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: FavouritePlatforms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlatformId,UserId")] FavouritePlatform favouritePlatform)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favouritePlatform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatforrmId", "Name", favouritePlatform.PlatformId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favouritePlatform.UserId);
            return View(favouritePlatform);
        }

        // GET: FavouritePlatforms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favouritePlatform = await _context.FavouritePlatform.FindAsync(id);
            if (favouritePlatform == null)
            {
                return NotFound();
            }
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatforrmId", "Name", favouritePlatform.PlatformId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favouritePlatform.UserId);
            return View(favouritePlatform);
        }

        // POST: FavouritePlatforms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlatformId,UserId")] FavouritePlatform favouritePlatform)
        {
            if (id != favouritePlatform.PlatformId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favouritePlatform);
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

        // GET: FavouritePlatforms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favouritePlatform = await _context.FavouritePlatform
                .Include(f => f.Platform)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.PlatformId == id);
            if (favouritePlatform == null)
            {
                return NotFound();
            }

            return View(favouritePlatform);
        }

        // POST: FavouritePlatforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favouritePlatform = await _context.FavouritePlatform.FindAsync(id);
            _context.FavouritePlatform.Remove(favouritePlatform);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavouritePlatformExists(int id)
        {
            return _context.FavouritePlatform.Any(e => e.PlatformId == id);
        }
    }
}
