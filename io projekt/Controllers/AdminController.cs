using io_projekt.Models;
using io_projekt.Views.Home;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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

		[HttpPost]
		public IActionResult EditThread(int threadId, string editAction, string t_name_input, int user_id_input)
		{
			//USUWANIE WATKU 
			Console.WriteLine("-+-+-+-++-+-+EDYTOWANIE WATKU+-+-+-+-++-+-+" + threadId +" :-:" + editAction + " :-:" + t_name_input +" :-: " +user_id_input);
			if (editAction == "author")
			{
				Models.Thread.updateQuery(threadId, "uzytkownikId", user_id_input.ToString());
			}
			else if (editAction == "t_name")
			{
				Models.Thread.updateQuery(threadId, "temat", t_name_input);
			}
			//Models.Thread.RemoveThread(id);

			return RedirectToAction("AdminPanel", "Home");
		}


	}
}
