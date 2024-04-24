using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class InventoriesModel : PageModel
    {
        public List<InventoryInfo> listInventories = new List<InventoryInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=CISProject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Store.Inventory";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InventoryInfo inventory = new InventoryInfo();
                                inventory.InventoryID = reader.GetInt32(0);
                                inventory.GameConsoleID = reader.GetInt32(1);
                                inventory.StoreID = reader.GetInt32(2);
                                listInventories.Add(inventory);
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

    public class InventoryInfo
    {
        public int InventoryID;
        public int GameConsoleID;
        public int StoreID;
    }
}
