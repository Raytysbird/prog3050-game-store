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
    public class AddressController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public AddressController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            //string u= Request.Headers["Referer"].ToString();
            string fullAddress = "";
            var currentUser = _userManager.GetUserId(HttpContext.User);
            var mailingAddress = await _context.Address.Include(a => a.User).Where(x => x.UserId == currentUser).Where(c=>c.IsShipping==false).ToListAsync();
           
            if (mailingAddress.Count != 0)
            {
                
                foreach (var item in mailingAddress)
                {
                   
                     fullAddress = string.Join(",", new string[] { item.StreetAddress, item.Building, item.AptNumber, item.UnitNumber }.Where(c => !string.IsNullOrEmpty(c)));
                    item.FullAddress = fullAddress;
                    
                }
                ViewBag.MailAddress = mailingAddress;
                
            }
            else
            {
                ViewBag.MailAddress = null;
            }
                

            var shippingAddress = await _context.Address.Include(a => a.User).Where(x => x.UserId == currentUser).Where(c => c.IsShipping == true).ToListAsync();
            if (shippingAddress.Count != 0)
            {
                foreach (var item in shippingAddress)
                {

                    fullAddress = string.Join(",", new string[] { item.StreetAddress, item.Building, item.AptNumber, item.UnitNumber }.Where(c => !string.IsNullOrEmpty(c)));
                    item.FullAddress = fullAddress;

                }
                ViewBag.ShippingAddress = shippingAddress;
            }
            else
            {
                ViewBag.ShippingAddress = null;
            }


            //if (mailingAddress.Count == 0)
            //{
            //    TempData["message"] = "You have not added any address information. Please add your address!!";
            //    return RedirectToAction("Create");
            //}
            if (mailingAddress.Count == 0)
            {
                TempData["message"] = "You have not added any address information. Please add your address!!";
            }
            return View();
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["ProvinceCode"] = new SelectList(_context.Province.Where(x => x.CountryCode == "CA"), "ProvinceCode", "ProvinceCode");
            return View();
        }
        public IActionResult CreateShipping()
        {
           
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["ProvinceCode"] = new SelectList(_context.Province.Where(x => x.CountryCode == "CA"), "ProvinceCode", "ProvinceCode");
           
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,StreetAddress,AptNumber,UnitNumber,Building,IsShipping,UserId,City,PostalCode,Province")] Address address)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(HttpContext.User);
                address.IsShipping = false;
                address.UserId = user;
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", address.UserId);
            ViewData["ProvinceCode"] = new SelectList(_context.Province.Where(x => x.CountryCode == "CA"), "ProvinceCode", "ProvinceCode");

            return View(address);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShipping([Bind("AddressId,StreetAddress,AptNumber,UnitNumber,Building,IsShipping,UserId,City,PostalCode,Province")] Address address)
        {
            if (ModelState.IsValid)
            {
                string u = Request.Headers["Referer"].ToString();
                var user = _userManager.GetUserId(HttpContext.User);
                address.IsShipping = true;
                address.UserId = user;
                _context.Add(address);
                await _context.SaveChangesAsync();
                if (u.Contains("/Cart"))
                {
                    return RedirectToAction("Index", "Cart");
                }
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", address.UserId);
            ViewData["ProvinceCode"] = new SelectList(_context.Province.Where(x => x.CountryCode == "CA"), "ProvinceCode", "ProvinceCode");

            return View(address);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", address.UserId);
            ViewData["ProvinceCode"] = new SelectList(_context.Province.Where(x => x.CountryCode == "CA"), "ProvinceCode", "ProvinceCode");
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,StreetAddress,AptNumber,UnitNumber,Building,IsShipping,UserId,City,PostalCode,Province")] Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", address.UserId);
            ViewData["ProvinceCode"] = new SelectList(_context.Province.Where(x => x.CountryCode == "CA"), "ProvinceCode", "ProvinceCode");
            return View(address);
        }

        // GET: Address/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var address = await _context.Address
        //        .Include(a => a.User)
        //        .FirstOrDefaultAsync(m => m.AddressId == id);
        //    if (address == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(address);
        //}

        // POST: Address/Delete/5
       
        public async Task<IActionResult> Delete(int id)
        {
            var address = await _context.Address.FindAsync(id);
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.AddressId == id);
        }
    }
}
