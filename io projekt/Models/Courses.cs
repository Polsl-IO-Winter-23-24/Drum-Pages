using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading;

namespace io_projekt.Models
{
    public class Courses
    {


        public List<Course> courses = new List<Course>();
        public List<Course> Topcourses = new List<Course>();

        public List<Comment> comments = new List<Comment>();
        public void connectToDataBase()
        {
            try
            {

                String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Console.WriteLine("tabela: ");
                    connection.Open();
                    String query = "select * from Kursy";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Course course = new Course();
                                course.setID(reader.GetInt32(0));
                                course.setTitle(reader.GetString(1));
                                course.setDescription(reader.GetString(2));
                                course.setAuthorID(reader.GetInt32(3));
                                course.setDifficulty(reader.GetInt32(4));
                                course.setRating(reader.GetInt32(5));

                                courses.Add(course);
                            }
                        }
                    }

                    query = "select * from Kursy order by ocena desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Course course = new Course();
                                course.setID(reader.GetInt32(0));
                                course.setTitle(reader.GetString(1));
                                course.setDescription(reader.GetString(2));
                                course.setAuthorID(reader.GetInt32(3));
                                course.setDifficulty(reader.GetInt32(4));
                                course.setRating(reader.GetInt32(5));

                                Topcourses.Add(course);
                            }
                        }
                    }





                    query = "select * from Komentarze";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Comment com = new Comment();
                                com.setID(reader.GetInt32(0));
                                com.setClassID(reader.GetInt32(1));
                                com.setAuthorID(reader.GetInt32(2));
                                com.setCourseID(reader.GetInt32(3));
                                com.setContent(reader.GetString(4));
                                com.setRating(reader.GetInt32(5));

                                comments.Add(com);

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






        public class Course
        {
            private int id;
            private string title;
            private string description;
            private int authorID;
            private int difficutly;
            private int rating;

            public Course()
            {
                title = "";
                description = "";
            }

            public int getID() { return id; }
            public string getTitle() { return title; }
            public string getDescription() { return description; }
            public int getAuthorID() { return authorID; }
            public int getDifficulty() { return difficutly; }
            public int getRating() { return rating; }

            public void setID(int id) { this.id = id; }
            public void setTitle(string title) { this.title = title; }
            public void setDescription(string description) { this.description = description; }
            public void setAuthorID(int authorID) { this.authorID = authorID; }
            public void setDifficulty(int difficulty) { this.difficutly = difficulty; }
            public void setRating(int rating) { this.rating = rating; }

        }



        public class Comment
        {
            private int id;
            private int classID;
            private int authorID;
            private int courseID;
            private string content;
            private int rating;

            public Comment()
            {
                content = "";
            }

            public int getID() { return id; }
            public int getClassID() { return classID; }
            public int getAuthorID() { return authorID; }
            public int getCourseID() { return courseID; }
            public string getContent() { return content; }
            public int getRating() { return rating; }

            public void setID(int id) { this.id = id; }
            public void setClassID(int classID) { this.classID = classID; }
            public void setAuthorID(int authorID) { this.authorID = authorID; }
            public void setCourseID(int courseID) { this.courseID = courseID; }
            public void setContent(string content) { this.content = content; }
            public void setRating(int rating) { this.rating = rating; }

        }
    }
}
