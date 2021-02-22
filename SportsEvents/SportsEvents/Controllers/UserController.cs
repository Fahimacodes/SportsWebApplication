using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsEvents.Models;



namespace SportsEvents.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;


        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {

            var members = from m in _context.Membership
                         select m;

            return View(members);
        }

        public IActionResult Create()
        {
            ViewBag.SportID = new SelectList(_context.Sport, "SportID", "Event");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportID,UserRegisteredID,Name,DOB,Gender,Email,TelephoneNo,MobileNo,HouseNo,StreetName,PostCode,Biography,Skills,WorkLocation")] Membership member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SportID = new SelectList(_context.Sport, "SportID", "Event", member.SportID);
            return View(member);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Membership.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            ViewBag.SportID = new SelectList(_context.Sport, "SportID", "Event");
            return View(member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportID,UserRegisteredID,Name,DOB,Gender,Email,TelephoneNo,MobileNo,HouseNo,StreetName,PostCode,Biography,Skills,WorkLocation")] Membership membership)
        {
            if (id != membership.UserRegisteredID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(membership.UserRegisteredID))
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
            ViewBag.SportID = new SelectList(_context.Sport, "SportID", "Event", membership.SportID);
            return View(membership);
        }

        private bool MemberExists(int id)
        {
            return _context.Membership.Any(s => s.UserRegisteredID == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Membership
                .FirstOrDefaultAsync(m => m.UserRegisteredID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Membership.FindAsync(id);
            _context.Membership.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
