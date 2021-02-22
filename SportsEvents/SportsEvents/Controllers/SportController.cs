using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsEvents.Models;


namespace SportsEvents.Controllers
{
    public class SportController : Controller
    {
        private readonly ApplicationDbContext _context;

       SportsDbAccessLayer log = new SportsDbAccessLayer();

        public SportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            var sports = from m in _context.Sport
                         select m;
            
            return View(sports);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        // Reference: https://www.youtube.com/watch?v=mWntrphY54w
        [HttpPost]
        public IActionResult Login([Bind] Admin admin)
        {
            int res = log.LogInCheck(admin);
            if (res == 1)
            {
                return RedirectToAction(nameof(Index));
            }             
            else
            {
                TempData["msg"] = "The Username or Password entered was incorrect.";
            }
            return View();
        }

        // Reference: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-2.1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportID,SportName,Event,Description,Date,Time,Location")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sport.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportID,SportName,Event,Description,Date,Time,Location")] Sport sport)
        {
            if (id != sport.SportID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportExists(sport.SportID))
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
            return View(sport);
        }

        private bool SportExists(int id)
        {
            return _context.Sport.Any(s => s.SportID == id);
        }   
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sport
                .FirstOrDefaultAsync(s => s.SportID == id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _context.Sport.FindAsync(id);
            _context.Sport.Remove(sport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
