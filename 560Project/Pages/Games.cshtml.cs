using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace _560Project.Pages
{




    public class GamesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int GameIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? GameNameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PublisherIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UserRatingFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UnitPriceFilter { get; set; }

        public List<GameInfo> listGames = new List<GameInfo>();
        public List<GameInfo> infoToAdd = new List<GameInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=CISProject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT G.GameID, G.GameName, G.PublisherID, G.UserRating, GC.UnitPrice FROM Store.Game G LEFT JOIN Store.GameConsole GC ON GC.GameID = G.GameID";
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
                                game.UnitPrice = reader.GetInt32(4);
                                infoToAdd.Add(game);
                                if (GameIDFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.GameId == GameIDFilter).ToList();
                                }
                                if (!String.IsNullOrEmpty(GameNameFilter))
                                {
                                    infoToAdd = infoToAdd.Where(p => p.GameName.Contains(GameNameFilter)).ToList();

                                }
                                if (PublisherIDFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.PublisherID == PublisherIDFilter).ToList();

                                }
                                if (UserRatingFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.UserRating == UserRatingFilter).ToList();

                                }
                                if (UnitPriceFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.UnitPrice == UnitPriceFilter).ToList();

                                }
                                listGames = infoToAdd.ToList();
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
        public int UnitPrice;
    }
}
