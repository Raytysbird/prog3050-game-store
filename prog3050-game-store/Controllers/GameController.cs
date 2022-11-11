using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Controllers
{
    public class GameController : Controller
    {
        private readonly GameContext _context;
        private readonly IHostingEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;

        public GameController(GameContext context, IHostingEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        // GET: Game
        public async Task<IActionResult> Index(string keyword,int pg=1)
        {
            int pageSize= 9;
            if (pg<1)
            {
                pg = 1;
            }
            int rescCount = _context.Game.Count();
            var pager = new Pagination(rescCount,pg,pageSize);
            int rescSkip = (pg - 1) * pageSize;
            this.ViewBag.Pager = pager;
            ViewBag.Keyword = keyword;
            if (keyword!=null)
            {
                var games = await _context.Game.Skip(rescSkip).Take(pager.PageSize).Where(x => x.Name.Contains(keyword)).ToListAsync();
                return View(games);
            }
            return View(await _context.Game.Skip(rescSkip).Take(pager.PageSize).ToListAsync());
        }
    
        // GET: Game/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            var approvedReviews = _context.Review.Include(x=> x.AspUser).Where(x=> x.GameId == id && x.IsApproved == true).ToList();
            var currentUserPendingReview = _context.Review.Include(x => x.AspUser).Where(x => x.GameId == id && x.IsApproved == false && x.AspUserId == user).ToList();
            var reviews = approvedReviews.Union(currentUserPendingReview);
            ViewBag.ReviewObj = reviews.ToList();
            var rating = _context.Review.Where(x => x.GameId == id).Average(x => x.Rating);
            if (rating.HasValue)
            {
                ViewBag.Rating = rating.Value;
            }

            if (id == null)
            {
                return NotFound();
            }
           
           var game = await _context.Game.Include(x=>x.GameCategory).FirstOrDefaultAsync(m => m.GameId == id);

            var category = _context.GameCategory.Include(x=>x.Category).FirstOrDefault(x => x.GameId == id);
            if (category!=null)
            {
                ViewBag.CategoryName = category.Category.Name;
            }
            

            var platform = _context.GamePlatform.Include(x => x.Platform).FirstOrDefault(x => x.GameId == id);
            if (platform!=null)
            {
                ViewBag.PlatformName = platform.Platform.Name;
            }
            if (game == null)
            {
                return NotFound();
            }


            return View(game);
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Name,Description,Price,GameImage")] Game game)
        {
            if (ModelState.IsValid)
            {
                if (game.GameImage != null)
                {
                    string folder = "/images/game/";
                    folder += Guid.NewGuid().ToString() + game.GameImage.FileName;
                    string serverFolder = _webHostEnvironment.WebRootPath + folder;
                    game.ImagePath = folder;
                    await game.GameImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                TempData["message"] = "Game added to inventory";
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,Name,Description,Price")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameId))
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
            return View(game);
        }

        // GET: Game/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var game = await _context.Game
        //        .FirstOrDefaultAsync(m => m.GameId == id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(game);
        //}

        // POST: Game/Delete/5
      
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
