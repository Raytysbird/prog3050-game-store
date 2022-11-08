using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;

using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace GameStore.Controllers
{
    public class RelationController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public RelationController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Relation
        public async Task<IActionResult> Index(string keyword)
        {
            if (keyword != null)
            {
                var user = _context.AspNetUsers.Where(x => x.UserName.Contains(keyword));
                ViewBag.Keyword = keyword;
                ViewBag.User = user;
                return View();

            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> FriendsList()
        {
            List<AspNetUsers> lstUserId = new List<AspNetUsers>();
            List<AspNetUsers> lstFriends = new List<AspNetUsers>();

            var currentUser = _userManager.GetUserId(HttpContext.User);
            var friendList = await _context.Relation.Where(x => (x.ToUser == currentUser || x.FromUser == currentUser)).Where(z => z.AreFriends == true).ToListAsync();
            foreach (var item in friendList)
            {
                if (item.FromUser != currentUser)
                {
                    AspNetUsers user = new AspNetUsers();
                    user.Id = item.FromUser;
                    lstUserId.Add(user);

                }
                if (item.ToUser != currentUser)
                {
                    AspNetUsers user = new AspNetUsers();
                    user.Id = item.ToUser;
                    lstUserId.Add(user);
                }
            }

            foreach (var item in lstUserId)
            {
                var userDetails = _context.AspNetUsers.FirstOrDefault(x => x.Id == item.Id);
                lstFriends.Add(userDetails);

            }
            ViewBag.Friends = lstFriends;
            return View("Index");
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

        public async Task<IActionResult> SendRequest([Bind("RelationId,FromUser,ToUser,AreFriends")] Relation relation, string id)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(HttpContext.User);
                relation.FromUser = _userManager.GetUserId(HttpContext.User);
                relation.ToUser = id;
                relation.AreFriends = false;
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
