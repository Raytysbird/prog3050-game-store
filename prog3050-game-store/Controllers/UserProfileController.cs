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


        // GET: UserProfile/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            id = _userManager.GetUserId(HttpContext.User);
            ViewBag.Gender = new List<string>() { "Male", "Female", "Other" };

            var provinces = _context.Province.Where(x => x.CountryCode == "CA").ToList();
            ViewData["ProvinceCode"] = new SelectList(provinces, "Name", "Name");

            

            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _userManager.FindByIdAsync(id);

            if (aspNetUsers == null)
            {
                return NotFound();
            }
            //_context.Entry(aspNetUsers).State = EntityState.Detached;
            return View(aspNetUsers);

        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,userName,first_name,last_name,gender,dob, address, city, province, postalCode,receive_promotions")] User aspNetUsers)
        {
            id = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(id);
            user.first_name = aspNetUsers.first_name;
            user.last_name = aspNetUsers.last_name;
            user.gender = aspNetUsers.gender;
            user.dob = aspNetUsers.dob;
            user.receive_promotions = aspNetUsers.receive_promotions;

            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _userManager.UpdateAsync(user);
                    TempData["message"] = "Your profile has been updated!!";


                }

                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction("Index");
            }
            ViewBag.Gender = new List<string>() { "Male", "Female", "Other" };
            return View(aspNetUsers);
        }
    }
}