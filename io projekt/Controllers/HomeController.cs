
using io_projekt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Diagnostics;

namespace io_projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISession _session;
        private int currentUserID;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {   
            return View();
        }


        public IActionResult Forum()
        {
            return View();
        }

        public IActionResult Kursy()
        {
            return View();
        }
        public IActionResult Events()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string uname, string psw, bool remember)
        {
            bool pswCorrect = MainUser.CheckPassword(uname, psw);
            if (pswCorrect)
            {
                currentUserID = MainUser.GetUserIdByLogin(uname);
                _session.SetInt32("currentUserID", currentUserID);
            }
            return RedirectToAction("Index"); // Przekierowanie po zalogowaniu

        }

        [HttpPost]
        public IActionResult Register(string name, string surname, string email, string regname, string regpass)
        {
            // Obsługa rejestracji
            int wiek = 0;// Int32.Parse(Request.Form["age"]);
            int skill = 0;// Int32.Parse(Request.Form["skills"]);
            String konto = "Pro";
            MainUser.AddNewUser(regname, regpass, name, surname, wiek, konto, skill);

            return RedirectToAction("Index"); // Przekierowanie po zarejestrowaniu
        }

        [HttpPost]
        public IActionResult AddEvent(string Event_name, DateTime Event_date, string Event_description, string Event_location, string Event_creator)
        {
            currentUserID = _session.GetInt32("currentUserID") ?? 0;
            
            var result = Event.AddNewEvet(Event_name, Event_date, Event_description, Event_location, currentUserID);

            
            if (result.boolean)
            {
                return RedirectToAction("Events");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        public IActionResult Privacy()
        {
            //var a = MainUser.TestPar();
            Console.WriteLine("Odczytanie z bazy wydarzeń: ");
            Console.WriteLine("-----------------------");
            foreach (var i in Event.GetAllEvents())
            {
                Console.WriteLine("Nazawa: " + i.getName());
            }
            Console.WriteLine("-----------------------");
            var a = Event.AddNewEvet("test23", new DateTime(2025, 10, 10), "opi2", "miejsc2e", 6);
            if (a.boolean)
            {
                Console.WriteLine(a.message);
            }
            else 
            {
                Console.WriteLine(a.message);
            }
            Console.WriteLine("-----------------------");
            foreach (var i in Event.GetAllEvents())
              {
                  Console.WriteLine("Nazawa: " + i.getName());
              }
            Console.WriteLine("-----------------------");


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

    }
}