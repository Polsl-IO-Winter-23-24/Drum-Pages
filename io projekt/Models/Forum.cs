using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

namespace io_projekt.Models
{
    public class Thread
    {
        private int id;

        private string theme;

        private DateTime date;

        private int userID;

        public int getID() { return id; }
        public string getTheme() { return theme; }
        public DateTime getDate() { return date; }

        public int getUserID() { return userID; }

        private const String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

        private static IMemoryCache _cache; // Pole statyczne przechowuj�ce pami�� podr�czn�
        private static int maxId;

        private Thread() { }

        private Thread(int id, string theme, DateTime date, int userID)
        {
            this.id = id;
            this.theme = theme;
            this.date = date;
            this.userID = userID;
        }

        private static IMemoryCache GetCacheInstance()
        {
            if (_cache == null)
            {
                _cache = new MemoryCache(new MemoryCacheOptions());
            }
            return _cache;
        }

        public static List<Thread> GetAllThreads()
        {
            IMemoryCache cache = GetCacheInstance();
            if (!cache.TryGetValue($"AllThreads", out List<Thread> cachedThreads))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String query = "select * from master.dbo.Watki";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                cachedThreads = new List<Thread>();
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    String theme = reader.GetString(1);
                                    DateTime date = reader.GetDateTime(2);
                                    int userID = reader.GetInt32(3);
                                    cachedThreads.Add(new Thread(id, theme, date, userID));
                                    var cacheEntryOptions = new MemoryCacheEntryOptions
                                    {
                                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                                    };
                                    cache.Set($"AllThreads", cachedThreads, cacheEntryOptions);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in wrting from threads data base: " + ex.ToString());
                    return null;
                }
            }
            return cachedThreads;
        }
        public static (string message, bool boolean, int threadId) AddNewThread(string theme, DateTime date, int userID)
        {
            MainUser? organizor = MainUser.GetUserById(userID).user;
            if (organizor == null)
            {
                return (Constants.noUserFound, false, -1);
            }
            else
            {

                try
                {
                    int newThreadId = GetMaxThreadId() + 1;
                    if (newThreadId != -1)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "INSERT INTO master.dbo.Watki (temat, dataUtworzenia, uzytkownikId) VALUES (@Theme, @Date, @UserID);";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Theme", theme);
                                command.Parameters.AddWithValue("@Date", date);
                                command.Parameters.AddWithValue("@UserID", userID);
                                command.ExecuteNonQuery();
                            }
                        }
                        IMemoryCache cache = GetCacheInstance();
                        List<Thread> threadsFromCache = cache.Get<List<Thread>>("AllThreads");
                        if (threadsFromCache == null)
                        {
                            threadsFromCache = new List<Thread>();
                        }

                        threadsFromCache.Add(new Thread(newThreadId, theme, date, userID));
                        var cacheEntryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) //zapisanie usera na 10 min
                        };
                        cache.Set("AllThreads", threadsFromCache, cacheEntryOptions);
                        return (Constants.addNewThreadSucces, true, newThreadId);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in adding a new thread to the database: " + ex.ToString());
                    // Handle the exception as needed
                }
                return (Constants.addNewThreadError, false, -1);
            }
        }

        public static (string message, bool boolean) RemoveThread(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM master.dbo.Watki WHERE idWatku = @id";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        IMemoryCache cache = GetCacheInstance();
                        List<Thread> threadsFromCache = cache.Get<List<Thread>>("AllThreads");
                        if (threadsFromCache != null)
                        {
                            threadsFromCache.RemoveAll(ev => ev.id == id);
                            cache.Set("AllThreads", threadsFromCache);
                        }

                        return (Constants.RemoveThreadSucces, true);
                    }
                    else
                    {
                        return (Constants.RemoveThreadError, false);
                    }

                }
            }
            catch (Exception ex)
            {
                return (Constants.RemoveThreadError + ": " + ex.Message, false);
            }
        }
        private static bool updateQuery(int threadId, string toUpdate, string newValue)
        {
            //zaktualizowanie pamieci cache 
            GetAllThreads();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"UPDATE master.dbo.Watki SET {toUpdate} = @newValue WHERE idWatku = @threadId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@threadId", threadId);
                    command.ExecuteNonQuery();
                    IMemoryCache cache = GetCacheInstance();
                    List<Thread> threadsFromCache = cache.Get<List<Thread>>("AllThreads");
                    if (threadsFromCache == null)
                    {
                        threadsFromCache = new List<Thread>();
                    }
                    Thread threadToUpdate = threadsFromCache.Find(e => e.id == threadId);
                    if (threadToUpdate != null)
                    {
                        //tu case dla kazdego przypadku 
                        switch (toUpdate)
                        {
                            case "temat":
                                threadToUpdate.theme = newValue;
                                break;
                            case "dataUtworzenia":
                                threadToUpdate.date = DateTime.Parse(newValue);
                                break;
                            case "uzytkownikId":
                                if (MainUser.UserExists(int.Parse(newValue)))
                                {
                                    threadToUpdate.userID = int.Parse(newValue);
                                }
                                else
                                {
                                    return (false);
                                }
                                break;

                        }
                        var cacheEntryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                        };
                        cache.Set("AllThreads", threadsFromCache, cacheEntryOptions);
                        return (true);
                    }
                    return (true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
                return (false);
            }
        }


        public static int GetMaxThreadId()
        {
            int maxThreadId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = "SELECT MAX(idWatku) FROM master.dbo.Watki";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            maxThreadId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("B��d podczas pobierania maksymalnej warto�ci idWatku: " + ex.Message);
            }
            maxId = maxThreadId;
            return maxThreadId;
        }

        public static List<int> GetThreadIds() {
            List<int> threadIds = new List<int>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "SELECT idWatku FROM master.dbo.Watki";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                threadIds.Add(id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("B��d podczas pobierania maksymalnej warto�ci idWatku: " + ex.Message);
            }
            return threadIds;
        }

    }



    public class Post
    {
        private int id;
        private string content;
        private DateTime creationDate;
        private int threadID;
        private int userID;

        public int getID() { return id; }
        public string getContent() { return content; }
        public int getUserID() { return userID; }
        public int getThreadID() { return threadID; }
        public DateTime getCreationDate() { return creationDate; }

        private const String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

        private static IMemoryCache _cache; // Pole statyczne przechowuj�ce pami�� podr�czn�
        private static int maxId;

        private Post() { }

        private Post(int id, string content, DateTime creationDate, int threadID, int userID)
        {
            this.id = id;
            this.content = content;
            this.creationDate = creationDate;
            this.threadID = threadID;
            this.userID = userID;
        }
        private static IMemoryCache GetCacheInstance()
        {
            if (_cache == null)
            {
                _cache = new MemoryCache(new MemoryCacheOptions());
            }
            return _cache;
        }

        public static List<Post> GetAllPosts()
        {
            IMemoryCache cache = GetCacheInstance();
            if (!cache.TryGetValue("AllPosts", out List<Post> cachedPosts))
            {

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string queryString = $"SELECT * FROM master.dbo.Wpisy";
                        using (SqlCommand command = new SqlCommand(queryString, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                cachedPosts = new List<Post>();
                                while (reader.Read())
                                {
                                    //Wczytanie danych z bazy 
                                    int postId = reader.GetInt32(0);
                                    string content = reader.GetString(1);
                                    DateTime creationDate = reader.GetDateTime(2);
                                    int threadId = reader.GetInt32(3);
                                    int userId = reader.GetInt32(3);

                                    //stworzenie nowego obiektu typu user i wpisanie go do cache
                                    cachedPosts.Add(new Post(postId, content, creationDate, threadId, userId));
                                    var cacheEntryOptions = new MemoryCacheEntryOptions
                                    {
                                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) //uzytkownikow do pamieci na 10 min
                                        //AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                                    };
                                    cache.Set("AllPosts", cachedPosts, cacheEntryOptions);
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in wrting from posts data base: " + ex.ToString());
                    return cachedPosts;
                }
            }
            return cachedPosts;
        }


        public static List<Post> GetPostsByThreadId(int threadId)
        {
            IMemoryCache cache = GetCacheInstance();
            if (!cache.TryGetValue($"Thread_{threadId}", out List<Post> cachedPosts))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string queryString = $"SELECT * FROM master.dbo.Wpisy WHERE idWatku = {threadId}";
                        using (SqlCommand command = new SqlCommand(queryString, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                cachedPosts = new List<Post>();
                                while (reader.Read())
                                {
                                    //Wczytanie danych z bazy 
                                    int postId = reader.GetInt32(0);
                                    string content = reader.GetString(1);
                                    DateTime date = reader.GetDateTime(2);
                                    int threadID = reader.GetInt32(3);
                                    int userId = reader.GetInt32(4);

                                    //stworzenie noego obiekty typu user i wpisanie go do cache
                                    cachedPosts.Add(new Post(postId, content, date, threadID, userId));
                                    var cacheEntryOptions = new MemoryCacheEntryOptions
                                    {
                                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) //zapisanie usera na 10 min
                                    };
                                    cache.Set($"Thread_{threadId}", cachedPosts, cacheEntryOptions);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return cachedPosts;
        }

        public static (String message, bool boolean) AddNewPost(String content, DateTime creationDate, int threadId, int userID)
        {
            MainUser? organizor = MainUser.GetUserById(userID).user;
            if (organizor == null)
            {
                return (Constants.noUserFound, false);
            }
            else
            {
                try
                {
                    int id = GetMaxPostId() + 1;
                    if (id != -1)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "INSERT INTO master.dbo.Wpisy (zawartosc, dataUtworzenia, idWatku, uzytkownikId) VALUES (@content, @date,@threadId,@userID)";
                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@content", content);
                            command.Parameters.AddWithValue("@date", creationDate);
                            command.Parameters.AddWithValue("@threadId", threadId);
                            command.Parameters.AddWithValue("@userID", userID);
                            command.ExecuteNonQuery();

                            // Update cache only if database insertion is successful
                            IMemoryCache cache = GetCacheInstance();

                            // Use the correct cache key for the thread
                            List<Post> postsFromCache = cache.Get<List<Post>>($"Thread_{threadId}");

                            if (postsFromCache == null)
                            {
                                postsFromCache = new List<Post>();
                            }

                            postsFromCache.Add(new Post(id, content, creationDate, threadId, userID));
                            var cacheEntryOptions = new MemoryCacheEntryOptions
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Save the post for 10 minutes
                            };

                            // Update the cache for the specific thread
                            cache.Set($"Thread_{threadId}", postsFromCache, cacheEntryOptions);

                            return (Constants.addNewPostSuccess, true);
                        }
                    }
                    else
                    {
                        return (Constants.addNewPostError, false);
                    }
                }
                catch (Exception ex)
                {
                    return (Constants.addNewPostError + ": " + ex.Message, false);
                }
            }
        }

        public static (string message, bool boolean) RemovePost(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM master.dbo.Wpisy WHERE idWpisu = @id";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        IMemoryCache cache = GetCacheInstance();
                        int threadId = GetThreadIdByPostId(id);

                        List<Post> postsFromCache = cache.Get<List<Post>>($"Thread_{threadId}");
                        if (postsFromCache != null)
                        {
                            postsFromCache.RemoveAll(ev => ev.id == id);
                            cache.Set("Thread_{threadId}", postsFromCache);
                        }

                        return (Constants.deletePostSuccess, true);
                    }
                    else
                    {
                        return (Constants.deletePostError, false);
                    }

                }
            }
            catch (Exception ex)
            {
                return (Constants.deletePostError + ": " + ex.Message, false);
            }
        }


        private static bool updateQuery(int postId, string toUpdate, string newValue)
        {
            //zaktualizowanie pamieci cache 
            GetAllPosts();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"UPDATE master.dbo.Wpisy SET {toUpdate} = @newValue WHERE idWpisu = @postId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@postId", postId);
                    command.ExecuteNonQuery();
                    IMemoryCache cache = GetCacheInstance();
                    List<Post> postsFromCache = cache.Get<List<Post>>("AllPosts");
                    if (postsFromCache == null)
                    {
                        postsFromCache = new List<Post>();
                    }
                    Post postToUpdate = postsFromCache.Find(e => e.id == postId);
                    if (postToUpdate != null)
                    {
                        //tu case dla kazdego przypadku 
                        switch (toUpdate)
                        {
                            case "zawartosc":
                                postToUpdate.content = newValue;
                                break;
                            case "dataUtworzenia":
                                postToUpdate.creationDate = DateTime.Parse(newValue);
                                break;
                            case "idWatku":
                                postToUpdate.threadID = int.Parse(newValue);
                                break;
                            case "uzytkownikId":
                                if (MainUser.UserExists(int.Parse(newValue)))
                                {
                                    postToUpdate.userID = int.Parse(newValue);
                                }
                                else
                                {
                                    return (false);
                                }
                                break;

                        }
                        var cacheEntryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                        };
                        cache.Set("AllPosts", postsFromCache, cacheEntryOptions);
                        return (true);
                    }
                    return (true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
                return (false);
            }
        }



        public static int GetThreadIdByPostId(int id) { 
            int threadId = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = "SELECT idWatku FROM master.dbo.Wpisy WHERE idWpisu = @id";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            threadId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("B��d podczas pobierania maksymalnej warto�ci idWpisu: " + ex.Message);
            }
            return threadId;
        }

        public static int GetMaxPostId()
        {
            int maxPostId = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = "SELECT MAX(idWpisu) FROM master.dbo.Wpisy";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            maxPostId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("B��d podczas pobierania maksymalnej warto�ci idWpisu: " + ex.Message);
            }
            return maxPostId;
        }



    }
}