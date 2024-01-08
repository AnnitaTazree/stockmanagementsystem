using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using static stockmanagementsystem.Pages.stock_availableModel;

namespace stockmanagementsystem.Pages
{
    public class stock_inModel : PageModel
    {

        public List<ProductInfo> listproducts = new List<ProductInfo>();
        public string errorMessage = "";
        public ProductInfo productInfo = new ProductInfo();
        public string productname;

        public void OnGet()
        {

            productname = Request.Query["ProductName"];


            if (productname == "null")
            {

                productname = "null";
                try
                {
                    string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True; TrustServerCertificate=true;";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "SELECT * FROM products;";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ProductInfo productInfo = new ProductInfo();
                                    productInfo.productid = "" + reader.GetInt32(0);
                                    productInfo.productname = reader.GetString(1);
                                    productInfo.category = reader.GetString(3);
                                    productInfo.quantity = "" + reader.GetDecimal(2);
                                    productInfo.issued_date = "" + reader.GetDateTime(4).ToString();
                                    productInfo.qnt = Convert.ToInt32(productInfo.quantity);
                                    listproducts.Add(productInfo);
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
            else
            {
                try
                {
                    string conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = conString;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select count(*) from products where productname = '" + productname + "';", con);
                    da.Fill(dt); if (dt.Rows[0][0].ToString() == "0")
                    {
                        errorMessage = "There is no such product";
                    }
                   

                    else

                    {
                        string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";



                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string Sql = " SELECT * FROM products WHERE productname = '" + productname + "';";

                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {

                                //command.Parameters.AddWithValue("@productname", productname);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        ProductInfo productInfo = new ProductInfo();
                                        productInfo.productid = "" + reader.GetInt32(0);
                                        productInfo.productname = reader.GetString(1);
                                        productInfo.category = reader.GetString(3);
                                        productInfo.quantity = "" + reader.GetDecimal(2);
                                        productInfo.issued_date = "" + reader.GetDateTime(4).ToString();
                                        productInfo.qnt = Convert.ToInt32(productInfo.quantity);
                                        listproducts.Add(productInfo);
                                    }
                                }

                            }
                            connection.Close();
                        }
                    }
                }


                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;

                }

            }
        }

        public void OnPost()
        {
            try
            {
                string conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                SqlConnection con = new SqlConnection();
                con.ConnectionString = conString;
                        
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select count(*) from products where productid = '" + productInfo.productid + "' productname = '" + productInfo.productname + "'category = '" + productInfo.category + "'quantity = '" + productInfo.quantity + "';", con);
                    da.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter("select productid from products where productid = '" + productInfo.productid + "' productname = '" + productInfo.productname + "'category = '" + productInfo.category + "'quantity = '" + productInfo.quantity + "'; ", con);
                    da2.Fill(dt2);
                    string productID = "";
                    productID = string.Concat(dt2.Rows[0][0].ToString());
                    using (SqlConnection connection = new SqlConnection(conString))
                    {

                        string Sql = "UPDATE products" +
                            " SET productname=@productname,category=@category,quantity=@quantity " +
                            "WHERE productid=@productid";
                        using (SqlCommand command = new SqlCommand(Sql, con))
                        {

                            command.Parameters.AddWithValue("@productname", productInfo.productname);
                            command.Parameters.AddWithValue("@productid", productInfo.productid);
                            command.Parameters.AddWithValue("@category", productInfo.category);
                            command.Parameters.AddWithValue("@quantity", productInfo.quantity);

                            command.ExecuteNonQuery();


                            Response.Redirect("/Edit");

                        }

                    }
                }

                else
                {
                    try
                    {

                        conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";
                        using (SqlConnection connection = new SqlConnection(conString))
                        {

                            String Sql = "INSERT INTO products" +
                                   "(productname,category,quantity) VALUES" +
                                   "(@productname,@category,@quantity);";
                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {

                                command.Parameters.AddWithValue("@productname", productInfo.productname);
                                //command.Parameters.AddWithValue("@productid", productInfo.productid);
                                command.Parameters.AddWithValue("@category", productInfo.category);
                                command.Parameters.AddWithValue("@quantity", productInfo.quantity);

                                command.ExecuteNonQuery();

                                Response.Redirect("/add_item");


                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        return;

                    }
                }
                
            }

            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
        }
    }
}
