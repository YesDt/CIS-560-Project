using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _560Project.Pages
{
    public class StoresModel : PageModel
    {
        public List<StoreInfo> listStores = new List<StoreInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=CISProject;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Company.Stores";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StoreInfo store = new StoreInfo();
                                store.StoreID = reader.GetInt32(0);
                                store.StoreName = reader.GetString(1);
                                listStores.Add(store);
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

    public class StoreInfo
    {
        public int StoreID;
        public string StoreName;
    }
}




/*using Microsoft.AspNetCore.Mvc;
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


@page
@model _560Project.Pages.InventoriesModel
@{
    ViewData["Title"] = "Inventory";
}
<p>This shows the inventory; the games and consoles, and the stores.</p>
<table class="table">
<thead>
    <tr>
        <th>InventoryID</th>
        <th>GameConsoleID</th>
        <th>StoreID</th>
    </tr>
</thead>
<tbody>
    @foreach (var inventory in Model.listInventories)
    {
        <tr>
            <td>@inventory.InventoryID</td>
            <td>@inventory.GameConsoleID</td>
            <td>@inventory.StoreID</td>
        </tr>
    }
</tbody>
</table>
*/