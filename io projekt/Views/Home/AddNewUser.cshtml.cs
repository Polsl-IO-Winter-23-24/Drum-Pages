using io_projekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace io_projekt.Views.Home
{
    public class AddNewUserModel : PageModel
    {
        [BindProperty]
        public string login { get; set; }

        [BindProperty]
        public string password { get; set; }

        [BindProperty]
        public string name { get; set; }

        [BindProperty]
        public string lastName { get; set; }

        [BindProperty]
        public int age { get; set; }

        [BindProperty]
        public string type { get; set; }

        [BindProperty]
        public int skills { get; set; }

    }
}
