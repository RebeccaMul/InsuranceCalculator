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
    public partial class Default2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //CheckDate method checks the submitted Start Date is valid 
        protected void CheckDate(object sender, EventArgs e)
        {
            startDate.DataBind();

            //If no start date is entered, request one:
            if (string.IsNullOrWhiteSpace(startDate.Text))
            {
                Decline.Text = "Please enter a valid policy start date.";
                Decline.Visible = true;
            }
            else
            {
                //Declaring DateTime and storing start date:
                DateTime sDate = Convert.ToDateTime(startDate.Text);

                //Obtaining today's date:
                DateTime today = DateTime.Now.Date;

                //If statement, comparing today's date to submitted start date:
                if (sDate.Date < today.Date)
                {
                    //Decline - start date prior to today
                    Decline.Text = "Declined - Start Date of Policy";
                    Decline.Visible = true;
                }

                if (sDate.Date > today.Date || sDate.Date == today.Date)
                {
                    //Hiding decline message if acceptable date is entered
                    Decline.Visible = false;

                    //Start date acceptable, submit to database as potential policy
                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();

                    string sql = "INSERT INTO Policy (startDate) OUTPUT INSERTED.policyID VALUES (@start)";
                    SqlCommand cmd = new SqlCommand(sql, myConnection);
                    //Parameterising value:
                    cmd.Parameters.Add(new SqlParameter("@start", sDate));
                    int newPolicyID = (int)cmd.ExecuteScalar();

                    //If a new Policy was successfully created, move to Step two with the new policy ID
                    if (newPolicyID != null)
                    {
                        Response.Redirect("StepTwo.aspx?policyID=" + newPolicyID);
                    }

                    myConnection.Close();

                }
            }

        }

    }
}