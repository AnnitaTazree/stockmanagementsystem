using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static stockmanagementsystem.Pages.stock_availableModel;


namespace stockmanagementsystem.Pages
{
    public class EditModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["ProductId"];
            try
            {
                string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM products WHERE productid=@productid;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@productid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //ProductInfo productInfo = new ProductInfo();
                                productInfo.productid = "" + reader.GetInt32(0);
                                productInfo.productname = reader.GetString(1);
                                productInfo.category = reader.GetString(3);
                                productInfo.quantity = "" + reader.GetDecimal(2);
                                productInfo.issued_date = "" + reader.GetDateTime(4).ToString();
                                
                            }
                        }
                    }
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            productInfo.productid = Request.Form["inputProductId"];
            productInfo.productname = Request.Form["inputProductName"];
            productInfo.category = Request.Form["inputCategory"];
            productInfo.quantity = Request.Form["inputQuantity"];
            productInfo.issued_date = Request.Form["inputIssuedDate"];

            if (productInfo.productname.Length == 0 || productInfo.productid.Length == 0 ||
             productInfo.category.Length == 0 || productInfo.quantity.Length == 0)
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
                    string Sql = "UPDATE products" +
                                " SET productname=@productname,category=@category,quantity=@quantity " +
                                "WHERE productid=@productid";

                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {

                        command.Parameters.AddWithValue("@productname", productInfo.productname);
                        command.Parameters.AddWithValue("@productid", productInfo.productid);
                        command.Parameters.AddWithValue("@category", productInfo.category);
                        command.Parameters.AddWithValue("@quantity", productInfo.quantity);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/stock-available?ProductName=null");
        }

    }
}
