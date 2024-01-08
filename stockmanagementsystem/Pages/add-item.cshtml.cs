using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using static stockmanagementsystem.Pages.stock_availableModel;

namespace stockmanagementsystem.Pages
{
    public class add_itemModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            productInfo.productname = Request.Form["inputProductName"];
            productInfo.productid = Request.Form["inputProductId"];
            productInfo.category = Request.Form["inputCategory"];
            productInfo.quantity = Request.Form["inputQuantity"];
            productInfo.issued_date = Request.Form["inputIssuedDate"];

            if (productInfo.productname.Length == 0 || productInfo.category.Length == 0 || productInfo.quantity.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
           

            try
            {
                string conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";
                SqlConnection con = new SqlConnection();
                con.ConnectionString = conString;
                con.Open();


                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select count(*) from products where productname = '" + productInfo.productname + "' AND category = '" + productInfo.category + "' AND quantity = '" + productInfo.quantity + "';", con);
                da.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    errorMessage = "This product already exists";
                    return;

                }

                else
                {
                    try
                    {
                        conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";
                        using (SqlConnection connection = new SqlConnection(conString))
                        {
                           
                            connection.Open();
                            string Sql = "INSERT INTO products" +
                                        "(productname,category,quantity) VALUES" +
                                        "(@productname,@category,@quantity);";

                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {

                                command.Parameters.AddWithValue("@productname", productInfo.productname);
                                //command.Parameters.AddWithValue("@productid", productInfo.productid);
                                command.Parameters.AddWithValue("@category", productInfo.category);
                                command.Parameters.AddWithValue("@quantity", productInfo.quantity);

                                command.ExecuteNonQuery();

                            }


                        }


                    }
                    catch(Exception ex) 
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

            productInfo.productid = ""; productInfo.productname = ""; productInfo.category = ""; productInfo.quantity = "";
            successMessage = "New Item Added Correctly";

            Response.Redirect("/stock-available?ProductName=null");
        }
    }




}