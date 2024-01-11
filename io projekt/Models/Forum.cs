using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

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

        private static IMemoryCache _cache; // Pole statyczne przechowuj¹ce pamiêæ podrêczn¹
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

        public static List<Thread> getAllThreads()
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
        public int getThreadID() { return threadID; }
        public DateTime getCreationDate() {return creationDate;}

        private const String connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

        private static IMemoryCache _cache; // Pole statyczne przechowuj¹ce pamiêæ podrêczn¹
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

        public static List<Post> GetPostsByThreadId(int threadId) {
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


    }


}