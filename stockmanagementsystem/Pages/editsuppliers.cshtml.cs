using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace stockmanagementsystem.Pages
{
    public class editsuppliersModel : PageModel
    {

        public SuppliersInfo suppliersInfo = new SuppliersInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["SuppliersId"];
            try
            {
                string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM suppliers WHERE suppliersid=@suppliersid;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@suppliersid", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                suppliersInfo.suppliersid = "" + reader.GetInt32(0);
                                suppliersInfo.suppliersname = reader.GetString(1);
                                suppliersInfo.phone = reader.GetString(2);
                                suppliersInfo.address = reader.GetString(3);
                                suppliersInfo.category = reader.GetString(4);

                            }
                        }
                    }
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void OnPost()
        {
            suppliersInfo.suppliersid = Request.Form["inputSuppliersId"];
            suppliersInfo.suppliersname = Request.Form["inputSuppliersName"];
            suppliersInfo.phone = Request.Form["inputPhone"];
            suppliersInfo.address = Request.Form["inputAddress"];
            suppliersInfo.category = Request.Form["inputCategory"];

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
                    string Sql = "UPDATE suppliers" +
                            " SET suppliersname=@suppliersname,phone=@phone,address=@address,category=@category" +
                            " WHERE suppliersid=@suppliersid";

                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Parameters.AddWithValue("@suppliersid", suppliersInfo.suppliersid);
                        command.Parameters.AddWithValue("@suppliersname", suppliersInfo.suppliersname);
                        command.Parameters.AddWithValue("@phone", suppliersInfo.phone);
                        command.Parameters.AddWithValue("@address", suppliersInfo.address);
                        command.Parameters.AddWithValue("@category", suppliersInfo.category);

                        command.ExecuteNonQuery();



                    }
                    connection.Close();
                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/suppliers-info?Category=null");
        }

    }
}
