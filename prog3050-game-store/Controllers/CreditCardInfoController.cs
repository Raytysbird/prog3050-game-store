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
    public class CreditCardInfoController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public CreditCardInfoController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CreditCardInfo
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var gameContext = _context.CreditCardInfo.Where(c => c.UserId == userId);
            return View(await gameContext.ToListAsync());
        }

        // GET: CreditCardInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCardInfo = await _context.CreditCardInfo
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CreditCardId == id);
            if (creditCardInfo == null)
            {
                return NotFound();
            }

            return View(creditCardInfo);
        }

        // GET: CreditCardInfo/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: CreditCardInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreditCardId,UserId,Number,ExpDate,Ccc")] CreditCardInfo creditCardInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditCardInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", creditCardInfo.UserId);
            return View(creditCardInfo);
        }

        // GET: CreditCardInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCardInfo = await _context.CreditCardInfo.FindAsync(id);
            if (creditCardInfo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", creditCardInfo.UserId);
            return View(creditCardInfo);
        }

        // POST: CreditCardInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreditCardId,UserId,Number,ExpDate,Ccc")] CreditCardInfo creditCardInfo)
        {
            if (id != creditCardInfo.CreditCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditCardInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditCardInfoExists(creditCardInfo.CreditCardId))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", creditCardInfo.UserId);
            return View(creditCardInfo);
        }

        // GET: CreditCardInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCardInfo = await _context.CreditCardInfo
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CreditCardId == id);
            if (creditCardInfo == null)
            {
                return NotFound();
            }

            return View(creditCardInfo);
        }

        // POST: CreditCardInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditCardInfo = await _context.CreditCardInfo.FindAsync(id);
            _context.CreditCardInfo.Remove(creditCardInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditCardInfoExists(int id)
        {
            return _context.CreditCardInfo.Any(e => e.CreditCardId == id);
        }
    }
}
