using System;
using System.Collections.Generic;
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
using ClosedXML.Excel;

namespace GameStore.Controllers
{
    public class MerchandiseController : Controller
    {
        private readonly GameContext _context;
        private readonly IHostingEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;

        public MerchandiseController(GameContext context, IHostingEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        // GET: Merchandise
        public async Task<IActionResult> Index(int? id, string keyword, int pg = 1)
        {
            ViewBag.GameId = new SelectList(_context.Game, "GameId", "Name", id);
            int pageSize = 9;
            if (pg < 1)
            {
                pg = 1;
            }
            int rescCount = _context.Merchandise.Count();
            var pager = new Pagination(rescCount, pg, pageSize);
            int rescSkip = (pg - 1) * pageSize;
            this.ViewBag.Pager = pager;
            ViewBag.Keyword = keyword;

            if (id == null && keyword == null)
            {
                return View(await _context.Merchandise.Include(x => x.Game).Skip(rescSkip).Take(pager.PageSize).ToListAsync());
                //return View(await _context.Merchandise.Include(x => x.Game).ToListAsync());
            }
            else if(id != null && keyword == null)
            {
                return View(await _context.Merchandise.Include(x=> x.Game).Where(x=> x.GameId == id).Skip(rescSkip).Take(pager.PageSize).ToListAsync());
            }
            else if(id == null && keyword != null){
                var merch = await _context.Merchandise.Skip(rescSkip).Take(pager.PageSize).Where(x => x.Name.Contains(keyword)).ToListAsync();
                return View(merch);
            }

            return View(await _context.Merchandise.Include(x => x.Game).Where(x => x.GameId == id && x.Name.Contains(keyword)).Skip(rescSkip).Take(pager.PageSize).ToListAsync());
            
            //return View(merch);



            
            //if (keyword != null)
            //{
            //    var games = await _context.Merchandise.Skip(rescSkip).Take(pager.PageSize).Where(x => x.Name.Contains(keyword)).ToListAsync();
            //    return View(games);
            //}
            //return View(await _context.Merchandise.Skip(rescSkip).Take(pager.PageSize).ToListAsync());

        }




        // GET: Merchandise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            if (id == null)
            {
                return NotFound();
            }

            var merchandise = await _context.Merchandise
                .FirstOrDefaultAsync(m => m.MerchandiseId == id);
            var merchId = await _context.WishlistItem.Include(x => x.Wishlist).Where(x => x.Wishlist.UserId == user_id).Where(x => x.MerchandiseId == id).ToListAsync();

            var cartMerchId = await _context.CartMerchandise.Include(x => x.Cart).Where(x => x.Cart.UserId == user_id).Where(x => x.MerchandiseId == id).ToListAsync();

            if (cartMerchId.Count != 0)
            {
                foreach (var item in cartMerchId)
                {
                    if (item.MerchandiseId == id)
                    {
                        ViewBag.IsInCartList = true;
                    }

                }
            }
            else
            {
                ViewBag.IsInCartList = false;
            }


            if (merchId.Count != 0)
            {
                foreach (var item in merchId)
                {
                    if (item.GameId == id)
                    {
                        ViewBag.IsInWishList = true;
                    }

                }
            }
            else
            {
                ViewBag.IsInWishList = false;
            }
            if (merchandise == null)
            {
                return NotFound();
            }

            return View(merchandise);
        }

        // GET: Merchandise/Create
        public IActionResult Create()
        {
            ViewBag.GameId = new SelectList(_context.Game, "GameId", "Name");
            return View();
        }

        public IActionResult test(int id)
        {
            var a = "";
            return View();

        }

        // POST: Merchandise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MerchandiseId,Name,Description,Price, GameId")] Merchandise merchandise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merchandise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(merchandise);
        }

        // GET: Merchandise/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchandise = await _context.Merchandise.FindAsync(id);
            if (merchandise == null)
            {
                return NotFound();
            }
            ViewBag.GameId = new SelectList(_context.Game, "GameId", "Name");
            return View(merchandise);
        }

        // POST: Merchandise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MerchandiseId,Name,Description,Price,GameId")] Merchandise merchandise)
        {
            if (id != merchandise.MerchandiseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merchandise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchandiseExists(merchandise.MerchandiseId))
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
            return View(merchandise);
        }

      

        // POST: Merchandise/Delete/5
      
        public async Task<IActionResult> Delete(int id)
        {
            var merchandise = await _context.Merchandise.FindAsync(id);
            _context.Merchandise.Remove(merchandise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchandiseExists(int id)
        {
            return _context.Merchandise.Any(e => e.MerchandiseId == id);
        }
    }
}
