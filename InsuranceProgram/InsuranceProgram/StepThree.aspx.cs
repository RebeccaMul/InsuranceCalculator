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
    public partial class StepThree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //On page load, request the Policy and Driver IDs from the URL and display the currently saved details from the database:
                int row = 0;
                if (Request.QueryString["policyID"] != null)
                {
                    row = int.Parse(Request.QueryString["policyID"]);

                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();

                    string query = "SELECT * FROM Policy INNER JOIN Driver ON Policy.policyID = Driver.policyID WHERE Policy.policyID=@rowid";

                    SqlCommand myCommand = new SqlCommand(query, myConnection);

                    myCommand.Parameters.AddWithValue("@rowid", row);

                    SqlDataReader rdr = myCommand.ExecuteReader();

                    while (rdr.Read())
                    {
                        DateTime pDate = Convert.ToDateTime(rdr["startDate"]);
                        String fName = rdr["fName"].ToString();
                        String lName = rdr["lName"].ToString();
                        String occ = rdr["occupation"].ToString();
                        DateTime dob = Convert.ToDateTime(rdr["dateOfBirth"]);

                        chosenStart.Text = pDate.ToShortDateString();
                        nameDriver.Text = fName + " " + lName;
                        occDriver.Text = occ;
                        dobDriver.Text = dob.ToShortDateString();
                    }
                }
            }
        }

        //CANCEL - removes any previously added policy details and returns to the home page
        protected void Cancel(object sender, EventArgs e)
        {
            int row = int.Parse(Request.QueryString["policyID"]);
            int driver = int.Parse(Request.QueryString["driverID"]);

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();
            //***********NEED TO FIND CLAIMS AND DELETE THEM FIRST
            string query = "DELETE FROM Driver WHERE driverID=@driver; DELETE FROM Policy WHERE policyID=@rowid";

            SqlCommand myCommand = new SqlCommand(query, myConnection);

            myCommand.Parameters.AddWithValue("@rowid", row);
            myCommand.Parameters.AddWithValue("@driver", driver);

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            Response.Redirect("Default.aspx");
        }

        //SHOWADDDRIVERS - Displays claim panel, and hides initial claim button
        protected void showAddDrivers(object sender, EventArgs e)
        {
            moreDrivers.Visible = true;
            driverbtn.Visible = false;
        }


        //NODRIVERS - Hides driver panel if 'no drivers' box is checked
        protected void noDrivers(object sender, EventArgs e)
        {
            if (driverCheck.Checked == true)
            {
                moreDrivers.Visible = false;
                driverbtn.Visible = false;
            }
            else if (driverCheck.Checked == false)
            {
                driverbtn.Visible = true;
            }
        }

        //DRIVERNUMSCHANGE - show the appropriate number of entry textboxes based on user's dropdown selection:
        protected void driverNumsChange(object sender, EventArgs e)
        {
            driverDetailLabel.Visible = false;

            if (driverNums.SelectedIndex == 1)
            {
                driverDetailLabel.Visible = true;
                //                claimDate.Visible = true;

            }
            else if (driverNums.SelectedIndex == 2)
            {
                driverDetailLabel.Visible = true;
                //                claimDate.Visible = true;
            }
            else if (driverNums.SelectedIndex == 3)
            {
                driverDetailLabel.Visible = true;
                //                claimDate.Visible = true;
            }
            else if (driverNums.SelectedIndex == 4)
            {
                driverDetailLabel.Visible = true;
                //                claimDate.Visible = true;
            }
            else if (driverNums.SelectedIndex == 5)
            {
                driverDetailLabel.Visible = true;
                //                claimDate.Visible = true;
            }
        }


        //CHECKDRIVERS - Checks for the driver's details for approval, and either declines or submits:
        protected void checkDrivers(object sender, EventArgs e)
        {
            /*     claimNums.DataBind(); fName.DataBind(); lName.DataBind(); occ.DataBind(); dobirth.DataBind();
                 //Collecting input data:
                 int numOfClaims = claimNums.SelectedIndex;
                 String dFName = fName.Text;
                 String dLName = lName.Text;
                 String dOcc = occ.Text;
                 DateTime dDob = Convert.ToDateTime(dobirth.Text);
                 DateTime start = Convert.ToDateTime(chosenStart.Text);
            */

            int policy = int.Parse(Request.QueryString["policyID"]);
            int driver = int.Parse(Request.QueryString["driverID"]);

            //If no drivers have been added but the user has not confirmed this, present a warning:
            if (driverbtn.Visible == true && driverCheck.Checked == false)
            {
                Decline.Text = "Please confirm if any additional drivers are required.";
                Decline.Visible = true;
            }

            //No drivers are confirmed, just advance to calculation:
            if (driverCheck.Checked == true && moreDrivers.Visible == false)
            {
                Response.Redirect("premiumCalculation.aspx?policyID=" + policy + "&driverID=" + driver);
            }

            /*
             * //check drivers ages, claim numbers total
            //Drivers are present, performs quantity checks and if approved add driver details associated with policy:
                 if (moreDrivers.Visible == true)
                 {
                     ////CLAIMS
                     //Driver has over two claims, decline:
                     if (claimNums.SelectedIndex > 2)
                     {
                         Decline.Visible = true;
                         Decline.Text = "Declined - Driver has more than 2 claims: " + dFName + " " + dLName;
                     }
                     else if (claimNums.SelectedIndex <= 2)
                     {
                         //Driver has under two claims, progress method:
                         Decline.Visible = false;

                         //Performing age checks:
                         int driverAge = calculateAge(start, dDob);
                         if (driverAge < 21)
                         {
                             Decline.Text = "Declined - Age of youngest driver: " + dFName + " " + dLName;
                             Decline.Visible = true;
                         }
                         else if (driverAge > 75)
                         {
                             Decline.Text = "Declined - Age of oldest driver: " + dFName + " " + dLName;
                             Decline.Visible = true;
                         }
                         else if (driverAge > 21 && driverAge < 75)
                         {
                             Decline.Visible = false;
                             int policy = int.Parse(Request.QueryString["policyID"]);

                             //Acceptable age, adding driver to policy & database:
                             string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                             SqlConnection myConnection = new SqlConnection(connectionString);

                             myConnection.Open();

                             string sql = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol)";
                             SqlCommand cmd = new SqlCommand(sql, myConnection);
                             //Parameterising value:
                             cmd.Parameters.Add(new SqlParameter("@fname", dFName));
                             cmd.Parameters.Add(new SqlParameter("@lname", dLName));
                             cmd.Parameters.Add(new SqlParameter("@occ", dOcc));
                             cmd.Parameters.Add(new SqlParameter("@dob", dDob));
                             cmd.Parameters.Add(new SqlParameter("@pol", policy));
                             int newDriverID = (int)cmd.ExecuteScalar();

                             myConnection.Close();

                             //Once driver is created, Counting the number of submitted claims to process:
                             int claimTotal = 0;
                             if (claimNums.SelectedIndex == 1)
                             {
                                 claimTotal = 1;
                             }
                             else if (claimNums.SelectedIndex == 2)
                             {
                                 claimTotal = 2;
                             }

                             //Running method to add relevant claims for this driver:
                             addDriverClaims(claimTotal, newDriverID);

                             //Once complete, move to Step three with the policy & driver IDs
                             Response.Redirect("StepThree.aspx?policyID=" + policy + "&driverID=" + newDriverID);
                         }
                     }
                 } */
        }


    }
}