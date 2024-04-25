using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class InventoriesModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int InventoryIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? GameConsoleIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int StoreIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ConsoleIDFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? QuantityFilter { get; set; }

        

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
                                try
                                {
                                    int gameTemp = reader.GetInt32(1);
                                }
                                catch (Exception e1)
                                { 
                                    inventory.ConsoleID = reader.GetInt32(3);
                                    inventory.StoreID = reader.GetInt32(2);
                                    inventory.Quantity = reader.GetInt32(4);
                                }
                                try
                                {
                                    int consoleTemp = reader.GetInt32(3);
                                }
                                catch (Exception e2)
                                {
                                    inventory.GameConsoleID = reader.GetInt32(1);
                                    inventory.StoreID = reader.GetInt32(2);
                                    inventory.Quantity = reader.GetInt32(4);
                                }
                                infoToAdd.Add(inventory);
                                if (InventoryIDFilter != 0)
                                {

                                    infoToAdd = infoToAdd.Where(p => p.InventoryID == InventoryIDFilter).ToList();


                                }
                                if (!String.IsNullOrEmpty(GameConsoleIDFilter))
                                {
                                    infoToAdd = infoToAdd.Where(p => p.GameConsoleID == int.Parse(GameConsoleIDFilter)).ToList();

                                }
                                if (StoreIDFilter != 0)
                                {
                                    infoToAdd = infoToAdd.Where(p => p.StoreID == StoreIDFilter).ToList();

                                }
                                if (!String.IsNullOrEmpty(ConsoleIDFilter))
                                {
                                    infoToAdd = infoToAdd.Where(p => p.ConsoleID == int.Parse(ConsoleIDFilter)).ToList();

                                }
                                if ((!String.IsNullOrEmpty(QuantityFilter)))
                                {
                                    infoToAdd = infoToAdd.Where(p => p.Quantity == int.Parse(QuantityFilter)).ToList();

                                }
                                listInventories = infoToAdd.ToList();
                            }
                        }
                    }
                }
            }
            catch (Exception e3)
            {

            }
        }
    }

    public class InventoryInfo
    {
        public int InventoryID;
        public int GameConsoleID;
        public int StoreID;
        public int ConsoleID;
        public int Quantity;
    }
}
