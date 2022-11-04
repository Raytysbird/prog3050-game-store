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
    public class RelationController : Controller
    {
        private readonly GameContext _context;

        public RelationController(GameContext context)
        {
            _context = context;
        }

        // GET: Relation
        public async Task<IActionResult> Index()
        {
            var gameContext = _context.Relation.Include(r => r.FromUserNavigation).Include(r => r.ToUserNavigation);
            return View(await gameContext.ToListAsync());
        }

        // GET: Relation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relation
                .Include(r => r.FromUserNavigation)
                .Include(r => r.ToUserNavigation)
                .FirstOrDefaultAsync(m => m.RelationId == id);
            if (relation == null)
            {
                return NotFound();
            }

            return View(relation);
        }

        // GET: Relation/Create
        public IActionResult Create()
        {
            ViewData["FromUser"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["ToUser"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Relation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelationId,FromUser,ToUser,AreFriends")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.FromUser);
            ViewData["ToUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.ToUser);
            return View(relation);
        }

        // GET: Relation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relation.FindAsync(id);
            if (relation == null)
            {
                return NotFound();
            }
            ViewData["FromUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.FromUser);
            ViewData["ToUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.ToUser);
            return View(relation);
        }

        // POST: Relation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RelationId,FromUser,ToUser,AreFriends")] Relation relation)
        {
            if (id != relation.RelationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationExists(relation.RelationId))
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
            ViewData["FromUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.FromUser);
            ViewData["ToUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.ToUser);
            return View(relation);
        }

        // GET: Relation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relation
                .Include(r => r.FromUserNavigation)
                .Include(r => r.ToUserNavigation)
                .FirstOrDefaultAsync(m => m.RelationId == id);
            if (relation == null)
            {
                return NotFound();
            }

            return View(relation);
        }

        // POST: Relation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relation = await _context.Relation.FindAsync(id);
            _context.Relation.Remove(relation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationExists(int id)
        {
            return _context.Relation.Any(e => e.RelationId == id);
        }
    }
}
