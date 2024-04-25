using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace _560Project.Pages
{
    public class GamesAndConsolesModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int GameConsoleIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int GameIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ConsoleIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UnitPriceFilter { get; set; }

        public List<GameConsoleInfo> listGameConsoles = new List<GameConsoleInfo>();
        public List<GameConsoleInfo> infoToAdd = new List<GameConsoleInfo>();
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
                                gameConsole.UnitPrice = reader.GetInt32(4);
                                infoToAdd.Add(gameConsole);
                                if (GameConsoleIDFilter != 0)
                                {

                                    infoToAdd = infoToAdd.Where(p => p.GameConsoleID == GameConsoleIDFilter).ToList();
                                    

                                }
                                if (GameIDFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.GameID == GameIDFilter).ToList();

                                }
                                if (ConsoleIDFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.ConsoleID == ConsoleIDFilter).ToList();

                                }
                                if (!String.IsNullOrEmpty(TitleFilter))
                                {
                                    infoToAdd = infoToAdd.Where(p => p.Title.Contains(TitleFilter)).ToList();

                                }
                                if (UnitPriceFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.UnitPrice == UnitPriceFilter).ToList();

                                }
                                listGameConsoles = infoToAdd.ToList();

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
        public int UnitPrice;
    }
}
