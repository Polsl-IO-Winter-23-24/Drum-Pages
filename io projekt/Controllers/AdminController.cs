using io_projekt.Models;
using io_projekt.Views.Home;
using Microsoft.AspNetCore.Mvc;

namespace io_projekt.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}


		[HttpPost]
		public IActionResult DeleteThread(int id)
		{
			//USUWANIE WATKU 
			Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-USUNIETO Watek-=-=-=-=-=-=-=-=-=-=-=-=-=-");
			Models.Thread.RemoveThread(id);
			
			return RedirectToAction("AdminPanel", "Home");
		}
	}
}
