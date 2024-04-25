using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class InventoriesModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int InventoryIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int GameConsoleIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int StoreIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int QuantityFilter { get; set; }

        public List<InventoryInfo> listInventories = new List<InventoryInfo>();
        public List<InventoryInfo> infoToAdd = new List<InventoryInfo>();
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
                                inventory.Quantity = reader.GetInt32(3);
                                infoToAdd.Add(inventory);
                                if (InventoryIDFilter != 0)
                                {

                                    infoToAdd = infoToAdd.Where(p => p.InventoryID == InventoryIDFilter).ToList();


                                }
                                if (GameConsoleIDFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.GameConsoleID == GameConsoleIDFilter).ToList();

                                }
                                if (StoreIDFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.StoreID == StoreIDFilter).ToList();

                                }
                                if (QuantityFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.Quantity == QuantityFilter).ToList();

                                }
                                listInventories = infoToAdd.ToList();
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
        public int Quantity;
    }
}
