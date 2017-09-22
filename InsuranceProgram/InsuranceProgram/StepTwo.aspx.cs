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


        //CANCEL - removes any previously added policy details and returns to the home page
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


        //CHECKDRIVER - Checks for the driver's details for approval, and either declines or submits:
        protected void checkDriver(object sender, EventArgs e)
        {
            claimNums.DataBind(); fName.DataBind(); lName.DataBind(); occ.DataBind(); dobirth.DataBind();
            //Collecting input data:
            int numOfClaims = claimNums.SelectedIndex;
            String dFName = fName.Text;
            String dLName = lName.Text;
            String dOcc = occ.Text;
            DateTime dDob = Convert.ToDateTime(dobirth.Text);
            DateTime start = Convert.ToDateTime(chosenStart.Text);

            //If no claims have been added but user has not confirmed this, present a warning:
            if (claimbtn.Visible == true && claimCheck.Checked == false)
            {
                Decline.Text = "Please confirm any previous claim details.";
                Decline.Visible = true;
            }

            //No claims are confirmed, just add the driver to policy:
            if (claimCheck.Checked == true && addClaims.Visible == false)
            {
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
                    int newDriverID = 0;
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
                    newDriverID = (int)cmd.ExecuteScalar();
                   
                    myConnection.Close();

                    //If a new Driver was successfully added, move to Step three with the policy & driver IDs
                    if (newDriverID > 0)
                    {
                        Response.Redirect("StepThree.aspx?policyID=" + policy + "&driverID=" + newDriverID);
                    }
                }
            }

            //Claims are present, performs quantity checks and if approved add claim details associated with driver:
            if (addClaims.Visible == true)
            {
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
            }
        }

        //ADDDRIVERCLAIMS - Takes the number of claims input along with driverID, and adds the claims for this driver to the database
        public void addDriverClaims(int claimNumber, int Driver)
        {
            int policy = int.Parse(Request.QueryString["policyID"]);
            //Only adding one/first claim:
            if (claimNumber == 1)
            {
                claimDate.DataBind();
                DateTime first = Convert.ToDateTime(claimDate.Text);

                //Adding claim to database:
                string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(connectionString);

                myConnection.Open();

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) OUTPUT INSERTED.claimID VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                
                cmd.Parameters.Add(new SqlParameter("@date", first));
                cmd.Parameters.Add(new SqlParameter("@driver", Driver));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();

                myConnection.Close();
            } 
            //adding first and second claims
            else if (claimNumber == 2)
            {
                claimDate.DataBind(); claim2.DataBind();
                DateTime first = Convert.ToDateTime(claimDate.Text);
                DateTime second = Convert.ToDateTime(claim2.Text);

                //Adding claim to database:
                string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(connectionString);

                myConnection.Open();

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) OUTPUT INSERTED.claimID VALUES (@date, @driver, @policy); INSERT INTO Claim (cDate, driverID, policyID) OUTPUT INSERTED.claimID VALUES (@date2, @driver, @policy);";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                
                cmd.Parameters.Add(new SqlParameter("@date", first));
                cmd.Parameters.Add(new SqlParameter("@date2", second));
                cmd.Parameters.Add(new SqlParameter("@driver", Driver));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();

                myConnection.Close();
            }

        }

        //CALCULATEAGE - takes today's date and date of birth to calculate and return current age in years.
        public static int calculateAge(DateTime sDate, DateTime birthdate)
        {
            int age = sDate.Year - birthdate.Year;
            if (sDate < birthdate.AddYears(age))
                age--;

            return age;
        }

        //ADDCLAIM - Displays claim panel, and hides initial claim button
        protected void addClaim(object sender, EventArgs e)
        {
            addClaims.Visible = true;
            claimbtn.Visible = false;
        }

        //NOCLAIMS - Hides claim panel if 'no claims' box is checked
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

        //CLAIMNUMSCHANGE - show the appropriate number of entry textboxes based on user's dropdown selection:
        protected void claimNumsChange(object sender, EventArgs e)
        {
            claimDateLabel.Visible = false;

            if (claimNums.SelectedIndex == 1)
            {
                claimDateLabel.Visible = true;
                claimDate.Visible = true;
                claim2.Visible = false;
                claim3.Visible = false;
                claim4.Visible = false;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 2)
            {
                claimDateLabel.Visible = true;
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = false;
                claim4.Visible = false;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 3)
            {
                claimDateLabel.Visible = true;
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = true;
                claim4.Visible = false;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 4)
            {
                claimDateLabel.Visible = true;
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = true;
                claim4.Visible = true;
                claim5.Visible = false;
            }
            else if (claimNums.SelectedIndex == 5)
            {
                claimDateLabel.Visible = true;
                claimDate.Visible = true;
                claim2.Visible = true;
                claim3.Visible = true;
                claim4.Visible = true;
                claim5.Visible = true;
            }
        }
    }
}