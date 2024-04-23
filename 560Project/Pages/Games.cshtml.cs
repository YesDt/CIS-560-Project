using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
   

  

    public class GamesModel : PageModel
    {
        public List<GameInfo> listGames = new List<GameInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=CISProject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Store.Game";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                GameInfo game = new GameInfo();
                                game.GameId = reader.GetInt32(0);
                                game.GameName = reader.GetString(1);
                                game.PublisherID = reader.GetInt32(2);
                                game.UserRating = reader.GetInt32(3);
                                listGames.Add(game);
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

    public class GameInfo
    {
        public int GameId;
        public string GameName;
        public int PublisherID;
        public int UserRating;
    }
}
