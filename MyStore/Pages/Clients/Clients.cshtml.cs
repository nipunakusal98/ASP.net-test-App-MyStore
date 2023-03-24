using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class ClientsModel : PageModel
    {
        public List<ClientInfo> ClientList = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-JDO92P0U\\SQLEXPRESS02;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String sql = "SELECT * FROM clients";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + rdr.GetInt32(0);
                                clientInfo.name = rdr.GetString(1);
                                clientInfo.email = rdr.GetString(2);
                                clientInfo.phone = rdr.GetString(3);
                                clientInfo.address = rdr.GetString(4);
                                clientInfo.created_at = rdr.GetDateTime(5).ToString();

                                ClientList.Add(clientInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }


    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;

    }

}
