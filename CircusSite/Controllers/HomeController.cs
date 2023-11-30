using CircusSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CircusSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddNewProgramm(string Name, DateTime Date, int Price)
        {
            if (string.IsNullOrWhiteSpace(Name) ||
               string.IsNullOrWhiteSpace(Price.ToString())
               )
            {
                return View("CreateProgramm");
            }
            else
            {
                if (ModelState.IsValid &&
                    Price > 0
                    )
                {
                    using(CircusContext db = new CircusContext() )
                    {
                        Ticket tic = new Ticket();
                        tic.Price = Price;
                        tic.ProgrammName = Name;  
                        tic.DataProvedenia = Date;
                        db.Tickets.Add(tic);
                        db.SaveChanges();
                        return View("CreateProgramm");
                    }

                }
                return View("CreateProgramm");

            }
        }


        [HttpPost]
        public IActionResult OpenEdit(string Id)
        {
            using(CircusContext db = new CircusContext())
            {
                Ticket tic = db.Tickets.FirstOrDefault(x => x.IdTicket.ToString() == Id)!;
                DataForEdit.ticket = tic;
                return View("EditProgramm");
            }
        }

        [HttpPost]
        public IActionResult EditProgramm(int Id, string Name, DateTime Date, int Price)
        {
            if (string.IsNullOrWhiteSpace(Name) ||
               string.IsNullOrWhiteSpace(Price.ToString())
               )
            {
                return View("EditProgramm");
            }
            else
            {
                if (ModelState.IsValid &&
                    Price > 0
                    )
                {
                    using (CircusContext db = new CircusContext())
                    {
                        Ticket tic = db.Tickets.FirstOrDefault(x => x.IdTicket == Id);
                        tic.Price = Price;
                        tic.ProgrammName = Name;
                        tic.DataProvedenia = Date;
                        db.SaveChanges();
                        return View("Index");
                    }

                }
                return View("EditProgramm");

            }
        }

        [HttpPost]
        public IActionResult DeleteProgramm(int Id)
        {
                using(CircusContext db = new CircusContext())
                {
                    Ticket tic = db.Tickets.FirstOrDefault(x => x.IdTicket == Id);
                    db.Tickets.Remove(tic);
                    db.SaveChanges();
                }
            return View("Index");
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditProgramm()
        {
            return View();
        }

        public IActionResult CreateProgramm()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}