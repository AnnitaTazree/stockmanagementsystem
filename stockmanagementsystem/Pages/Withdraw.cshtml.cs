using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using static stockmanagementsystem.Pages.stock_availableModel;


namespace stockmanagementsystem.Pages
{
    public class WithdrawModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public string changedQuantity = "";
        public string errorMessage = "";
        public string successMessage = "";
        public string productID = "";
        public void OnGet()
        {
            string id = Request.Query["ProductId"];
            productID = Request.Query["ProductId"];
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
                                // productInfo.productname = reader.GetString(1);
                                // productInfo.category = reader.GetString(2);
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
            //productInfo.productname = Request.Form["inputProductName"];
            productInfo.productid = Request.Form["inputProductId"];
            productInfo.quantity = Request.Form["inputQuantity"];
            // productInfo.category = Request.Form["inputCategory"];
            string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";
            string reuse = "";
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select Quantity from products where productid = " + productInfo.productid + ";", con);
                da.Fill(dt);
                reuse = String.Concat(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            changedQuantity = Request.Form["inputQuantity"];
            productInfo.issued_date = Request.Form["inputIssuedDate"];
            errorMessage = reuse;
            int pre = 0, cng = 0, rem = 0;
            pre = Convert.ToInt32(reuse.ToString());
            cng = Convert.ToInt32(changedQuantity);
            rem = pre - cng;

            if (productInfo.quantity.Length == 0)
            {
                errorMessage = "All the fields are required--" + reuse;
                return;
            }
            else //save the new client into the database
            {
                try
                {
                    connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string Sql = "UPDATE products" +
                                " SET quantity=@quantity " +
                                "WHERE productid=@productid";


                        using (SqlCommand command = new SqlCommand(Sql, connection))
                        {

                            //command.Parameters.AddWithValue("@productname", productInfo.productname);
                            command.Parameters.AddWithValue("@productid", productInfo.productid);
                            //command.Parameters.AddWithValue("@category", productInfo.category);
                            command.Parameters.AddWithValue("@quantity", rem.ToString());

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
}
