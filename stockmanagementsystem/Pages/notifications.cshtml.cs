using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static stockmanagementsystem.Pages.stock_availableModel;


namespace stockmanagementsystem.Pages
{
    public class notificationsModel : PageModel
    {
        public List<ProductInfo> listproducts = new List<ProductInfo>();
        public ProductInfo productInfo = new ProductInfo();
        public List<ProductInfo> OutOfStockProducts = new List<ProductInfo>();
        public List<ProductInfo> NeedtoStockProducts = new List<ProductInfo>();
        public List<ProductInfo> FullOfStockProducts = new List<ProductInfo>(); 
        public string errorMessage = "";
        
        public void OnGet()
        {
            OutOfStockProducts = new List<ProductInfo>();
            NeedtoStockProducts = new List<ProductInfo>();
            FullOfStockProducts = new List<ProductInfo>();

            try
            {
                string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get products that are out of stock
                    string sql = "SELECT * FROM products WHERE quantity = 0";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            
                                while (reader.Read())
                                {ProductInfo productInfo = new ProductInfo();
                                productInfo.productid = "" + reader.GetInt32(0);
                                productInfo.productname = reader.GetString(1);
                                productInfo.category = reader.GetString(3);
                                productInfo.quantity = "" + reader.GetDecimal(2);
                                productInfo.issued_date = "" + reader.GetDateTime(4).ToString();
                                // productInfo.qnt = Convert.ToInt32(productInfo.quantity);



                                OutOfStockProducts.Add(productInfo);
                                }
                            
                        }

                        // Get products that are full of stock
                    }
                    connection.Close();

                }

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;

            }
            try
            {
                string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM products WHERE quantity < 10";

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

                                NeedtoStockProducts.Add(productInfo);
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

            try
            {
                string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open (); 

                   string sql = "SELECT * FROM products WHERE quantity >= 50";

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

                                FullOfStockProducts.Add(productInfo);
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
            if (OutOfStockProducts.Count == 0)
            {
                errorMessage = "There is no out of stock product";
                return;
            }

        }

    }
}
