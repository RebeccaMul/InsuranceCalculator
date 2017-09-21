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
            string query = "DELETE FROM Claim WHERE driverID=@driver; DELETE FROM Driver WHERE driverID=@driver; DELETE FROM Policy WHERE policyID=@policy";

            SqlCommand myCommand = new SqlCommand(query, myConnection);

            myCommand.Parameters.AddWithValue("@policy", row);
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
                Decline.Visible = false;
            }
            else if (driverCheck.Checked == false)
            {
                driverbtn.Visible = true;
                Decline.Visible = false;
            }
        }

        //DRIVERNUMSCHANGE - show the appropriate number of entry textboxes based on user's dropdown selection:
        protected void driverNumsChange(object sender, EventArgs e)
        {

            if (driverNums.SelectedIndex == 1)
            {
                newDriver2.Visible = true;
                newDriver3.Visible = false;
                newDriver4.Visible = false;
                newDriver5.Visible = false;

            }
            else if (driverNums.SelectedIndex == 2)
            {
                newDriver2.Visible = true;
                newDriver3.Visible = true;
                newDriver4.Visible = false;
                newDriver5.Visible = false;
            }
            else if (driverNums.SelectedIndex == 3)
            {
                newDriver2.Visible = true;
                newDriver3.Visible = true;
                newDriver4.Visible = true;
                newDriver5.Visible = false;
            }
            else if (driverNums.SelectedIndex == 4)
            {
                newDriver2.Visible = true;
                newDriver3.Visible = true;
                newDriver4.Visible = true;
                newDriver5.Visible = true;
            }
        }

        //CHECKDRIVERS - Checks for the driver's details for approval, and either declines or submits:
        protected void checkDrivers(object sender, EventArgs e)
        {
            //Getting policy and driver details
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

            //Additional drivers required, advance to adding them to policy:
            if (driverCheck.Checked == false && moreDrivers.Visible == true)
            {
                //Adding relevant drivers if selected:
                if (driverNums.SelectedIndex == 1)
                {
                    addDriver(1);
                }
                else if (driverNums.SelectedIndex == 2)
                {
                    addDriver(2);
                }
                else if (driverNums.SelectedIndex == 3)
                {
                    addDriver(3);
                }
                else if (driverNums.SelectedIndex == 4)
                {
                    addDriver(4);
                }
            }
        }

        //addDriver - show second claim panel:
        protected void addDriver(int drivernum)
        {
            //Getting policy start date:
            DateTime start = Convert.ToDateTime(chosenStart.Text);
            String primary = Request.QueryString["driverID"];

            //Only add driver2:
            if (drivernum == 1)
            {
                //Getting driver2 details:
                String TwoFName = fName.Text;
                String TwoLName = lName.Text;
                String TwoOcc = occ.Text;
                DateTime TwoDob = Convert.ToDateTime(dobirth.Text);

                //Performing age checks:
                int driverAge = calculateAge(start, TwoDob);
                if (driverAge < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + TwoFName + " " + TwoLName;
                    Decline.Visible = true;
                }
                else if (driverAge > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + TwoFName + " " + TwoLName;
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
                    cmd.Parameters.Add(new SqlParameter("@fname", TwoFName));
                    cmd.Parameters.Add(new SqlParameter("@lname", TwoLName));
                    cmd.Parameters.Add(new SqlParameter("@occ", TwoOcc));
                    cmd.Parameters.Add(new SqlParameter("@dob", TwoDob));
                    cmd.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriverID = (int)cmd.ExecuteScalar();

                    myConnection.Close();

                    //Once driver is created, move to additional claim step:
                    Response.Redirect("StepThreeClaim.aspx?policyID=" + policy + "&primary=" + primary + "&driverID=" + newDriverID);
                }

            }
            else if (drivernum == 2)
            {
                //Getting driver2 + driver3 details:
                String TwoFName = fName.Text; String ThreeFName = d3fname.Text;
                String TwoLName = lName.Text; String ThreeLName = d3lname.Text;
                String TwoOcc = occ.Text; String ThreeOcc = d3occ.Text;
                DateTime TwoDob = Convert.ToDateTime(dobirth.Text); DateTime ThreeDob = Convert.ToDateTime(d3dob.Text);

                //Performing age checks:
                int driverAge2 = calculateAge(start, TwoDob);
                int driverAge3 = calculateAge(start, ThreeDob);
                if (driverAge2 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + TwoFName + " " + TwoLName;
                    Decline.Visible = true;
                }
                else if (driverAge3 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + ThreeFName + " " + ThreeLName;
                    Decline.Visible = true;
                }
                else if (driverAge2 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + TwoFName + " " + TwoLName;
                    Decline.Visible = true;
                }
                else if (driverAge3 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + ThreeFName + " " + ThreeLName;
                    Decline.Visible = true;
                }
                else if (driverAge2 > 21 && driverAge2 < 75 && driverAge3 > 21 && driverAge3 < 75)
                {
                    Decline.Visible = false;
                    int policy = int.Parse(Request.QueryString["policyID"]);

                    //Acceptable age, adding driver to policy & database:
                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();

                    string sql = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol);";
                    SqlCommand cmd = new SqlCommand(sql, myConnection);
                    //Parameterising value:
                    cmd.Parameters.Add(new SqlParameter("@fname", TwoFName));
                    cmd.Parameters.Add(new SqlParameter("@lname", TwoLName));
                    cmd.Parameters.Add(new SqlParameter("@occ", TwoOcc));
                    cmd.Parameters.Add(new SqlParameter("@dob", TwoDob));
                    cmd.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver2 = (int)cmd.ExecuteScalar();

                    myConnection.Close();

                    //Add driver3:
                    string connectionString2 = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection2 = new SqlConnection(connectionString2);

                    myConnection2.Open();

                    string sql2 = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol)";
                    SqlCommand cmd2 = new SqlCommand(sql2, myConnection2);
                    //Parameterising value:
                    cmd2.Parameters.Add(new SqlParameter("@fname", ThreeFName));
                    cmd2.Parameters.Add(new SqlParameter("@lname", ThreeLName));
                    cmd2.Parameters.Add(new SqlParameter("@occ", ThreeOcc));
                    cmd2.Parameters.Add(new SqlParameter("@dob", ThreeDob));
                    cmd2.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver3 = (int)cmd2.ExecuteScalar();

                    myConnection2.Close();

                    //Once driver is created, move to additional claim step:
                    Response.Redirect("StepThreeClaim.aspx?policyID=" + policy + "&primary=" + primary + "&driver2=" + newDriver2 + "&driver3=" + newDriver3);
                }

            }
            else if (drivernum == 3)
            {
                //Getting driver2-4 details:
                String TwoFName = fName.Text; String ThreeFName = d3fname.Text; String FourFName = d4fname.Text;
                String TwoLName = lName.Text; String ThreeLName = d3lname.Text; String FourLName = d4lname.Text;
                String TwoOcc = occ.Text; String ThreeOcc = d3occ.Text; String FourOcc = d4occ.Text;
                DateTime TwoDob = Convert.ToDateTime(dobirth.Text); DateTime ThreeDob = Convert.ToDateTime(d3dob.Text); DateTime FourDob = Convert.ToDateTime(d4dob.Text);

                //Performing age checks:
                int driverAge2 = calculateAge(start, TwoDob);
                int driverAge3 = calculateAge(start, ThreeDob);
                int driverAge4 = calculateAge(start, FourDob);
                if (driverAge2 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + TwoFName + " " + TwoLName;
                    Decline.Visible = true;
                }
                else if (driverAge3 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + ThreeFName + " " + ThreeLName;
                    Decline.Visible = true;
                }
                else if (driverAge4 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + FourFName + " " + FourLName;
                    Decline.Visible = true;
                }
                else if (driverAge2 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + TwoFName + " " + TwoLName;
                    Decline.Visible = true;
                }
                else if (driverAge3 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + ThreeFName + " " + ThreeLName;
                    Decline.Visible = true;
                }
                else if (driverAge4 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + FourFName + " " + FourLName;
                    Decline.Visible = true;
                }
                else if (driverAge2 > 21 && driverAge2 < 75 && driverAge3 > 21 && driverAge3 < 75 && driverAge4 > 21 && driverAge4 < 75)
                {
                    Decline.Visible = false;
                    int policy = int.Parse(Request.QueryString["policyID"]);

                    //Acceptable age, adding driver to policy & database:
                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();

                    string sql = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol);";
                    SqlCommand cmd = new SqlCommand(sql, myConnection);
                    //Parameterising value:
                    cmd.Parameters.Add(new SqlParameter("@fname", TwoFName));
                    cmd.Parameters.Add(new SqlParameter("@lname", TwoLName));
                    cmd.Parameters.Add(new SqlParameter("@occ", TwoOcc));
                    cmd.Parameters.Add(new SqlParameter("@dob", TwoDob));
                    cmd.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver2 = (int)cmd.ExecuteScalar();

                    myConnection.Close();

                    //Add driver3:
                    string connectionString2 = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection2 = new SqlConnection(connectionString2);

                    myConnection2.Open();

                    string sql2 = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol)";
                    SqlCommand cmd2 = new SqlCommand(sql2, myConnection2);
                    //Parameterising value:
                    cmd2.Parameters.Add(new SqlParameter("@fname", ThreeFName));
                    cmd2.Parameters.Add(new SqlParameter("@lname", ThreeLName));
                    cmd2.Parameters.Add(new SqlParameter("@occ", ThreeOcc));
                    cmd2.Parameters.Add(new SqlParameter("@dob", ThreeDob));
                    cmd2.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver3 = (int)cmd2.ExecuteScalar();

                    myConnection2.Close();

                    //Add driver 4:
                    string connectionString3 = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection3 = new SqlConnection(connectionString3);

                    myConnection3.Open();

                    string sql3 = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol)";
                    SqlCommand cmd3 = new SqlCommand(sql3, myConnection3);
                    //Parameterising value:
                    cmd3.Parameters.Add(new SqlParameter("@fname", ThreeFName));
                    cmd3.Parameters.Add(new SqlParameter("@lname", ThreeLName));
                    cmd3.Parameters.Add(new SqlParameter("@occ", ThreeOcc));
                    cmd3.Parameters.Add(new SqlParameter("@dob", ThreeDob));
                    cmd3.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver4 = (int)cmd3.ExecuteScalar();

                    myConnection3.Close();

                    //Once driver is created, move to additional claim step:
                    Response.Redirect("StepThreeClaim.aspx?policyID=" + policy + "&primary=" + primary + "&driver2=" + newDriver2 + "&driver3=" + newDriver3 + "&driver4=" + newDriver4);
                }
            }
            else if (drivernum == 4)
            {
                //Getting driver2-5 details:
                String TwoFName = fName.Text; String ThreeFName = d3fname.Text; String FourFName = d4fname.Text; String FiveFName = d5fname.Text;
                String TwoLName = lName.Text; String ThreeLName = d3lname.Text; String FourLName = d4lname.Text; String FiveLName = d5lname.Text;
                String TwoOcc = occ.Text; String ThreeOcc = d3occ.Text; String FourOcc = d4occ.Text; String FiveOcc = d5occ.Text;
                DateTime TwoDob = Convert.ToDateTime(dobirth.Text); DateTime ThreeDob = Convert.ToDateTime(d3dob.Text); DateTime FourDob = Convert.ToDateTime(d4dob.Text); DateTime FiveDob = Convert.ToDateTime(d5dob.Text);

                //Performing age checks:
                int driverAge2 = calculateAge(start, TwoDob);
                int driverAge3 = calculateAge(start, ThreeDob);
                int driverAge4 = calculateAge(start, FourDob);
                int driverAge5 = calculateAge(start, FiveDob);
                if (driverAge2 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + TwoFName + " " + TwoLName;
                    Decline.Visible = true;
                }
                else if (driverAge3 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + ThreeFName + " " + ThreeLName;
                    Decline.Visible = true;
                }
                else if (driverAge4 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + FourFName + " " + FourLName;
                    Decline.Visible = true;
                }
                else if (driverAge5 < 21)
                {
                    Decline.Text = "Declined - Age of youngest driver: " + FiveFName + " " + FiveLName;
                    Decline.Visible = true;
                }
                else if (driverAge2 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + TwoFName + " " + TwoLName;
                    Decline.Visible = true;
                }
                else if (driverAge3 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + ThreeFName + " " + ThreeLName;
                    Decline.Visible = true;
                }
                else if (driverAge4 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + FourFName + " " + FourLName;
                    Decline.Visible = true;
                }
                else if (driverAge5 > 75)
                {
                    Decline.Text = "Declined - Age of oldest driver: " + FiveFName + " " + FiveLName;
                    Decline.Visible = true;
                }
                else if (driverAge2 > 21 && driverAge2 < 75 && driverAge3 > 21 && driverAge3 < 75 && driverAge4 > 21 && driverAge4 < 75 && driverAge5 > 21 && driverAge5 < 75)
                {
                    Decline.Visible = false;
                    int policy = int.Parse(Request.QueryString["policyID"]);

                    //Acceptable age, adding driver to policy & database:
                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();

                    string sql = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol);";
                    SqlCommand cmd = new SqlCommand(sql, myConnection);
                    //Parameterising value:
                    cmd.Parameters.Add(new SqlParameter("@fname", TwoFName));
                    cmd.Parameters.Add(new SqlParameter("@lname", TwoLName));
                    cmd.Parameters.Add(new SqlParameter("@occ", TwoOcc));
                    cmd.Parameters.Add(new SqlParameter("@dob", TwoDob));
                    cmd.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver2 = (int)cmd.ExecuteScalar();

                    myConnection.Close();

                    //Add driver3:
                    string connectionString2 = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection2 = new SqlConnection(connectionString2);

                    myConnection2.Open();

                    string sql2 = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol)";
                    SqlCommand cmd2 = new SqlCommand(sql2, myConnection2);
                    //Parameterising value:
                    cmd2.Parameters.Add(new SqlParameter("@fname", ThreeFName));
                    cmd2.Parameters.Add(new SqlParameter("@lname", ThreeLName));
                    cmd2.Parameters.Add(new SqlParameter("@occ", ThreeOcc));
                    cmd2.Parameters.Add(new SqlParameter("@dob", ThreeDob));
                    cmd2.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver3 = (int)cmd2.ExecuteScalar();

                    myConnection2.Close();

                    //Add driver 4:
                    string connectionString3 = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection3 = new SqlConnection(connectionString3);

                    myConnection3.Open();

                    string sql3 = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol)";
                    SqlCommand cmd3 = new SqlCommand(sql3, myConnection3);
                    //Parameterising value:
                    cmd3.Parameters.Add(new SqlParameter("@fname", ThreeFName));
                    cmd3.Parameters.Add(new SqlParameter("@lname", ThreeLName));
                    cmd3.Parameters.Add(new SqlParameter("@occ", ThreeOcc));
                    cmd3.Parameters.Add(new SqlParameter("@dob", ThreeDob));
                    cmd3.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver4 = (int)cmd3.ExecuteScalar();

                    myConnection3.Close();

                    //add driver 5:
                    string connectionString4 = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection4 = new SqlConnection(connectionString4);

                    myConnection4.Open();

                    string sql4 = "INSERT INTO Driver (fName, lName, occupation, dateOfBirth, policyID) OUTPUT INSERTED.driverID VALUES (@fname, @lname, @occ, @dob, @pol)";
                    SqlCommand cmd4 = new SqlCommand(sql4, myConnection4);
                    //Parameterising value:
                    cmd4.Parameters.Add(new SqlParameter("@fname", ThreeFName));
                    cmd4.Parameters.Add(new SqlParameter("@lname", ThreeLName));
                    cmd4.Parameters.Add(new SqlParameter("@occ", ThreeOcc));
                    cmd4.Parameters.Add(new SqlParameter("@dob", ThreeDob));
                    cmd4.Parameters.Add(new SqlParameter("@pol", policy));
                    int newDriver5 = (int)cmd4.ExecuteScalar();

                    myConnection4.Close();

                    //Once driver is created, move to additional claim step:
                    Response.Redirect("StepThreeClaim.aspx?policyID=" + policy + "&primary=" + primary + "&driver2=" + newDriver2 + "&driver3=" + newDriver3 + "&driver4=" + newDriver4 + "&driver5=" + newDriver5);
                }
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

    }
}