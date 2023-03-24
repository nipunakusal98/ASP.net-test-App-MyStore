using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages
{
    public class CustomersModel : PageModel
    {
        public List <CustomerInfo> CustomerList =new List<CustomerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-JDO92P0U\\SQLEXPRESS02;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "SELECT * FROM clients";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.id = "" + rdr.GetInt32(0);
                                customerInfo.name = rdr.GetString(1);
                                customerInfo.email = rdr.GetString(2);
                                customerInfo.phone = rdr.GetString(3);
                                customerInfo.address = rdr.GetString(4);
                                customerInfo.created_at = rdr.GetDateTime(5).ToString();

                                CustomerList.Add(customerInfo);

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

    public class CustomerInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;

    }
}
