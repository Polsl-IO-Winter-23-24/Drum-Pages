using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace io_projekt.Models
{
    public class Model
    {
      
        public List<Thread> Threads = new List<Thread>();
        public void connectToDataBase()
        {
            try
            {

                String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

                using (SqlConnection connection  = new SqlConnection(connectionString))
                {
                    Console.WriteLine("tabela: ");
                    connection.Open();
                    String query = "select * from master.dbo.Watki";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                Thread thread = new Thread();
                                thread.setID(reader.GetInt32(0));
                                thread.setTheme(reader.GetString(1));
                                thread.setCreationDate(reader.GetDateTime(2));
                                thread.setUserID(reader.GetInt32(3));

                                Threads.Add(thread);
                            }
                        }
                    }

                    query = "select * from master.dbo.Wpisy";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Post post = new Post();
                                post.setID(reader.GetInt32(0));
                                post.setContent(reader.GetString(1));
                                post.setCreationDate(reader.GetDateTime(2));
                                post.setThreadID(reader.GetInt32(3));
                                post.setUserID(reader.GetInt32(4));
                               
                                foreach (var thread in Threads) {
                                    if (thread.getID() == post.getThreadID()) { 
                                        List<Post> posts = thread.getPosts();
                                        posts.Add(post);
                                    }
                                }
                            }
                        }
                    }






                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


 
    public class Thread { 
        private int id;
        
        private string theme;

        private DateTime creationDate;
        
        private int userID;

        private List<Post> posts = new List<Post>();

        public int getID() { return id; }
        public string getTheme() { return theme; }
        public DateTime getCreationDate() { return creationDate; }

        public int getUserID() { return userID; }
        public void setID(int ID) {  this.id = ID; }
        public void setTheme(String theme) { this.theme = theme; }
        public void setUserID(int userID) { this.userID = userID; }
        
        public void setCreationDate(DateTime creationDate) {  this.creationDate = creationDate; }

        public List<Post> getPosts() { return posts; }
        public void setPosts(List<Post> posts) {  this.posts = posts; }


    }

    public class Post {
        private int id;
        private string content;
        private DateTime creationDate;
        private int threadID;
        private int userID;

        public int getID() { return id; }
        public string getContent() { return content; }
        public int getUserID() { return userID;}
        public void setID(int id) { this.id = id; }
        public int getThreadID() { return threadID; }

        public DateTime getCreationDate() {return creationDate;}

        public void setCreationDate(DateTime creationDate) { this.creationDate = creationDate; }
        public void setContent(String content) { this.content = content;}
        public void setThreadID(int threadID) {  this.threadID = threadID; }
        public void setUserID(int userID) { this.userID = userID; }
    }


}