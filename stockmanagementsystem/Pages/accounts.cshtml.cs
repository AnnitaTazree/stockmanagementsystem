using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace stockmanagementsystem.Pages
{
    public class accountsModel : PageModel
    {
        public List<IndexInfo> indexInfo = new List<IndexInfo>();
        public string errorMessage = "";
        public string successMessage = "";
        public IndexInfo info = new IndexInfo();

        public void OnGet()
        {

        }

        public void OnPost()
        {
            info.email = Request.Form["inputUpdateEmail"];
            info.password = Request.Form["inputUpdatePassword"];

            if (info.email.Length == 0 || info.password.Length == 0)
            {
                errorMessage = "Each field is required";
                return;
            }


            try
            {
                string conString = @"Data Source=DESKTOP-SCKABHO\SQLEXPRESS;Initial Catalog=stockmanagement;Integrated Security=True;TrustServerCertificate=true;";
                SqlConnection con = new SqlConnection();
                con.ConnectionString = conString;
                //con.Open();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select count(*) from signlog where Email = '" + info.email + "' and [Password] = '" + info.password + "';", con);
                da.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter("select Email from signlog where Email = '" + info.email + "' and [Password] = '" + info.password + "';", con);
                    da2.Fill(dt2);
                    string Id = "";
                    Id = string.Concat(dt2.Rows[0][0].ToString());
                    using (SqlConnection connection = new SqlConnection(conString))
                    {
                        String Sql = "UPDATE signlog" +
                                " SET Email-@email,password=@password " +
                            "WHERE Id=@id";


                        using (SqlCommand command = new SqlCommand(Sql, connection))
                        {
                            command.Parameters.AddWithValue("@Id", info.id);
                            command.Parameters.AddWithValue("@Email", info.email);
                            command.Parameters.AddWithValue("@Password", info.password);

                            command.ExecuteNonQuery();

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
                            connection.Open();
                            String Sql = "INSERT INTO signlog" +
                               "(Email,password) VALUES" +
                               "(@email,@password);";


                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {
                                command.Parameters.AddWithValue("@Id", info.id);
                                command.Parameters.AddWithValue("@Email", info.email);
                                command.Parameters.AddWithValue("@Password", info.password);

                                command.ExecuteNonQuery();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        return;
                    }


                }

                //con.Close();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            info.id = ""; info.email = ""; info.password = "";
            successMessage = "Updated Successfully";

        }
    }
}
