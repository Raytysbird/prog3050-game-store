using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Controllers
{
    [Authorize(Roles = "Employee")]
    public class AdminController : Controller
    {
        private readonly GameContext _context;

        public AdminController(GameContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.AspNetUsers.ToListAsync());
        }

        public async Task<IActionResult> Review()
        {
            ViewBag.AdminType = "Review";
            var pendingReviews = _context.Review.Where(x => x.IsApproved == false);
            return View(pendingReviews);
        }

        // GET: Admin/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var aspNetUsers = await _context.AspNetUsers
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (aspNetUsers == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aspNetUsers);
        //}

        //// GET: Admin/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,Address,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,FirstName,LastName,Gender,Dob,ReceivePromotions")] AspNetUsers aspNetUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(aspNetUsers);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(aspNetUsers);
        //}

        //// GET: Admin/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var aspNetUsers = await _context.AspNetUsers.FindAsync(id);
        //    if (aspNetUsers == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(aspNetUsers);
        //}

        //// POST: Admin/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,Address,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,FirstName,LastName,Gender,Dob,ReceivePromotions")] AspNetUsers aspNetUsers)
        //{
        //    if (id != aspNetUsers.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(aspNetUsers);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AspNetUsersExists(aspNetUsers.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(aspNetUsers);
        //}

        //// GET: Admin/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var aspNetUsers = await _context.AspNetUsers
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (aspNetUsers == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aspNetUsers);
        //}

        //// POST: Admin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var aspNetUsers = await _context.AspNetUsers.FindAsync(id);
        //    _context.AspNetUsers.Remove(aspNetUsers);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AspNetUsersExists(string id)
        //{
        //    return _context.AspNetUsers.Any(e => e.Id == id);
        //}
    }
}
