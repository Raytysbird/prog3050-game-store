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
    public class UserProfileController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public UserProfileController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserProfile
        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            var userId= _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            return View(user);
        }

        // GET: UserProfile/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // GET: UserProfile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUsers);
        }

        // GET: UserProfile/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
           ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
           id = _userManager.GetUserId(HttpContext.User);
           ViewBag.Gender = new List<string>() {"Male","Female","Other" };
           
           var provinces = _context.Province.Where(x => x.CountryCode == "CA").ToList();
           ViewData["ProvinceCode"] = new SelectList(provinces, "Name", "Name");

            var gameCategory = _context.Category.OrderBy(x=>x.Name);
            ViewData["GameCategory"] = new SelectList(gameCategory, "Name", "Name");

            var platformCategory = _context.Platform.OrderBy(x => x.Name);
            ViewData["PlatformCategory"] = new SelectList(platformCategory, "Name", "Name");

            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _userManager.FindByIdAsync(id);
           
            //ViewData["CountryCode"] = new SelectList(_context.Country.OrderBy(g => g.Name), "Name", "Name", _context.Province.Select(x => x.CountryCode));
            if (aspNetUsers == null)
            {
                return NotFound();
            }
            return View(aspNetUsers);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,userName,first_name,last_name,gender,dob, address, city, province, postalCode,receive_promotions")] User aspNetUsers)
        {
            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var stamp = _userManager.GetSecurityStampAsync(aspNetUsers);
                    aspNetUsers.SecurityStamp = stamp.ToString();

                    IdentityResult result = await _userManager.UpdateAsync(aspNetUsers);

                    //_context.Update(aspNetUsers);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUsersExists(aspNetUsers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Gender = new List<string>() { "Male", "Female", "Other" };
            var provinces = _context.Province.Where(x => x.CountryCode == "CA").ToList();
            ViewData["ProvinceCode"] = new SelectList(provinces, "Name", "Name");
            return View(aspNetUsers);
        }

        // GET: UserProfile/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // POST: UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUsers = await _context.AspNetUsers.FindAsync(id);
            _context.AspNetUsers.Remove(aspNetUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
