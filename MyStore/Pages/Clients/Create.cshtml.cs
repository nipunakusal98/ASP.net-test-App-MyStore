using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length ==0 || clientInfo.email.Length == 0 || clientInfo.phone.Length ==0 || clientInfo.address.Length == 0) 
            {
                errorMessage = "You must fill the required Fields.";
                return;
            }

            // save data to the db

            try
            {
                String connectionString = "Data Source=LAPTOP-JDO92P0U\\SQLEXPRESS02;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String query = "INSERT INTO clients (name,email,phone,address) VALUES (@name,@email,@phone,@address)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);

                        cmd.ExecuteNonQuery();
                    }
                }

               

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; 
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";

            successMessage = "New client Added sucessfully.";

            Response.Redirect("/Clients/Clients");
        }
    }
}
