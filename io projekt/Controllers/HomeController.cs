
using io_projekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace io_projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        public IActionResult AddNewUser()
        {

            if(Request.Method == "POST") 
            {
               

                int wiek = Int32.Parse(Request.Form["age"]);
                int skill = Int32.Parse(Request.Form["skills"]);
                MainUser.AddNewUser(Request.Form["login"], Request.Form["password"], Request.Form["name"], Request.Form["lastName"], wiek, Request.Form["type"], skill);
                
            }
            return View();
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