using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using ClosedXML.Excel;
using System.IO;

namespace GameStore.Controllers
{
    public class WishlistController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public WishlistController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(HttpContext.User);
            var wishList = _context.Wishlist.FirstOrDefault(x => x.UserId == id);
           
            //if (wishList == null)
            //{
            //    Wishlist wishlist = new Wishlist();
            //    wishlist.UserId = id;
            //    _context.Wishlist.Add(wishlist);
            //    _context.SaveChanges();
            //}
            var wishListItem = _context.WishlistItem.Where(x => x.WishlistId == wishList.WishlistId).Select(x => x.GameId).ToList();
            if (wishListItem == null)
            {
                TempData["message"] = "No Game added to Wish List Right now";
            }

            else
            {
                var gameName = _context.Game.Where(x => wishListItem.Contains(x.GameId)).ToList();

                ViewBag.WishList = gameName;
            }
           
            //  var gameName = _context.Game.Where(x=>x.GameId == wishList.I)

            return View();
        }
        public IActionResult PrintMemberWishList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("WishList");
                var currentRow = 1;
                int count = 0;
                worksheet.Cell(currentRow, 1).Value = "User Name";
                worksheet.Cell(currentRow, 2).Value = "Game";

                var wishlist = _context.Wishlist.Include(x => x.User);


                foreach (var item in wishlist)
                {
                    var userName = item.User.UserName;
                    var userWish = _context.WishlistItem.Include(x=>x.Game).Where(x => x.WishlistId == item.WishlistId);
                    count = 0;
                    foreach (var wish in userWish)
                    {
                        currentRow++;
                        count++;
                        if (count==1)
                        {
                            worksheet.Cell(currentRow, 1).Value = userName;

                        }
                        worksheet.Cell(currentRow, 2).Value = wish.Game.Name; 
                    }
                }
                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "WishListDetails.xlsx"
                        );
                }
            }
        }
        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WishlistId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // GET: Wishlist/Create
        public IActionResult Create(int id)
        {
            ViewData["UserId"] = _userManager.GetUserId(HttpContext.User);

            var user_id = _userManager.GetUserId(HttpContext.User);

            var wishList1 = _context.Wishlist.FirstOrDefault(x => x.UserId == user_id);
            if (wishList1==null)
            {
                Wishlist wishlist = new Wishlist();
                wishlist.UserId = user_id;
                _context.Wishlist.Add(wishlist);
                _context.SaveChanges();
            }
            var wishList = _context.Wishlist.FirstOrDefault(x => x.UserId == user_id);
            WishlistItem wishlistItem = new WishlistItem();
            wishlistItem.GameId = id;
            wishlistItem.WishlistId = wishList.WishlistId;

            _context.WishlistItem.Add(wishlistItem);
            _context.SaveChanges();
            TempData["message"] = "Game Added to Wish List!";
            return RedirectToAction("Index");
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WishlistId,UserId")] Wishlist wishlist)
        {            
            if (ModelState.IsValid)
            {
                wishlist.UserId = _userManager.GetUserId(HttpContext.User);
                TempData["message"] = "Added to your wishlist";
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wishlist);
        }

    
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);

            var gameName = await _context.WishlistItem.FirstOrDefaultAsync(x => x.GameId == id);

            TempData["message"] = "Game removed from your wish list";
            _context.WishlistItem.Remove(gameName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
