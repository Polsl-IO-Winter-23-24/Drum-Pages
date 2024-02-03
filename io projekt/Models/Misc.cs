//Klasa na rozne roznosci 
//pobrać dostępne sprzety- odczytanie słownika ✅
//pobrać sprzętu danego userea  ✅
//dodac sprzet do słownika
//dodać sprzet do usera ze słownika 
//usunąć sprzet ze słownika- admin - NAJLEPIEJ W TO MIEJSCE WPISAC COS NA ZASADZIE NIEDOSTEPNE 
//usunąć sprzet od Usera 
//edycja w slowniku - admin
//edycja dla usera




using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlClient;

namespace io_projekt.Models
{

    public struct Gear
    {
        public Gear(int id, string name)
        {
            ID = id;
            NAME = name;
        }
        public int ID { get; }
        public string NAME { get; }
    }

    public struct Style
    {
        public Style(int id, string name)
        {
            ID = id;
            NAME = name;
        }
        public int ID { get; }
        public string NAME { get; }
    }



    public static class Misc
    {
        private const string connectionString = "Data Source=(local)\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        public static List<Gear> GetAllGear()
        {
            List<Gear> gearList = new List<Gear>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = $"SELECT * FROM master.dbo.Sprzet";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {                       
                            while (reader.Read())
                            {
                                //Wczytanie danych z bazy 
                                int dataId = reader.GetInt32(0);
                                string dataName = reader.GetString(1);                             
                                gearList.Add(new Gear(dataId,dataName)); 
                            }
                            return (gearList);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        public static List<Style> GetAllStyles()
        {
            List<Style> styleList = new List<Style>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = $"SELECT * FROM master.dbo.Style";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Wczytanie danych z bazy 
                                int dataId = reader.GetInt32(0);
                                string dataName = reader.GetString(1);
                                styleList.Add(new Style(dataId, dataName));
                            }
                            return (styleList);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return (null);
            }
        }


        public static Gear GetGearById(int idSprzetu)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = $"SELECT * FROM master.dbo.Sprzet WHERE idSprzetu = {idSprzetu}";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int dataId = reader.GetInt32(0);
                                string dataName = reader.GetString(1);
                                return (new Gear(dataId, dataName));
                            }
                            return (new Gear(-1,"err"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return (new Gear(-1,ex.Message));
            }           
        }


        public static Style GetStyleById(int idStylu)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = $"SELECT * FROM master.dbo.Style WHERE idStylu = {idStylu}";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int dataId = reader.GetInt32(0);
                                string dataName = reader.GetString(1);
                                return (new Style(dataId, dataName));
                            }
                            return (new Style(-1, "err"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return (new Style(-1, ex.Message));
            }


        }


        public static List<Gear> GetUserGear(int userId) 
        {
            List<Gear> list = new List<Gear>();
            List<int> gearIds = new List<int>();

            if(!MainUser.UserExists(userId))
            {
                return list;

             }
            else {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = $"SELECT idSprzetu FROM master.dbo.SprzetyUzytkownik WHERE uzytkownikId = {userId}";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int dataId = reader.GetInt32(0);
                                gearIds.Add(dataId);
                            }

                        }
                    }
                }

                foreach (var a in gearIds)
                {
                   list.Add(GetGearById(a));
                }
            }
            catch (Exception ex) {
                list.Add(new Gear(-1, ex.Message));
                return list;
            
            }
              return list;
            }
        }

        public static List<Style> GetUserStyle(int userId)
        {

            List<Style> list = new List<Style>();
            List<int> styleIds = new List<int>();

            if (!MainUser.UserExists(userId))
            {
                return list;

            }
            else
            {

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string queryString = $"SELECT stylId FROM master.dbo.StyleUzytkownik WHERE uzytkownikId = {userId}";
                        using (SqlCommand command = new SqlCommand(queryString, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    int dataId = reader.GetInt32(0);
                                    styleIds.Add(dataId);
                                }

                            }
                        }
                    }

                    foreach (var a in styleIds)
                    {
                        list.Add(GetStyleById(a));
                    }
                }
                catch (Exception ex)
                {
                    list.Add(new Style(-1, ex.Message));
                    return list;

                }
                return list;
            }

        }


        public static void AddGear(string name) //na HashSet<string> .add zwraca true false
        {
           
        }

        public static void AddUserGear(int userId, int gearId)
        { 
        
        }

        public static void RemoveGear(int id)
        { 
        
        }

        public static void RemoveUserGear(int userId, int gearId)
        { 
        
        }

        public static void EditGear(int gearId, string name)
        { 
        
        }

        public static void EditUserGear(int userId, int gearId)
        { 

        }









    }
}
