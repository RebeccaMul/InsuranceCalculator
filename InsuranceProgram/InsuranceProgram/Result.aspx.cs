using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Data;

namespace InsuranceProgram
{
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //On page load, request the Policy and Driver IDs from the URL and update the final premium:
                int row = 0;
                if (Request.QueryString["policyID"] != null)
                {
                    row = int.Parse(Request.QueryString["policyID"]);
                    int premium = int.Parse(Request.QueryString["premium"]);
                    int driver = int.Parse(Request.QueryString["primary"]);

                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();
                    string query = "UPDATE Policy SET totalPrice=@newPrice WHERE policyID=@policy";
                    
                    SqlCommand myCommand = new SqlCommand(query, myConnection);

                    myCommand.Parameters.AddWithValue("@policy", row);
                    myCommand.Parameters.AddWithValue("@newPrice", premium);

                    myCommand.ExecuteNonQuery();
                    
                    //Download and display final details from database:
                    string query2 = "SELECT * FROM Policy INNER JOIN Driver ON Policy.policyID = Driver.policyID WHERE Policy.policyID=@rowid AND Driver.driverID=@driver";
                    SqlCommand myCommand2 = new SqlCommand(query2, myConnection);
                    myCommand2.Parameters.AddWithValue("@rowid", row);
                    myCommand2.Parameters.AddWithValue("@driver", driver);
                    SqlDataReader rdr = myCommand2.ExecuteReader();

                    while (rdr.Read())
                    {
                        DateTime pDate = Convert.ToDateTime(rdr["startDate"]);
                        String fName = rdr["fName"].ToString();
                        String lName = rdr["lName"].ToString();
                        String cost = rdr["totalPrice"].ToString();

                        Name.Text = fName + " " + lName;
                        Price.Text = "£"+cost;
                        Date.Text = pDate.ToShortDateString();
                    }





                    myConnection.Close();
                }
            }



        }

        //Redirects to home page if button is clicked:
        protected void Redirect(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    
    }

}