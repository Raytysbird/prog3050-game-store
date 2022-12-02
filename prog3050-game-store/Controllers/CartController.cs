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

            var address = _context.Address.Where(x => x.UserId == id && x.IsShipping == true).FirstOrDefault();

            var creditCardInfo = _context.CreditCardInfo.Where(x => x.UserId == id).ToList();

            if (address!=null)
            {
                address.FullAddress = string.Join(",", new string[] { address.StreetAddress, address.Building, address.AptNumber, address.UnitNumber }.Where(c => !string.IsNullOrEmpty(c)));
                ViewBag.Address = address;
            }
            if (address==null)
            {
                ViewBag.Address = null;

            }
            if (creditCardInfo.Count == 0)
            {
                ViewBag.CreditCard = null;
            }
            if (creditCardInfo.Count != 0)
            {
                ViewBag.CreditCard = new SelectList(creditCardInfo, "CreditCardId", "Number");
               // ViewBag.CreditCard = creditCardInfo;
            }
            else if (cart == null)
            {
                Cart cartGame = new Cart();
                cartGame.UserId = id;
                cartGame.TotalCost = 0;
                cartGame.CreditCardId = null;
                cartGame.StateOfOrder = null;
                _context.Cart.Add(cartGame);
                _context.SaveChanges();
                TempData["message"] = "No Item added to Cart Right now";
                ViewBag.Cart = null;
                return View();
            }
            var cartGameItems = _context.CartGame.Where(x => x.CartId == cart.CartId).Include(x => x.Game).ToList();
            var cartMerchItems = _context.CartMerchandise.Where(x => x.CartId == cart.CartId).Include(x => x.Merchandise).ToList();

            var total = 0f;

            foreach (var item in cartGameItems)
            {
                var priceGameItem = await _context.Game.FindAsync(item.GameId);
                if (priceGameItem == null)
                {
                    return NotFound();
                }

                total += priceGameItem.Price;
            }

            foreach (var item in cartMerchItems)
            {
                var cartMerchItem = await _context.Merchandise.FindAsync(item.MerchandiseId);
                if (cartMerchItem == null)
                {
                    return NotFound();
                }

                total += cartMerchItem.Price;
            }
            //var priceMerchItem = _context.Merchandise.Where(x => x.CartId == cart.CartId).Select(x => x.Merchandise);

            var cartStatus = _context.Cart.Where(x => x.UserId == id).Select(x=> x.StateOfOrder);
            if(cartStatus != null)
            {
                ViewBag.Status = cartStatus.FirstOrDefault();
            }
           
            if (cartGameItems!=null || cartMerchItems !=null )
            {
                ViewBag.CartGame = cartGameItems;
                ViewBag.CartMerch = cartMerchItems;
                ViewBag.Total = total;
                

            }
            var cartModel = _context.Cart.Include(x=> x.User).Where(x => x.UserId == id).Include(x=> x.CreditCard).FirstOrDefault();

            return View(cartModel);
        }
        public async Task<IActionResult> Purchases(int? id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            if (id != null)
            {
                

                var cart = _context.Cart.Where(x =>x.UserId == user_id).FirstOrDefault();
                if (cart.StateOfOrder == null)
                {
                    cart.StateOfOrder = "In Process";
                    _context.Cart.Update(cart);
                    _context.SaveChanges();
                }
                //else if (cart.StateOfOrder == "Delivered")
                //{
                //    _context.CartGame.ToList();
                //    Cart cartGame = new Cart();
                //    cartGame.UserId = user_id;
                //    cartGame.TotalCost = 0;
                //    cartGame.CreditCardId = null;
                //    cartGame.StateOfOrder = null;
                //    _context.Cart.Add(cartGame);
                //    _context.SaveChanges();
                //    TempData["message"] = "No Item added to Cart Right now";
                //    ViewBag.Cart = null;
                //    return View("Index");
                //}

                var cartGameItems = _context.CartGame.Where(x => x.CartId == cart.CartId).Include(x => x.Game).ToList();
                var cartMerchItems = _context.CartMerchandise.Where(x => x.CartId == cart.CartId).Include(x => x.Merchandise).ToList();

                var creditCardInfos = _context.CreditCardInfo.Where(x => x.CreditCardId == id).FirstOrDefault();



                //var cartGameItems = _context.CartGame.Where(x => x.CartId == cart.CartId).Include(x => x.Game).ToList();
                //var cartMerchItems = _context.CartMerchandise.Where(x => x.CartId == cart.CartId).Include(x => x.Merchandise).ToList();

                var total = 0d;

                foreach (var item in cartGameItems)
                {
                    var priceGameItem = await _context.Game.FindAsync(item.GameId);
                    if (priceGameItem == null)
                    {
                        return NotFound();
                    }

                    total += Math.Round(priceGameItem.Price);
                }

                foreach (var item in cartMerchItems)
                {
                    var cartMerchItem = await _context.Merchandise.FindAsync(item.MerchandiseId);
                    if (cartMerchItem == null)
                    {
                        return NotFound();
                    }

                    total += Math.Round(cartMerchItem.Price,2);
                }
                //var priceMerchItem = _context.Merchandise.Where(x => x.CartId == cart.CartId).Select(x => x.Merchandise);



                //if (cartGameItems != null || cartMerchItems != null)
                //{
                    ViewBag.CartGame = cartGameItems;
                    ViewBag.CartMerch = cartMerchItems;
                    ViewBag.Total = total;

                //}




                //foreach (var item in creditCardInfos)
                //{
                //    var creditCardNumber = await _context.CreditCardInfo.FindAsync(item.CreditCardId);
                //    if (creditCardNumber == null)
                //    {
                //        return NotFound();
                //    }

                //}

                //if (cartGameItems.Count != 0 || cartMerchItems.Count != 0)
                //{
                //    ViewBag.CartGame = cartGameItems;
                //    ViewBag.CartMerch = cartMerchItems;
                //}

                ViewBag.Status = _context.Cart.Where(x => x.UserId == user_id).FirstOrDefault().StateOfOrder;



                TempData["message"] = "Thank you for placing an order";
                return View();


            }

            var cartStatus = _context.Cart.Where(x => x.UserId == user_id).Select(x => x.StateOfOrder);
            if (cartStatus != null)
            {
                ViewBag.Status = cartStatus.FirstOrDefault();
                if (cartStatus.FirstOrDefault() != "In Process" && cartStatus.FirstOrDefault() != "Delivered")
                {
                    TempData["message"] = "Order In Process";

                }
            }

            return View("Purchases");




            //var cartDetails = _context.Cart.Include(x => x.CartMerchandise).Include(x => x.CartGame).Where(x=> x.UserId == us.ToList();
            //}


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
           
            if (cart == null)
            {
                Cart cartGame = new Cart();
                cartGame.UserId = user_id;
                cartGame.TotalCost = 0;
                cartGame.CreditCardId = null;
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
        public async Task<IActionResult> Delete(int gameId, int merchId)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);


            if (gameId != 0)
            {
                var cartGame = await _context.CartGame.FirstOrDefaultAsync(x => x.GameId == gameId);
                TempData["message"] = "Game removed from your cart";
                _context.CartGame.Remove(cartGame);
                await _context.SaveChangesAsync();
            }
            if (merchId != 0)
            {
                var cartMerch = await _context.CartMerchandise.FirstOrDefaultAsync(x => x.MerchandiseId == merchId);
                TempData["message"] = "Merchandise removed from your cart";
                _context.CartMerchandise.Remove(cartMerch);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
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
