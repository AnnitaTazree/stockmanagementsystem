using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace stockmanagementsystem.Pages
{
    public class InsertsuppliersModel : PageModel
    {
        public SuppliersInfo suppliersInfo = new SuppliersInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            suppliersInfo.suppliersname = Request.Form["suppliersName"];
            suppliersInfo.suppliersid = Request.Form["SuppliersId"];
            suppliersInfo.phone = Request.Form["Phone"];
            suppliersInfo.address = Request.Form["Address"];
            suppliersInfo.category = Request.Form["Category"];

            if (suppliersInfo.suppliersname.Length == 0 || suppliersInfo.phone.Length == 0 ||
               suppliersInfo.address.Length == 0 || suppliersInfo.category.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save the new client into the database

            try
            {
                string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string Sql = "INSERT INTO suppliers" +
                               "(suppliersname,phone,address,category) VALUES" +
                               "(@suppliersname,@phone,@address,@category);";
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {

                        // command.Parameters.AddWithValue("@suppliersid", suppliersInfo.suppliersid);
                        command.Parameters.AddWithValue("@suppliersname", suppliersInfo.suppliersname);
                        command.Parameters.AddWithValue("@phone", suppliersInfo.phone);
                        command.Parameters.AddWithValue("@address", suppliersInfo.address);
                        command.Parameters.AddWithValue("@category", suppliersInfo.category);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            suppliersInfo.suppliersid = ""; suppliersInfo.suppliersname = ""; suppliersInfo.phone = ""; suppliersInfo.address = ""; suppliersInfo.category = "";
            successMessage = "New Info Added Correctly";

            Response.Redirect("/suppliers-info?Category=null");
        }
    }





}
