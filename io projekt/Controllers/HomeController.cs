
using io_projekt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Diagnostics;
using System.Threading;
using System.Data.SqlClient;
using Thread = io_projekt.Models.Thread;
using Microsoft.Extensions.Hosting;

namespace io_projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISession _session;
        private int currentUserID;
        private string whoIsLogged;  // sprawdzanie kto jest zalogowany - rozne funkcje dla roznych kont
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            
            currentUserID = _session.GetInt32("currentUserID") ?? 0;
            if (currentUserID != 0)
            {
                whoIsLogged = MainUser.GetUserById(currentUserID).user.getAccountType();
                ViewBag.UserName = MainUser.GetUserById(currentUserID).user.getName();
                ViewBag.IsLoggedIn = whoIsLogged;
            }
            return View();
        }


        public IActionResult Forum()
        {
			currentUserID = _session.GetInt32("currentUserID") ?? 0;
			if (currentUserID != 0)
			{
				whoIsLogged = MainUser.GetUserById(currentUserID).user.getAccountType();
				ViewBag.IsLoggedIn = whoIsLogged;
			}
			// Retrieve entries for the specified threadId from your data source
			List<Thread> threads = Thread.GetAllThreads();
            List<Tuple<Thread, List<Post>>> threadDataList = new List<Tuple<Thread, List<Post>>>();
            List<Tuple<int, bool>> ratedThreadIdsAndRatings = new List<Tuple<int, bool>>();

            if (currentUserID != 0)
            {
                (String msg, bool boolean, ratedThreadIdsAndRatings) = Thread.GetThreadIdsAndRatingsByUser(currentUserID);

            }

            foreach (var thread in threads)
            {
                List<Post> posts = Post.GetPostsByThreadId(thread.getID());
                Tuple<Thread, List<Post>> threadData = new Tuple<Thread, List<Post>>(thread, posts);
                threadDataList.Add(threadData);
            }

            var dataToReturn = (threadDataList, ratedThreadIdsAndRatings);
            
            return View(dataToReturn);
        }

       
        public IActionResult Events()
        {
            currentUserID = _session.GetInt32("currentUserID") ?? 0;
            if (currentUserID != 0)
            {
                whoIsLogged = MainUser.GetUserById(currentUserID).user.getAccountType();
                ViewBag.IsLoggedIn = whoIsLogged;           
            }
            return View();
        }
        
         
        public IActionResult Courses()
        {
            currentUserID = _session.GetInt32("currentUserID") ?? 0;
            if (currentUserID != 0)
            {
                whoIsLogged = MainUser.GetUserById(currentUserID).user.getAccountType();
                ViewBag.IsLoggedIn = whoIsLogged;
                ViewBag.UserID = currentUserID;
            }
            return View();
            
        }
        
        public IActionResult Course(int courseID)
	 {
     		currentUserID = _session.GetInt32("currentUserID") ?? 0;
     		if (currentUserID != 0)
     		{
         		ViewBag.courseId = courseID;
         		ViewBag.UserId = currentUserID;
     		}

     		return View();
	 }

          [HttpPost]
  public IActionResult AddCourse(String title, String description, String difficulty)
  {
      Course course = new Course();
      course.setTitle(title);
      course.setDescription(description);
      course.setAuthorID(currentUserID);
      course.setAuthorName(MainUser.GetUserById(currentUserID).user.getLogin());
      course.setDifficulty(int.Parse(difficulty));
      course.setRating(0);
      course.writeToDB();

      return RedirectToAction("Courses");
  }




 	public IActionResult deleteCourse(int courseID)
{

            //try
            //{
            //    String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
            //    SQL query to delete a record from the "Kursy" table with the given ID
            //    string query = "DELETE FROM Kursy WHERE kursId = @kursId";

            //    Create and open a connection to the database
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        Create a command with the query and connection
            //using (SqlCommand command = new SqlCommand(query, connection))
            //        {
            //            Add parameter for the kursId
       
            //           command.Parameters.AddWithValue("@kursId", courseID);

            //            Execute the command to delete the record
       
            //           int rowsAffected = command.ExecuteNonQuery();

            //            Check if any rows were affected
            //    if (rowsAffected > 0)
            //            {
            //                Console.WriteLine("Record deleted successfully.");
            //            }
            //            else
            //            {
            //                Console.WriteLine("No record found with the provided ID: " + courseID);
            //            }
            //        }
            //    }
            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            return RedirectToAction("Courses");
}



        [HttpPost]
        public IActionResult AddLesson(String title, String content, String videoURL, String courseID)
        {
            Lesson lesson = new Lesson();
            lesson.setTitle(title);
            lesson.setContent(content);
            lesson.setCourseID(int.Parse(courseID));
            if (string.IsNullOrEmpty(videoURL))
	    {
   		 lesson.setVideoURL("None");
	    }
	    else {
    		 lesson.setVideoURL(videoURL);
	    }
            lesson.setRating(0);
            lesson.writeToDB();

            return RedirectToAction("Course", new { courseID = courseID });
        }



 	 public IActionResult deleteLesson(int lessonID, int courseID)
 {

     //try
     //{
     //    String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
     //    // SQL query to delete a record from the "Kursy" table with the given ID
     //    string query = "DELETE FROM Lekcje WHERE lekcjaId = @lekcjaId";

     //    // Create and open a connection to the database
     //    using (SqlConnection connection = new SqlConnection(connectionString))
     //    {
     //        connection.Open();

     //        // Create a command with the query and connection
     //        using (SqlCommand command = new SqlCommand(query, connection))
     //        {
     //            // Add parameter for the kursId
     //            command.Parameters.AddWithValue("@lekcjaId", lessonID);

     //            // Execute the command to delete the record
     //            int rowsAffected = command.ExecuteNonQuery();

     //            // Check if any rows were affected
     //            if (rowsAffected > 0)
     //            {
     //                Console.WriteLine("Record deleted successfully.");
     //            }
     //            else
     //            {
     //                Console.WriteLine("No lesson found with the provided ID: " + lessonID);
     //            }
     //        }
     //    }
     //}

     //catch (Exception ex)
     //{
     //    Console.WriteLine(ex.Message);
     //}
     return RedirectToAction("Course", new { courseID = courseID });
 }


        public IActionResult Lesson(int classID)
        {
	        ViewBag.LessonId = classID;
            ViewBag.UserId = currentUserID;
            return View();
        }
        public IActionResult AdminPanel()
        {
            
            var threads = Thread.GetAllThreads();
			int firstThreadId = threads.Count > 0 ? threads[0].getID() : 0;
			List<Post> posts = Post.GetPostsByThreadId(firstThreadId);
			return View(posts);
        }

        [HttpPost]
        public IActionResult Login(string uname, string psw, bool remember)
        {
            Console.WriteLine("LOGIN: "+uname + "  psw: " + psw);
            var cache = MainUser.GetCacheInstance();
            cache.Remove("AllUsers");

            bool pswCorrect = MainUser.CheckPassword(uname, psw);
            if (pswCorrect)
            {
                currentUserID = MainUser.GetUserIdByLogin(uname);

                _session.SetInt32("currentUserID", currentUserID);
                whoIsLogged = MainUser.GetUserById(currentUserID).user.getAccountType();
                Console.WriteLine(whoIsLogged);
                ViewBag.IsLoggedIn = whoIsLogged;
                // ViewBag.IsLoggedIn = true;
                Console.WriteLine("ZALOGOWANO");
                
                if (whoIsLogged == "Admin")
                {
                    var dataa = new { mess = "Welcome boss" };
                    // return RedirectToAction("AdminPanel");
                    return Json(dataa);
                }
                var data = new { mess = "Login correctly" };
                return Json(data);
            }
            else
            {
                Console.WriteLine("NIE ZALOGOWANO");
                var data = new { mess = "Incorrect data" };
                return Json(data);
               

            }
            return RedirectToAction("Index"); // Przekierowanie po zalogowaniu

        }

        [HttpPost]
        public IActionResult Register(string name, string surname, string email, string regname, string regpass, int age, string accountType, int skill)
        {
       
            MainUser.AddNewUser(regname, regpass, name, surname, age, accountType, skill, email);
            //odrazu go zaloguj 
            Login(regname, regpass,false);
            return RedirectToAction("Index"); // Przekierowanie po zarejestrowaniu
        }

        [HttpGet]
        public IActionResult ViewThread()
        {
            // Retrieve entries for the specified threadId from your data source
            List<int> threadIds = Thread.GetThreadIds();
            List<Tuple<int, List<Post>>> threadDataList = new List<Tuple<int, List<Post>>>();

            foreach (int threadId in threadIds)
            {
                List<Post> posts = Post.GetPostsByThreadId(threadId);
                Tuple<int, List<Post>> threadData = new Tuple<int, List<Post>>(threadId, posts);
                threadDataList.Add(threadData);
            }

            // You may need to pass the entries to the view
            return View(threadDataList);
        }

        [HttpPost]
        public IActionResult AddThread(string newThreadTheme)
        {
            currentUserID = _session.GetInt32("currentUserID") ?? 0;
            DateTime creationDate = DateTime.Now;

            (String msg, bool ifWorked, int threadId) = Thread.AddNewThread(newThreadTheme, creationDate, currentUserID);
            return RedirectToAction("Forum");
        }



        [HttpPost]
        public IActionResult addPositiveRatingForThread(int threadId)
        {
            currentUserID = _session.GetInt32("currentUserID") ?? 0;
            (String msg, bool ifWorked) = Thread.AddThreadRatings(1, threadId, currentUserID);

            return RedirectToAction("Forum");
        }

        [HttpPost]
        public IActionResult addNegativeRatingForThread(int threadId)
        {
            currentUserID = _session.GetInt32("currentUserID") ?? 0;
            (String msg, bool ifWorked) = Thread.AddThreadRatings(0, threadId, currentUserID);

            return RedirectToAction("Forum");
        }


        [HttpPost]
        public IActionResult AddPost(string newPostContent, int threadId)
        {
            try
            {
                currentUserID = _session.GetInt32("currentUserID") ?? 0;
                DateTime creationDate = DateTime.Now;

                Post.AddNewPost(newPostContent, creationDate, threadId, currentUserID);

                return RedirectToAction("Forum");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in adding a new post to the database: " + ex.ToString());
                // Handle the exception as needed

                return RedirectToAction("Forum");
            }
        }



        [HttpPost]
        public IActionResult AddEvent(string Event_name, DateTime Event_date, string Event_description, string Event_location, string Event_creator)
        {
            //dodawanie eventu 
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

        [HttpPost]
        public IActionResult LogOut() {
			Console.WriteLine("=+=+=++=+=+=++=+=+=+WYLOGOWANIE=+=+=++=+=+=++=+=+=+");
			currentUserID = 0;
            _session.SetInt32("currentUserID", currentUserID);
            
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ForgotPassword(string user)
        {
            if (MainUser.GetUserIdByLogin(user) == -1)
            {
                // return RedirectToAction("Index");
                var data = new { mess = "No User Found" };
                return Json(data);
            }
            else
            {
                MainUser.RecoverPassword(user);
                Console.WriteLine("odzyskiwanie hasla" + user);
                //  return Json(new { NotFoundMessage = "Nie znaleziono użytkownika." });
                var data = new { mess = "A new password has been sent to an e-mail" };
                return Json(data);
            }
           // return RedirectToAction("Index");
        }


		public IActionResult Privacy()
        {
            



			Console.WriteLine("-----------------------");
            foreach (var i in Misc.GetUserStyle(1))
            {
                Console.WriteLine("Id: " + i.ID + " Nazwa: " + i.NAME);
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
