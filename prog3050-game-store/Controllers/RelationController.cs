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
using Microsoft.AspNetCore.Http;

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
            var currentUser = _userManager.GetUserId(HttpContext.User);
            if (keyword!=null)
            {
                HttpContext.Session.SetString("keyword", keyword);
            }
           
            ViewBag.User = null;
            List<AspNetUsers> lstUserId = new List<AspNetUsers>();
            List<AspNetUsers> lstFriends = new List<AspNetUsers>();
            List<AspNetUsers> lstNewFriends = new List<AspNetUsers>();
            List<String> lstNotInSearch = new List<string>();

            //For current friends of user
            var friendList = await _context.Relation.Where(x => (x.ToUser == currentUser || x.FromUser == currentUser)).Where(z => z.AreFriends == true).ToListAsync();
            foreach (var item in friendList)
            {
                if (item.FromUser != currentUser)
                {
                    AspNetUsers user = new AspNetUsers();
                    user.Id = item.FromUser;
                    lstUserId.Add(user);
                    lstNotInSearch.Add(user.Id.ToString());

                }
                if (item.ToUser != currentUser)
                {
                    AspNetUsers user = new AspNetUsers();
                    user.Id = item.ToUser;
                    lstUserId.Add(user);
                    lstNotInSearch.Add(user.Id.ToString());
                }
            }

            foreach (var item in lstUserId)
            {
                var userDetails = _context.AspNetUsers.FirstOrDefault(x => x.Id == item.Id);
                lstFriends.Add(userDetails);

            }
            ViewBag.Friends = lstFriends;
            //Request sent and Search filter
            var requestSent = await _context.Relation.Include(c=>c.ToUserNavigation).Where(x => x.FromUser == currentUser).Where(z => z.AreFriends == null).ToListAsync();
            ViewBag.SentRequest = requestSent;

            var pendingRequests = await _context.Relation.Include(c => c.FromUserNavigation).Where(x => x.ToUser == currentUser).Where(z => z.AreFriends == null).ToListAsync();
            ViewBag.PendingRequests = pendingRequests;

            foreach (var item in requestSent)
            {
                lstNotInSearch.Add(item.ToUserNavigation.Id);
            }
            foreach (var item in pendingRequests)
            {
                lstNotInSearch.Add(item.FromUserNavigation.Id);
            }
            if (keyword != null)
            {
               
               var user = _context.AspNetUsers.Where(x => x.UserName.Contains(keyword)).Where(y=>y.Id!=currentUser).ToList();
                foreach (var item in user)
                {
                    if (!lstNotInSearch.Contains(item.Id))
                    {
                        lstNewFriends.Add(item);

                    }
                }
                if (lstNewFriends.Count>0)
                {
                    ViewBag.User = lstNewFriends;
                }
                else
                {
                    ViewBag.User = null;
                }
                ViewBag.Keyword = keyword;
               
            }                     
            return View("Index");
        }

        // GET: Relation/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

            public async Task<IActionResult> SendRequest([Bind("RelationId,FromUser,ToUser,AreFriends")] Relation relation, string id)
        {
            if (ModelState.IsValid)
            {
                var user= _userManager.GetUserId(HttpContext.User);
                relation.FromUser= _userManager.GetUserId(HttpContext.User);
                relation.ToUser = id;
                relation.AreFriends = null;
                _context.Add(relation);
                TempData["message"] = "Request sent successfully!!";
                await _context.SaveChangesAsync();
                string k=HttpContext.Session.GetString("keyword");
                return RedirectToAction("Index", "Relation",new {keyword=k });
            }
            ViewData["FromUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.FromUser);
            ViewData["ToUser"] = new SelectList(_context.AspNetUsers, "Id", "Id", relation.ToUser);
            return View("Index");
        }
        public async Task<IActionResult> AcceptRequest(string id, int relId)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(HttpContext.User);
                Relation relation = new Relation();
                relation.RelationId = relId;
                relation.FromUser = id;
                relation.ToUser = user;
                relation.AreFriends = true;
                _context.Update(relation);
                TempData["message"] = "Added to friend list successfully!!";
                await _context.SaveChangesAsync();
                string k = HttpContext.Session.GetString("keyword");
                return RedirectToAction("Index", "Relation", new { keyword = k });
            }
            return View("Index");
        }
        public async Task<IActionResult> DeleteRequest(string id,int relId)
        {
            if (ModelState.IsValid)
            {
                var relation = await _context.Relation.FindAsync(relId);
                _context.Relation.Remove(relation);
                await _context.SaveChangesAsync();
                TempData["message"] = "Request removed successfully!!";
                return RedirectToAction(nameof(Index));
               
            }
            return View("Index");
        }
        public async Task<IActionResult> RemoveFriend(string id, int relId)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserId(HttpContext.User);
                var relation =  _context.Relation.Where(x => (x.ToUser == currentUser || x.FromUser == currentUser)&&(x.ToUser == id || x.FromUser == id)).FirstOrDefault(z => z.AreFriends == true);
                _context.Relation.Remove(relation);
                await _context.SaveChangesAsync();
                TempData["message"] = "Removed from your friend list successfully!!";
                return RedirectToAction(nameof(Index));
               
            }
            return View("Index");
        }
       
    }
}
