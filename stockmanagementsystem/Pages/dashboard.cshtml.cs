using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Dynamic.Core;
using static stockmanagementsystem.Pages.stock_availableModel;

namespace stockmanagementsystem.Pages
{
    public class dashboardModel : PageModel
    {

        public List<ProductInfo> listproducts = new List<ProductInfo>();
        public ProductInfo productInfo = new ProductInfo();
        public string Totalqntvalue = "0";
        public string Totalpvalue = "0";
        public string errorMessage = "";
        public void OnGet()
        {

            try
            {
                string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT SUM(quantity) FROM products";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // command.Parameters.AddWithValue("@quantity", Totalqntvalue);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                //ProductInfo productInfo = new ProductInfo();
                                //productInfo.productid = "" + reader.GetInt32(0);
                                //productInfo.productname = reader.GetString(1);
                                //productInfo.category = reader.GetString(2);
                                //productInfo.quantity = "" + reader.GetString(0);
                                //productInfo.qnt = Convert.ToInt32(productInfo.quantity);
                                Totalqntvalue = "" + reader.GetDecimal(0);

                                //listproducts.Add(productInfo);
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
                    connection.Open();
                    string sql = "SELECT count(productid) FROM products";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // command.Parameters.AddWithValue("@quantity", Totalqntvalue);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                //ProductInfo productInfo = new ProductInfo();
                                //productInfo.productid = "" + reader.GetInt32(0);
                                //productInfo.productname = reader.GetString(1);
                                //productInfo.category = reader.GetString(2);
                                // productInfo.quantity = reader.GetString(3);
                                //productInfo.qnt = Convert.ToInt32(productInfo.quantity);
                                Totalpvalue = "" + reader.GetInt32(0);

                                //listproducts.Add(productInfo);
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
            try
            {
                string conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                SqlConnection con = new SqlConnection();
                con.ConnectionString = conString;

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select count(*) from products where quantity = '" + productInfo.quantity + "';", con);
                da.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {




                }

            }
            catch (Exception ex)
            {








            }
        }
        
    }




}
