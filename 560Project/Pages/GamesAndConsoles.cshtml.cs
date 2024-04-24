using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class GamesAndConsolesModel : PageModel
    {
        public List<GameConsoleInfo> listGameConsoles = new List<GameConsoleInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=CISProject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Store.GameConsole";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GameConsoleInfo gameConsole = new GameConsoleInfo();
                                gameConsole.GameConsoleID = reader.GetInt32(0);
                                gameConsole.GameID = reader.GetInt32(1);
                                gameConsole.ConsoleID = reader.GetInt32(2);
                                gameConsole.Title = reader.GetString(3);
                                gameConsole.Quantity = reader.GetInt32(4);
                                gameConsole.UnitPrice = reader.GetInt32(5);
                                listGameConsoles.Add(gameConsole);
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

    public class GameConsoleInfo
    {
        public int GameConsoleID;
        public int GameID;
        public int ConsoleID;
        public string Title;
        public int Quantity;
        public int UnitPrice;
    }
}
