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


		[HttpPost]
		public IActionResult DeleteUser(int id)
		{
			//USUWANIE WATKU 
			Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-USUNIETO Usera-=-=-=-=-=-=-=-=-=-=-=-=-=-" + id);
			MainUser.DeleteAcoount(id);

			return RedirectToAction("AdminPanel", "Home");
		}

		[HttpPost]
		public IActionResult EditUser([FromForm] int userId,
				[FromForm] string editAction,
				[FromForm] string t_login_input,
				[FromForm] string t_name_input,
				[FromForm] string t_lname_input,
				[FromForm] string type_input,
				[FromForm] string skill_input,
				[FromForm] string t_age_input,
				[FromForm] string t_email_input)
		{
			Console.WriteLine("EDYCJA USERA");
			switch (editAction)
			{
				case "t_login":
					if (!string.IsNullOrEmpty(t_login_input)) {
						MainUser.EditAccount(userId, "login", t_login_input);
					}
					break;
				case "t_name":
					if (!string.IsNullOrEmpty(t_name_input))
					{
						MainUser.EditAccount(userId, "imie", t_name_input);
					}
					break;
				case "t_lastname":
					if (!string.IsNullOrEmpty(t_lname_input))
					{
						MainUser.EditAccount(userId, "nazwisko", t_lname_input);
					}
					break;
				case "t_age":
					if (!string.IsNullOrEmpty(t_age_input))
					{
						MainUser.EditAccount(userId, "wiek", t_age_input);
			}
					break;
				case "t_skill":
					if (!string.IsNullOrEmpty(skill_input))
					{
						Console.WriteLine("UPADATE SKILLL");
						MainUser.EditAccount(userId, "umiejetnosci", skill_input);
					}
					break;
				case "t_type":
					if (!string.IsNullOrEmpty(type_input))
					{
						MainUser.EditAccount(userId, "rodzajKonta", type_input);
					}
					break;
				case "t_mail":
					if (!string.IsNullOrEmpty(t_email_input))
					{
						MainUser.EditAccount(userId, "email", t_email_input);
					}
					break;
			}
			return RedirectToAction("AdminPanel", "Home");
		}


	}
}
