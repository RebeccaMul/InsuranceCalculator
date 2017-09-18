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
    public partial class StepTwo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //On page load, request the Policy ID from the URL and display the currently saved details from the database: 
                int row = 0;
                if (Request.QueryString["policyID"] != null)
                {
                    row = int.Parse(Request.QueryString["policyID"]);

                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();

                    string query = "SELECT * FROM Policy WHERE policyID=@rowid";

                    SqlCommand myCommand = new SqlCommand(query, myConnection);

                    myCommand.Parameters.AddWithValue("@rowid", row);

                    SqlDataReader rdr = myCommand.ExecuteReader();

                    while (rdr.Read())
                    {
                        DateTime pDate = Convert.ToDateTime(rdr["startDate"]);
                        chosenStart.Text = pDate.ToShortDateString(); ;
                    }
                }
            }
        }


        //Cancel method removes previously added details and returns to the home page
        protected void Cancel(object sender, EventArgs e)
        {
            int row = int.Parse(Request.QueryString["policyID"]);

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = "DELETE FROM Policy WHERE policyID=@rowid";

            SqlCommand myCommand = new SqlCommand(query, myConnection);

            myCommand.Parameters.AddWithValue("@rowid", row);

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            Response.Redirect("Default.aspx");
        }


        //Checks for the drivers detail's and if no data is entered, request it:
        protected void checkDriver(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fName.Text))
            {
                fNameDecline.Text = "Please enter a first name for this driver.";
                fNameDecline.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(lName.Text))
            {
                lNameDecline.Text = "Please enter a last name for this driver.";
                lNameDecline.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(occupation.Text))
            {
                occDecline.Text = "Please enter an Occupation for this driver.";
                occDecline.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(dateofbirth.Text))
            {
                dobDecline.Text = "Please enter a date of birth for this driver.";
                dobDecline.Visible = true;
            }
            //If all details are present then add driver to policy, and any claims if applicable:
            else
            {
                //No claims added, just adding driver:
                if (claimCheck.Checked && addClaims.Visible == false)
                {
                    //add driver code ****
                }

                //If no claims are added, but not confirmed, present a warning:
                if (claimbtn.Visible == true && claimCheck.Checked == false)
                {
                    Decline.Text = "Please confirm your previous claim details.";
                }

                //Claims present, add driver with claim details:
                if (addClaims.Visible == true)
                {


                }
            }
        }

        //Displays claim panel, and hides initial claim button
        protected void addClaim(object sender, EventArgs e)
        {
            addClaims.Visible = true;
            claimbtn.Visible = false;
        }

        //Hides claim panel if 'no claims' box is checked
        protected void noClaims(object sender, EventArgs e)
        {
            if (claimCheck.Checked == true)
            {
                addClaims.Visible = false;
                claimbtn.Visible = false;
            }
            else if (claimCheck.Checked == false)
            {
                claimbtn.Visible = true;
            }
        }

        //Dropdownlist change method - show the appropriate number of entry textboxes based on user's selection:
        protected void claimNumsChange(object sender, EventArgs e)
        {
            claimDateLabel.Visible = false;

            if (claimNums.SelectedIndex == 1)
            {
                claimDate.Visible = true;
                claim2.Visible = false;
                claim3.Visible = false;
                claim4.Visible = false;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 2)
            {
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = false;
                claim4.Visible = false;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 3)
            {
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = true;
                claim4.Visible = false;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 4)
            {
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = true;
                claim4.Visible = true;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 5)
            {
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = true;
                claim4.Visible = true;
                claim5.Visible = true;
            }
        }
    }
}