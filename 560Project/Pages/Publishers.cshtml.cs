using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class PublishersModel : PageModel
    {
        public List<PublisherInfo> listPublishers = new List<PublisherInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=CISProject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Store.Publisher";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PublisherInfo publisher = new PublisherInfo();
                                publisher.PublisherID = reader.GetInt32(0);
                                publisher.PublisherName = reader.GetString(1);
                                listPublishers.Add(publisher);
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

    public class PublisherInfo
    {
        public int PublisherID;
        public string PublisherName;
    }
}
