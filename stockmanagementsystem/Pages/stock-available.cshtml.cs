using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace stockmanagementsystem.Pages
{
    public class stock_availableModel : PageModel
    {
        public List<ProductInfo> listproducts = new List<ProductInfo>();
        public ProductInfo productInfo = new ProductInfo();
        public string errorMessage = "";
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
                    da.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "0")
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

        public class ProductInfo
        {
            public string productid { get; set; } = "";
            public string productname { get; set; } = "";
            public string category { get; set; } = "";
            public string quantity { get; set; } = "";
            public string issued_date { get; set; } = "";
            public int qnt { get; set; } = 0;

            public int Totalvalue { get; set; } = 0;


        }
    }
}

