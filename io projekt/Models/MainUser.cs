
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlClient;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;

namespace io_projekt.Models
{
    public class MainUser
    {
        private int id;
        private string login;
        private string password;
        private string name;

        private string lastName;
        private int age;
        private string accountType;
        private int skills;

        private const string connectionString = "Data Source=(local)\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        private static IMemoryCache _cache; // Pole statyczne przechowujące pamięć podręczną
        private static int maxId;
        // Konstruktor prywatny, aby zapobiec utworzeniu wielu instancji tej klasy
        private MainUser() { }

        private MainUser(int id, string login, string password, string name, string lastName, int age, string type, int skills ) { 
            this.id = id;
            this.login = login;     
            this.password = password;
            this.name = name;
            this.lastName = lastName;
            this.age = age;
            this.accountType = type;
            this.skills = skills;
        
        }

        
        public static (string message, int id) GetIdFromLogin(string login)
        {
            List<MainUser> users = GetAllUsers();
            int index = users.FindIndex(user => user.login == login);
            if (index != -1)
            {
                return (Constants.getUserIdSucces,users[index].id);
            }
            else
            {
                return (Constants.getUserIdError, -1);
            }

        }
        public int getId() 
        {    
            return id; 
        }   
        public string getLogin()
        {
            return login;
        }
        public string getPassword()
        {
            return password;
        }
        public string getName()
        {
        return name;    
        }
        public string getLastName()
        {
            return lastName;
        }
        public int getAge()
        {
            return age;
        }
        public string getAccountType() 
        {
            return accountType;
        }
        public int getSkills()
        {
            return skills;
        }


        private static IMemoryCache GetCacheInstance()
        {
            if (_cache == null)
            {             
                _cache = new MemoryCache(new MemoryCacheOptions());
            }
            return _cache;
        }

   
        public static (MainUser ?user, String message) GetUserById(int id)
        {
            IMemoryCache cache = GetCacheInstance();
            if (!cache.TryGetValue($"User_{id}", out MainUser cachedUser))
            {
                try
                {                  
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string queryString = $"SELECT * FROM master.dbo.Uzytkownicy WHERE userId = {id}";
                        using (SqlCommand command = new SqlCommand(queryString, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    //Wczytanie danych z bazy 
                                    int dataId = reader.GetInt32(0);
                                    string dataLogin = reader.GetString(1);
                                    string dataPassword = reader.GetString(2);
                                    string dataName = reader.GetString(3);
                                    string dataLastName = reader.GetString(4);
                                    int dataAge = reader.GetInt32(5);
                                    string dataAccountType = reader.GetString(6);
                                    int dataSkills = reader.GetInt32(7);
                                    Console.WriteLine("pierwsze odczytanie " + dataName);
                                    //stworzenie noego obiekty typu user i wpisanie go do cache
                                    cachedUser = new MainUser(dataId, dataLogin, dataPassword, dataName, dataLastName, dataAge, dataAccountType, dataSkills);
                                    var cacheEntryOptions = new MemoryCacheEntryOptions
                                    {
                                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) //zapisanie usera na 10 min
                                    };
                                    cache.Set($"User_{id}", cachedUser, cacheEntryOptions);

                                }
                                else 
                                {
                                    return (null,Constants.emptyTable);
                                }
                            }
                        }      
                    }
                }
                catch (Exception ex)
                {    
                    return (null, Constants.dataBaseException + " " + ex.ToString());
                }         
            }    
            return (cachedUser,Constants.getUserSucces);
        }

        public static List<MainUser> GetAllUsers()
        {        
            IMemoryCache cache = GetCacheInstance();
            if (!cache.TryGetValue("AllUsers", out List<MainUser> cachedUsers))
            {
               
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string queryString = $"SELECT * FROM master.dbo.Uzytkownicy";
                        using (SqlCommand command = new SqlCommand(queryString, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                cachedUsers = new List<MainUser>();
                                while (reader.Read())
                                {
                                    //Wczytanie danych z bazy 
                                    int dataId = reader.GetInt32(0);
                                    string dataLogin = reader.GetString(1);
                                    string dataPassword = reader.GetString(2);
                                    string dataName = reader.GetString(3);
                                    string dataLastName = reader.GetString(4);
                                    int dataAge = reader.GetInt32(5);
                                    string dataAccountType = reader.GetString(6);
                                    int dataSkills = reader.GetInt32(7);
                                    Console.WriteLine("Imie:  " + dataName);
                                    //stworzenie nowego obiektu typu user i wpisanie go do cache
                                    cachedUsers.Add(new MainUser(dataId, dataLogin, dataPassword, dataName, dataLastName, dataAge, dataAccountType, dataSkills));
                                    var cacheEntryOptions = new MemoryCacheEntryOptions
                                    {
                                       AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) //uzytkownikow do pamieci na 10 min
                                        //AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                                    };
                                    cache.Set("AllUsers", cachedUsers, cacheEntryOptions);
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in wrting from users data base: " + ex.ToString());
                    return cachedUsers;
                }
            }
            return cachedUsers;
        }


        public static (string message, bool boolean) AddNewUser(string login, string password, string name, string lastName, int age, string type, int skills)
        {
            if (ValidateLogin(login) && ValidatePassword(password))
            {
                try
                {
                    int newUserId = GetMaxUserId() + 1;
                    if(newUserId != -1)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "INSERT INTO master.dbo.Uzytkownicy (login, haslo, imie, nazwisko, wiek, rodzajKonta, umiejetnosci) VALUES (@login, @password,@name,@lastName,@age,@type,@skills)";
                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@login", login);
                            command.Parameters.AddWithValue("@password", password);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@lastName", lastName);
                            command.Parameters.AddWithValue("@age", age);
                            command.Parameters.AddWithValue("@type", type);
                            command.Parameters.AddWithValue("@skills", skills);
                            command.ExecuteNonQuery();
                        }

                        IMemoryCache cache = GetCacheInstance();
                        List<MainUser> usersFromCache = cache.Get<List<MainUser>>("AllUsers");
                        if (usersFromCache == null)
                        {
                            usersFromCache = new List<MainUser>();
                        }

                        usersFromCache.Add(new MainUser(newUserId, login, password, name, lastName, age, type, skills));
                        var cacheEntryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) //zapisanie usera na 10 min
                        };
                        cache.Set("AllUsers", usersFromCache, cacheEntryOptions);
                        return (Constants.addNewUserSucces,true);
                    }
                    else 
                    { 
                        return (Constants.addNewUserError,false); 
                    }
                }
                catch (Exception ex)
                {
                    return (Constants.dataBaseException + ": " + ex.Message,false);
                    
                } 
            }
            else
            {

                return (Constants.addNewUserError + ": " + Constants.badLoginPassword, false);
            }

           


        }


        //Czy login jest poprawny- czy zajety, dlugosc
        public static bool ValidateLogin(string login) 
        {
            var users = GetAllUsers();
            //TODO
            //Bardziej konkretne wymagania co do loginu- teraz tylko dlogosc i powtarzanie 
            if (login.Length >= 3 && login.Length <= 50)
            {
                if (users.Any(user => user.getLogin() == login)) //GetAllUsers().Any(user => user.getlogin() == login)
                {
                    Console.WriteLine($"Znaleziono powtórzenie dla loginu: {login}");
                    return false;
                }
                else
                {
                    
                    Console.WriteLine("lOGIN jest wolny");
                    return true; //jezeli login jest zajety
                }               
            }        
            else
            {
                Console.WriteLine("zla dlugosc");
                return false;
            }

     
        }

        public static int GetUserIdByLogin(string login)
        {
            int userId = -1; // Domyślna wartość, jeśli nie znajdzie użytkownika

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT userId FROM master.dbo.Uzytkownicy WHERE login = @login";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@login", login);

                    // Wykonanie zapytania i odczytanie ID użytkownika
                    object result = command.ExecuteScalar();

                    // Sprawdzenie czy wynik zapytania jest niepusty i jest liczbą całkowitą
                    if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out int id))
                    {
                        userId = id;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas pobierania ID użytkownika: " + ex.Message);
            }
            Console.WriteLine("odczytanie id z bazy:");
            return userId;
        }


        //Czy haslo jest poprawne - regex
        public static bool ValidatePassword(string password)
        {
            if (password.Length >= 3 && password.Length <= 50)
            {
                Console.WriteLine("HASLO OK");
                return true;
            }
            else
            {
                return false;
            }
            
        }

       // //czy user jest w bazie- zwraca id-> do logowania można wykorzystac metodę GetUserIdByLogin
       // public int findUser(string login) 
       // {        
       //     return 1;
       // }

        //Czy podane podczas logowania hasło jest poprawne 
        public static bool CheckPassword(string login,string password)
        {
            int id = GetUserIdByLogin(login);
            if (id != -1) //user found
            {
                if (GetUserById(id).user.getPassword() == password) //correct password
                {
                    return true;
                }
                else //bad password
                { 
                return false;
                }
            }
            else
            {
                return false; //no user found
            }

        }


        public static int GetMaxUserId()
        {
            int maxUserId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = "SELECT MAX(userId) FROM master.dbo.Uzytkownicy";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            maxUserId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas pobierania maksymalnej wartości userId: " + ex.Message);
            }
            maxId = maxUserId;  
            return maxUserId;
        }


        public static (string message, bool boolean) TestPar()
        {
            String str = "Wiadomosc"; 
            bool b = false;
            return (str, b);
        }



    }
}
