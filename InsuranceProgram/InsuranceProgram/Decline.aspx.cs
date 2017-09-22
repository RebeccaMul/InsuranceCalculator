using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Data;

namespace InsuranceProgram
{
    public partial class Decline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //On page load, request the Policy and Driver IDs from the URL and delete these details from database:
                int row = 0;
                if (Request.QueryString["policyID"] != null)
                {
                    row = int.Parse(Request.QueryString["policyID"]);
                    int driver = int.Parse(Request.QueryString["primary"]);

                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();
                    string query = "DELETE FROM Claim WHERE policyID=@policy; DELETE FROM Driver WHERE policyID=@policy; DELETE FROM Policy WHERE policyID=@policy";

                    SqlCommand myCommand = new SqlCommand(query, myConnection);

                    myCommand.Parameters.AddWithValue("@policy", row);
                    myCommand.Parameters.AddWithValue("@driver", driver);

                    myCommand.ExecuteNonQuery();

                    myConnection.Close();
                }
            }


        }

        protected void Redirect(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}