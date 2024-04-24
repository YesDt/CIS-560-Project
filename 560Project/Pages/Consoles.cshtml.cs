using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class ConsolesModel : PageModel
    {
        public List<ConsoleInfo> listConsoles = new List<ConsoleInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=CISProject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Store.Console";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ConsoleInfo console = new ConsoleInfo();
                                console.ConsoleID = reader.GetInt32(0);
                                console.ConsoleName = reader.GetString(1);
                                console.PublisherID = reader.GetInt32(2);
                                console.UserRating = reader.GetInt32(3);
                                console.UserPrice = reader.GetInt32(4);
                                console.Quantity = reader.GetInt32(5);
                                listConsoles.Add(console);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    public class ConsoleInfo
    {
        public int ConsoleID;
        public string ConsoleName;
        public int PublisherID;
        public int UserRating;
        public int UserPrice;
        public int Quantity;
    }
}
