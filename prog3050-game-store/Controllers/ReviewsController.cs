using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ReviewsController(GameContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int id)
        {
            var gameContext = _context.Review.Include(r => r.AspUser).Where(x=> x.GameId == id);
            return View(await gameContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.AspUser)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int id)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            ViewData["GameTitle"] = _context.Game.Where(x => x.GameId == id).Select(x => x.Name).FirstOrDefault();
            var gameReviewed = _context.Review.Include(r => r.AspUser).Where(x => x.GameId == id && x.AspUserId == user).FirstOrDefault();
            if (gameReviewed != null)
            {
                TempData["message"] = "Game Already Reviewed";
                return RedirectToAction("Details", "Game", new { id }); 
            }
            ViewData["gameId"] = id;
            ViewData["AspUserId"] = user;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,AspUserId,Title,Review1,Rating,GameId")] Review review)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {

                review.IsApproved = String.IsNullOrEmpty(review.Title) && String.IsNullOrEmpty(review.Review1);
                
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Game", new { id = review.GameId });
            }
            ViewData["AspUserId"] = user;
            ViewData["gameId"] = review.GameId;
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["AspUserId"] = user;
            ViewData["gameId"] = id;
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AspUserId,Title,Review1,Rating,GameId")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            ViewData["AspUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", review.AspUserId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.AspUser)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.ReviewId == id);
        }
    }
}
