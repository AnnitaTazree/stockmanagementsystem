using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;


namespace stockmanagementsystem.Pages
{
    public class IndexModel : PageModel
    {
        public List<IndexInfo> indexInfo = new List<IndexInfo>();
        public string errorMessage = "";
        public IndexInfo info = new IndexInfo();


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            info.email = Request.Form["inputEmail"];
            info.password = Request.Form["inputPassword"];

            if (info.email.Length == 0 || info.password.Length == 0)
            {
                errorMessage = "All the fields required";
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
                    Response.Redirect("/dashboard");
                }

                else 
                {
                    errorMessage = "Either password or email is incorrect";
                    return;
                
                }

                //con.Close();




            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }



        }
    }


    public class IndexInfo
    {
        public string id { get; set; } = "";
        public string email { get; set; } = "";
        public string password { get; set; } = "";

    }
}