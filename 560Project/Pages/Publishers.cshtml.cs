using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static System.Formats.Asn1.AsnWriter;

namespace _560Project.Pages
{
    public class PublishersModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int PublisherIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? PublisherNameFilter { get; set; }

        public List<PublisherInfo> listPublishers = new List<PublisherInfo>();
        public List<PublisherInfo> infoToAdd = new List<PublisherInfo>();
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
                                infoToAdd.Add(publisher);
                                if (PublisherIDFilter != 0)
                                {

                                    infoToAdd = infoToAdd.Where(p => p.PublisherID == PublisherIDFilter).ToList();


                                }
                                if (!String.IsNullOrEmpty(PublisherNameFilter))
                                {
                                    infoToAdd = infoToAdd.Where(p => p.PublisherName.Contains(PublisherNameFilter)).ToList();

                                }
                                listPublishers = infoToAdd.ToList();
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
