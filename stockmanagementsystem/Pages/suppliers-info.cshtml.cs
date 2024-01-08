using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Input;

namespace stockmanagementsystem.Pages
{
    public class suppliers_infoModel : PageModel
    {

        public List<SuppliersInfo> listsuppliers = new List<SuppliersInfo>();
        public string errorMessage = "";
        public SuppliersInfo suppliersInfo = new SuppliersInfo();
        public string category;

        public void OnGet()
        {
            category = Request.Query["Category"];


            if (category == "null")
            {

                category = "null";

                try
                {
                    string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "SELECT * FROM suppliers;";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    SuppliersInfo suppliersInfo = new SuppliersInfo();
                                    suppliersInfo.suppliersid = "" + reader.GetInt32(0);
                                    suppliersInfo.suppliersname = reader.GetString(1);
                                    suppliersInfo.phone = reader.GetString(2);
                                    suppliersInfo.address = reader.GetString(3);
                                    suppliersInfo.category = reader.GetString(4);

                                    listsuppliers.Add(suppliersInfo);
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
                    string connectionString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                  


                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string Sql = " SELECT * FROM suppliers WHERE category = '" + category + "';";

                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {

                                //command.Parameters.AddWithValue("@productname", productname);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        SuppliersInfo suppliersInfo = new SuppliersInfo();
                                        suppliersInfo.suppliersid = "" + reader.GetInt32(0);
                                        suppliersInfo.suppliersname = reader.GetString(1);
                                        suppliersInfo.phone = reader.GetString(2);
                                        suppliersInfo.address = reader.GetString(3);
                                        suppliersInfo.category = reader.GetString(4);

                                        listsuppliers.Add(suppliersInfo);
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
        }

        public void OnPost()
        {
            try
            {
                string conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";

                SqlConnection con = new SqlConnection();
                con.ConnectionString = conString;

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select count(*) from suppliers where suppliersid = '" + suppliersInfo.suppliersid + "' suppliersname = '" + suppliersInfo.suppliersname + "'phone = '" + suppliersInfo.phone + "'address = '" + suppliersInfo.address + "'category='" +suppliersInfo.category +"';", con);
                da.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter("select suppliersid from suppliers where  suppliersid = '" + suppliersInfo.suppliersid + "' suppliersname = '" + suppliersInfo.suppliersname + "'phone = '" + suppliersInfo.phone + "'address = '" + suppliersInfo.address + "'category='" + suppliersInfo.category + "';", con);
                    da2.Fill(dt2);
                    string suppliersID = "";
                    suppliersID = string.Concat(dt2.Rows[0][0].ToString());
                    using (SqlConnection connection = new SqlConnection(conString))
                    {

                        string Sql = "UPDATE suppliers" +
                            " SET suppliersname=@suppliersname,phone=@phone,address=@address,category=@category " +
                            "WHERE suppliersid=@suppliersid";
                        using (SqlCommand command = new SqlCommand(Sql, con))
                        {
                            command.Parameters.AddWithValue("@suppliersid", suppliersInfo.suppliersid);
                            command.Parameters.AddWithValue("@suppliersname", suppliersInfo.suppliersname);
                            command.Parameters.AddWithValue("@phone", suppliersInfo.phone);
                            command.Parameters.AddWithValue("@address", suppliersInfo.address);
                            command.Parameters.AddWithValue("@category", suppliersInfo.category);


                            command.ExecuteNonQuery();


                            Response.Redirect("/editsuppliers");

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

                            string Sql = "INSERT INTO suppliers" +
                                   "(suppliersname,phone,address,category) VALUES" +
                                   "(@suppliersname,@phone,@address,@category);";
                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {

                                //command.Parameters.AddWithValue("@suppliersid", suppliersInfo.suppliersid);
                                command.Parameters.AddWithValue("@suppliersname", suppliersInfo.suppliersname);
                                command.Parameters.AddWithValue("@phone", suppliersInfo.phone);
                                command.Parameters.AddWithValue("@address", suppliersInfo.address);
                                command.Parameters.AddWithValue("@category", suppliersInfo.category);

                                    command.ExecuteNonQuery();

                                Response.Redirect("/Insertsuppliers");


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
    public class SuppliersInfo
    {
        public string suppliersid { get; set; } = "";
        public string suppliersname { get; set; } = "";
        public string phone { get; set; } = "";
        public String address { get; set; } = "";
        public String category { get; set; } = "";


    }
}
