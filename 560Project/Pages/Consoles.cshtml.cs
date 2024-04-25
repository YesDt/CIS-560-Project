using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class ConsolesModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int ConsoleIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ConsoleNameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PublisherIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UserRatingFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UnitPriceFilter { get; set; }

        public List<ConsoleInfo> listConsoles = new List<ConsoleInfo>();
        public List<ConsoleInfo> infoToAdd = new List<ConsoleInfo>();
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
                                console.UnitPrice = reader.GetInt32(4);
                                infoToAdd.Add(console);
                                if (ConsoleIDFilter != 0)
                                {

                                    infoToAdd = infoToAdd.Where(p => p.ConsoleID == ConsoleIDFilter).ToList();


                                }
                                if (!String.IsNullOrEmpty(ConsoleNameFilter))
                                {
                                    infoToAdd = infoToAdd.Where(p => p.ConsoleName.Contains(ConsoleNameFilter)).ToList();

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
                                listConsoles = infoToAdd.ToList();
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
        public int UnitPrice;

    }
}
