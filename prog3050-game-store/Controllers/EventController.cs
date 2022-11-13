using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Identity;



namespace GameStore.Controllers
{
    public class EventController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<User> _userManager;



        public EventController(GameContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: Event
        public async Task<IActionResult> Index()
        {
            var user = _userManager.GetUserId(HttpContext.User);
            if (HttpContext.User.IsInRole("User"))
            {
                var userEvents = _context.UserEvent.Where(x => x.AspUserId == user).Select(x => x.EventId).ToList();
                var eventsExcludingCurrent = _context.Events.Where(x => !userEvents.Contains(x.EventId)).ToList();
                return View(eventsExcludingCurrent);
            }
           
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> UserEvents()
        {
            var user = _userManager.GetUserId(HttpContext.User);
            var events = await _context.UserEvent.Include(x=> x.Event).Where(x=> x.AspUserId == user).ToListAsync();
            return View(events);
        }
        public async Task<IActionResult> Booking(int id, bool isRegsiter)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            var selectEvent = _context.Events.Where(x => x.EventId == id).FirstOrDefault();
            if (isRegsiter)
            {
                UserEvent userEvent = new UserEvent() { EventId = id, AspUserId = user };
                _context.UserEvent.Add(userEvent);
                _context.SaveChanges();
                TempData["message"] = "Registered Event!!";
                return RedirectToAction("Index");
            }

            var currentEvent = _context.UserEvent.Where(x => x.EventId == id).FirstOrDefault();
            _context.UserEvent.Remove(currentEvent);
            TempData["message"] = "Unregistered Event!";
            _context.SaveChanges();
            return RedirectToAction("UserEvents");


        }



        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var events = await _context.Events
                 .FirstOrDefaultAsync(m => m.EventId == id);
            if (events == null)
            {
                return NotFound();
            }



            return View(events);
        }



        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,Description,StartDate,EndDate")] Events events)
        {
            if (ModelState.IsValid)
            {
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }



        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }



        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Name,Description,StartDate,EndDate")] Events events)
        {
            if (id != events.EventId)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.EventId))
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
            return View(events);
        }




        // POST: Event/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var events = await _context.Events.FindAsync(id);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}