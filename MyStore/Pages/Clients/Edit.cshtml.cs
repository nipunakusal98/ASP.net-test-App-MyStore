using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=LAPTOP-JDO92P0U\\SQLEXPRESS02;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                               
                                clientInfo.id = "" + rdr.GetInt32(0);
                                clientInfo.name = rdr.GetString(1);
                                clientInfo.email = rdr.GetString(2);
                                clientInfo.phone = rdr.GetString(3);
                                clientInfo.address = rdr.GetString(4);
                               

                                

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }

        }

        public void OnPost() 
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All fields must be filled";
                return;
            }
            try
            {
                String connectionString = "Data Source=LAPTOP-JDO92P0U\\SQLEXPRESS02;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String sql = "UPDATE clients SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@ID";

                    using (SqlCommand cmd = new SqlCommand(sql,conn))
                    {

                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);
                        cmd.Parameters.AddWithValue("@id", clientInfo.id);

                        cmd.ExecuteNonQuery();

                    }
                }


            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("Clients/Clients");
        }
    }
}
