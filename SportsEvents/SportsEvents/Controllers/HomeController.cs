using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEvents.Controllers
{
    public class HomeController : Controller
    {

        SportsDbAccessLayer log = new SportsDbAccessLayer();

        public IActionResult Login()
        {
            return View();
        }
        // Reference: https://www.youtube.com/watch?v=mWntrphY54w
        [HttpPost]
        public IActionResult Login([Bind] Member member)
        {
            int res = log.UserLogInCheck(member);
            if (res == 1)
            {
                return RedirectToAction(nameof(Homepage));
            }
            else
            {
                TempData["msg"] = "The Username or Password entered was incorrect.";
            }
            return View();
        }

        SportsDbAccessLayer userdb = new SportsDbAccessLayer();

        private readonly ApplicationDbContext dbc;
        public HomeController(ApplicationDbContext context)
        {
            dbc = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Homepage()
        {
            var sports = from m in dbc.Sport
                         select m;

            return View(sports);
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Membership()
        {
            ViewBag.SportID = new SelectList(dbc.Sport, "SportID", "Event");
            return View();
        }

        //Reference: https://www.c-sharpcorner.com/article/how-to-insert-data-to-database-using-model-in-asp-net-core-2-1-mvc-using-ado-net/
        [HttpPost]
        public IActionResult Membership([Bind] Membership membership)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbc.Membership.Add(membership);
                    string resp = userdb.AddUser(membership);
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            ViewBag.SportID = new SelectList(dbc.Sport, "SportID", "Event", membership.SportID);

            return View();
        }


    }
}
