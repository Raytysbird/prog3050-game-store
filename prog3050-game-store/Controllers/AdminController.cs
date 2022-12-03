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
        public async Task<IActionResult> Reports()
        {
            return View();
        }
        public async Task<IActionResult> Review()
        {
            var pendingReviews = _context.Review.Include(x => x.AspUser).Include(x => x.Game).Where(x => x.IsApproved == false);
            return View(pendingReviews);
        }

        public async Task<IActionResult> CheckReview(int id, bool isDeclined)
        {
            if (isDeclined)
            {
                var currentReview = _context.Review.Where(x => x.ReviewId == id).FirstOrDefault();
                _context.Review.Remove(currentReview);

            }
            else
            {
                _context.Review.Where(x => x.ReviewId == id).FirstOrDefault().IsApproved = true;
            }
            _context.SaveChanges();
            return RedirectToAction("Review");
        }
        public async Task<IActionResult> Orders()
        {
            string items = "";
            var games = _context.CartGame.Include(x => x.Cart).Include(x => x.Cart.User).Include(x => x.Game).Where(x => x.Cart.StateOfOrder == "In Process").ToList();
            foreach (var item in games)
            {
                items += item.Game.Name + ",";
            }
            var merch = _context.CartMerchandise.Include(x => x.Cart).Include(x => x.Cart.User).Include(x => x.Merchandise).Where(x => x.Cart.StateOfOrder == "In Process").ToList();
            foreach (var item in merch)
            {
                items += item.Merchandise.Name + ",";
            }
            ViewBag.Items = items.Trim(',');
            ViewBag.NewGames = games;
            ViewBag.NewMerch = merch;

            return View();
        }

        public async Task<IActionResult> ProcessOrder(int id, bool isProcessed)
        {
            if (isProcessed)
            {
                var currentReview = _context.Cart.Where(x => x.CartId == id).FirstOrDefault();
                currentReview.StateOfOrder = "Processed";
                _context.Update(currentReview);
                await _context.SaveChangesAsync();


            }
            return RedirectToAction("Orders");
        }
    }
}
