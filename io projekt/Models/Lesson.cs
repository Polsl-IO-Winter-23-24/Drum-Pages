using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Threading;




namespace io_projekt.Models
{
    public class Lesson : PageModel
    {

        private int id;
        private string title;
        private string content;
        private int courseID;
        private string videoURL;
        private int rating;

       

        public Lesson()
        {
            title = "";
            content = "xddd";
            videoURL = "";
            OnGet();
        }

        public void OnGet()
        {
        if (int.TryParse(Request.Query["id"], out int Cid))
        {
            this.id = Cid;

            try
            {

                String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Console.WriteLine("tabela: ");
                    connection.Open();
                    String query = "select * from Kursy where kursId=" + this.id;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                this.content = reader.GetString(3);

                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        }



        public int getID() { return id; }
        public string getTitle() { return title; }
        public string getContent() { return content; }
        public int getCourseID() { return courseID; }
        public string getVideoURL() { return videoURL; }
        public int getRating() { return rating; }

        public void setID(int id) { this.id = id; }
        public void setTitle(string title) { this.title = title; }
        public void setContent(string content) { this.content = content; }
        public void setCourseID(int courseID) { this.courseID = courseID; }
        public void setVideoURL(string url) { this.videoURL = url; }
        public void setRating(int rating) { this.rating = rating; }

    }
}

