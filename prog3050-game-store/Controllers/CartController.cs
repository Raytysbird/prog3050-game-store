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
    public class CartController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;

        public CartController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(HttpContext.User);

            var cart = _context.Cart.FirstOrDefault(x => x.UserId == id);

            var creditCardInfo = _context.CreditCardInfo.FirstOrDefault(x => x.UserId == id);
            if (creditCardInfo == null)
            {
                TempData["message"] = "No Credit Card Info there Right now";
                return RedirectToAction("Create", "CreditCardInfo");
                

            }
            else if (cart == null)
            {
                Cart cartGame = new Cart();
                cartGame.UserId = id;
                cartGame.TotalCost = 0;
                cartGame.CreditCardId = creditCardInfo.CreditCardId;
                cartGame.StateOfOrder = null;
                _context.Cart.Add(cartGame);
                _context.SaveChanges();
                TempData["message"] = "No Item added to Cart Right now";
                ViewBag.Cart = null;
                return View();
            }
            var cartGameItem = _context.CartGame.Where(x => x.CartId == cart.CartId).Include(x => x.Game).ToList();
            var cartMerchItem = _context.CartMerchandise.Where(x => x.CartId == cart.CartId).Include(x => x.Merchandise).ToList();
            
            if (cartGameItem!=null || cartMerchItem !=null )
            {
                ViewBag.CartGame = cartGameItem;
                ViewBag.CartMerch = cartMerchItem;

            }
            return View();
        }
        public async Task<IActionResult> Purchases()
        {
            var id = _userManager.GetUserId(HttpContext.User);
            
            var cart = _context.Cart.Where(x=>x.StateOfOrder=="In Process").FirstOrDefault(x => x.UserId == id);
            var cartGameItem = _context.CartGame.Where(x => x.CartId == cart.CartId).Include(x => x.Game).ToList();
            var cartMerchItem = _context.CartMerchandise.Where(x => x.CartId == cart.CartId).Include(x => x.Merchandise).ToList();

            if (cartGameItem != null || cartMerchItem != null)
            {
                ViewBag.CartGame = cartGameItem;
                ViewBag.CartMerch = cartMerchItem;

            }
            return View();
        }


            // GET: Cart/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.CreditCard)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create/
        public IActionResult Create(int gameId,int merchId)
        {
            ViewData["UserId"] = _userManager.GetUserId(HttpContext.User);

            var user_id = _userManager.GetUserId(HttpContext.User);

            var cart = _context.Cart.FirstOrDefault(x => x.UserId == user_id);
            var creditCardInfo = _context.CreditCardInfo.FirstOrDefault(x => x.UserId == user_id);
            if (creditCardInfo == null)
            {
                TempData["message"] = "No Credit Card Info there Right now";
                return RedirectToAction("Create", "CreditCardInfo");


            }
            if (cart == null)
            {
                Cart cartGame = new Cart();
                cartGame.UserId = user_id;
                cartGame.TotalCost = 0;
                cartGame.CreditCardId = creditCardInfo.CreditCardId;
                cartGame.StateOfOrder = null;
                _context.Cart.Add(cartGame);
                _context.SaveChanges();
            }
            if (cart!=null)
            {
                if (gameId!=0)
                {
                    CartGame cartGame = new CartGame();
                    cartGame.CartId = cart.CartId;
                    cartGame.GameId = gameId;
                    _context.CartGame.Add(cartGame);
                    _context.SaveChanges();

                }
                if (merchId!=0)
                {
                    CartMerchandise cartMerchandise = new CartMerchandise();
                    cartMerchandise.CartId = cart.CartId;
                    cartMerchandise.MerchandiseId = merchId;
                    _context.CartMerchandise.Add(cartMerchandise);
                    _context.SaveChanges();
                }
            }
            TempData["message"] = "Item Added to Cart!";
            return RedirectToAction("Index");
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,CreditCardId,TotalCost,StateOfOrder,UserId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                cart.UserId = _userManager.GetUserId(HttpContext.User);
                TempData["message"] = "Added to your cart";
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["CreditCardId"] = new SelectList(_context.CreditCardInfo, "CreditCardId", "ExpDate", cart.CreditCardId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", cart.UserId);
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,CreditCardId,TotalCost,StateOfOrder,UserId")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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
            ViewData["CreditCardId"] = new SelectList(_context.CreditCardInfo, "CreditCardId", "ExpDate", cart.CreditCardId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", cart.UserId);
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);


            var cart = await _context.Cart
                .Include(c => c.CreditCard)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.CartId == id);
        }
    }
}
