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

            var total = 0f;
            var id = _userManager.GetUserId(HttpContext.User);
            var cart = _context.Cart.Where(x => x.UserId == id).FirstOrDefault(x => x.StateOfOrder == null);
            if (cart == null)
            {
                cart = new Cart();
                cart.UserId = id;
                cart.TotalCost = 0;
                cart.CreditCard = null;
                cart.CartMerchandise = null;
                _context.Cart.Add(cart);
                _context.SaveChanges();
            }
            var address = _context.Address.Where(x => x.UserId == id && x.IsShipping == true).FirstOrDefault();
            var creditCardInfo = _context.CreditCardInfo.Where(x => x.UserId == id).ToList();
            var cartModel = _context.Cart.Include(x => x.User).Where(x => x.UserId == id).Include(x => x.CreditCard).FirstOrDefault(x => x.StateOfOrder == null);

            if (address != null)
            {
                address.FullAddress = string.Join(",", new string[] { address.StreetAddress, address.Building, address.AptNumber, address.UnitNumber }.Where(c => !string.IsNullOrEmpty(c)));
                ViewBag.Address = address;
            }
            if (address == null)
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
            }

            var cartGameItems = _context.CartGame.Where(x => x.CartId == cart.CartId).Include(x => x.Game).ToList();
            var cartMerchItems = _context.CartMerchandise.Where(x => x.CartId == cart.CartId).Include(x => x.Merchandise).ToList();

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
            if (cartGameItems.Count > 0 || cartMerchItems.Count > 0)
            {
                ViewBag.CartGame = cartGameItems;
                ViewBag.CartMerch = cartMerchItems;
                ViewBag.Total = total;
            }

            cartModel.TotalCost = total == 0 ? 0 : (float)Math.Round(total + (0.13f * total), 2);
            return View(cartModel);
        }
        public IActionResult DownloadGame(int id)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Games");
                var currentRow = 1;
                int count = 0;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "Description";
                worksheet.Cell(currentRow, 3).Value = "Price";

                var games = _context.Game.FirstOrDefault(x => x.GameId == id);

                currentRow++;
                count++;
                worksheet.Cell(currentRow, 1).Value = games.Name;
                worksheet.Cell(currentRow, 2).Value = games.Description;
                worksheet.Cell(currentRow, 3).Value = games.Price;


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "GameDetails.xlsx"
                        );
                }

            }

        }
        public IActionResult Sales()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Games");
                var currentRow = 1;
                int count = 0;
                worksheet.Cell(currentRow, 1).Value = "Item Number";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Description";
                worksheet.Cell(currentRow, 4).Value = "Price";
                worksheet.Cell(currentRow, 5).Value = "Quantity";
                worksheet.Cell(currentRow, 6).Value = "Total";


                var games = _context.CartGame.Include(x => x.Cart).Include(x => x.Cart.User).Include(x => x.Game).Where(x => x.Cart.StateOfOrder!=null).OrderBy(x=>x.GameId).ToList();
                var merch = _context.CartMerchandise.Include(x => x.Cart).Include(x => x.Cart.User).Include(x => x.Merchandise).Where(x => x.Cart.StateOfOrder !=null).ToList();

                for (int i = 0; i <games.Count; )
                {
                    var gameSales = _context.CartGame.Include(x => x.Cart).Where(x => x.GameId == games[i].GameId).Where(x=>x.Cart.StateOfOrder!=null).ToList();
                    if (gameSales.Count > 0)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = games[i].GameId;
                        worksheet.Cell(currentRow, 2).Value = games[i].Game.Name;
                        worksheet.Cell(currentRow, 3).Value = games[i].Game.Description;
                        worksheet.Cell(currentRow, 4).Value = games[i].Game.Price;
                        worksheet.Cell(currentRow, 5).Value = gameSales.Count;
                        worksheet.Cell(currentRow, 6).Value = gameSales.Count * games[i].Game.Price;
                        i = i + gameSales.Count;
                    }
                }

                for (int i = 0; i < merch.Count;)
                {
                    var merchSales = _context.CartMerchandise.Include(x => x.Cart).Where(x => x.MerchandiseId == merch[i].MerchandiseId).Where(x => x.Cart.StateOfOrder != null).ToList();
                    if (merchSales.Count > 0)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = merch[i].MerchandiseId;
                        worksheet.Cell(currentRow, 2).Value = merch[i].Merchandise.Name;
                        worksheet.Cell(currentRow, 3).Value = merch[i].Merchandise.Description;
                        worksheet.Cell(currentRow, 4).Value = merch[i].Merchandise.Price;
                        worksheet.Cell(currentRow, 5).Value = merchSales.Count;
                        worksheet.Cell(currentRow, 6).Value = merchSales.Count * games[i].Game.Price;
                        i = i + merchSales.Count;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "SalesReport.xlsx"
                        );
                }

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Cart carts)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                carts.StateOfOrder = "In Process";
                carts.UserId = user_id;
                _context.Update(carts);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Purchases");
        }

        public async Task<IActionResult> Purchases()
        {
            List<CartGame> lstGame = new List<CartGame>();
            var user_id = _userManager.GetUserId(HttpContext.User);
            var cart = _context.Cart.Where(x => x.UserId == user_id).Where(x => x.StateOfOrder != null);
            if (cart == null)
            {
                TempData["message"] = "You don't have any purchases with us yet. Please checkout our Games and Merchandise inventory!!";
                return View();
            }
            else
            {
                
                 var games = _context.CartGame.Include(x=>x.Cart).Include(x=>x.Game).Where(x=>x.Cart.UserId==user_id).Where(x=>x.Cart.StateOfOrder!=null).ToList();
                var merch = _context.CartMerchandise.Include(x => x.Cart).Include(x => x.Merchandise).Where(x => x.Cart.UserId == user_id).Where(x => x.Cart.StateOfOrder != null).ToList();



                ViewBag.CartGame = games;
                ViewBag.CartMerch = merch;
                return View();
            }
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

        public IActionResult Create(int gameId, int merchId)
        {
            ViewData["UserId"] = _userManager.GetUserId(HttpContext.User);

            var user_id = _userManager.GetUserId(HttpContext.User);

            var cart = _context.Cart.Where(x => x.UserId == user_id).FirstOrDefault(x => x.StateOfOrder == null);
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

            if (gameId != 0)
            {
                CartGame cartGame = new CartGame();
                cartGame.CartId = cart.CartId;
                cartGame.GameId = gameId;
                _context.CartGame.Add(cartGame);
                _context.SaveChanges();

            }
            if (merchId != 0)
            {
                CartMerchandise cartMerchandise = new CartMerchandise();
                cartMerchandise.CartId = cart.CartId;
                cartMerchandise.MerchandiseId = merchId;
                _context.CartMerchandise.Add(cartMerchandise);
                _context.SaveChanges();
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