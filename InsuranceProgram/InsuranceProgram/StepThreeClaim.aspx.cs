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
    public partial class StepThreeClaim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //On page load, request the Policy and Primary Driver IDs from the URL and display the currently saved details from the database:
                int row = 0;
                if (Request.QueryString["policyID"] != null)
                {
                    row = int.Parse(Request.QueryString["policyID"]);
                    int primary = int.Parse(Request.QueryString["primary"]);

                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    myConnection.Open();

                    string query = "SELECT * FROM Policy INNER JOIN Driver ON Policy.policyID = Driver.policyID WHERE Policy.policyID=@rowid AND Driver.driverID = @driver";

                    SqlCommand myCommand = new SqlCommand(query, myConnection);

                    myCommand.Parameters.AddWithValue("@rowid", row);
                    myCommand.Parameters.AddWithValue("@driver", primary);

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

            //Displaying driver panels based on number added to policy:
            if (Request.QueryString["driver2"] != null)
            {
                //show driver 2 panel
                driverdiv2.Visible = true;
            }
            if (Request.QueryString["driver3"] != null)
            {
                //show driver 3 panel
                driverdiv3.Visible = true;
            }
            if (Request.QueryString["driver4"] != null)
            {
                //show driver4 panel
                driverdiv4.Visible = true;
            }
            if (Request.QueryString["driver5"] != null)
            {
                //show driver5 panel
                driverdiv5.Visible = true;
            }

        }



        //CANCEL - removes any previously added policy details and returns to the home page
        protected void Cancel(object sender, EventArgs e)
        {
            int row = int.Parse(Request.QueryString["policyID"]);
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

            Response.Redirect("Default.aspx");
        }

        //REMOVE - none needed, set panel to hidden
        protected void removeClaims(object sender, EventArgs e)
        {
            String policy = Request.QueryString["policyID"];
            String primary = Request.QueryString["primary"];
            moreClaims.Visible = false;
        }

        //HIDECLAIMS - hide claims panel and empty textboxes for each driver:
        protected void hideClaims(object sender, EventArgs e)
        {
            if (claimCheck2.Checked == true)
            {
                twoClaims.Visible = false;
                twoclaim1.Text = string.Empty;
                twoclaim2.Text = string.Empty;
            }
            else if (claimCheck2.Checked == false)
            {
                twoClaims.Visible = true;
            }

            if (claimCheck3.Checked == true)
            {
                threeClaims.Visible = false;
                threeclaim1.Text = string.Empty;
                threeclaim2.Text = string.Empty;
            }
            else if (claimCheck3.Checked == false)
            {
                threeClaims.Visible = true;
            }

            if (claimCheck4.Checked == true)
            {
                fourClaims.Visible = false;
                fourclaim1.Text = string.Empty;
                fourclaim2.Text = string.Empty;
            }
            else if (claimCheck4.Checked == false)
            {
                fourClaims.Visible = true;
            }

            if (claimCheck5.Checked == true)
            {
                fiveClaims.Visible = false;
                fiveclaim1.Text = string.Empty;
                fiveclaim2.Text = string.Empty;
            }
            else if (claimCheck5.Checked == false)
            {
                fiveClaims.Visible = true;
            }
        }

        //ADDCLAIMS - store in database:
        protected void addClaims(object sender, EventArgs e)
        {
            String policy = Request.QueryString["policyID"];
            String primary = Request.QueryString["primary"];
            String start = chosenStart.Text;

            //No claims added, just redirect:
            if (moreClaims.Visible == false)
            {
                Response.Redirect("premiumCalculation.aspx?policyID=" + policy + "&primary=" + primary + "&start=" + start);
            }

            //All drivers confirmed no claims, just redirect:
            if (claimCheck2.Checked == true && claimCheck3.Checked == true && claimCheck4.Checked == true && claimCheck5.Checked == true)
            {
                Response.Redirect("premiumCalculation.aspx?policyID=" + policy + "&primary=" + primary + "&start=" + start);
            }

            //Adding claims according to entries:
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            //if any claim is entered, submit to database:
            if (!String.IsNullOrEmpty(twoclaim1.Text) && claimCheck2.Checked == false)
            {
                //Claim details:
                int driverID = int.Parse(Request.QueryString["driver2"]);
                DateTime claim1 = Convert.ToDateTime(twoclaim1.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim1));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }
            if (!String.IsNullOrEmpty(twoclaim2.Text) != null && claimCheck2.Checked == false)
            {
                int driverID = int.Parse(Request.QueryString["driver2"]);
                DateTime claim2 = Convert.ToDateTime(twoclaim2.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim2));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }
            if (!String.IsNullOrEmpty(threeclaim1.Text) && claimCheck3.Checked == false)
            {
                int driverID = int.Parse(Request.QueryString["driver3"]);
                DateTime claim3 = Convert.ToDateTime(threeclaim1.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim3));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }
            if (!String.IsNullOrEmpty(threeclaim2.Text) && claimCheck3.Checked == false)
            {
                int driverID = int.Parse(Request.QueryString["driver3"]);
                DateTime claim4 = Convert.ToDateTime(threeclaim2.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim4));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }
            if (!String.IsNullOrEmpty(fourclaim1.Text) && claimCheck4.Checked == false)
            {
                int driverID = int.Parse(Request.QueryString["driver4"]);
                DateTime claim5 = Convert.ToDateTime(fourclaim1.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim5));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }
            if (!String.IsNullOrEmpty(fourclaim2.Text) && claimCheck4.Checked == false)
            {
                int driverID = int.Parse(Request.QueryString["driver4"]);
                DateTime claim6 = Convert.ToDateTime(fourclaim2.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim6));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }
            if (!String.IsNullOrEmpty(fiveclaim1.Text) && claimCheck5.Checked == false)
            {
                int driverID = int.Parse(Request.QueryString["driver5"]);
                DateTime claim7 = Convert.ToDateTime(fiveclaim1.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim7));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }
            if (!String.IsNullOrEmpty(fiveclaim2.Text) && claimCheck5.Checked == false)
            {
                int driverID = int.Parse(Request.QueryString["driver5"]);
                DateTime claim8 = Convert.ToDateTime(fiveclaim2.Text);

                string sql = "INSERT INTO Claim (cDate, driverID, policyID) VALUES (@date, @driver, @policy)";
                SqlCommand cmd = new SqlCommand(sql, myConnection);
                cmd.Parameters.Add(new SqlParameter("@date", claim8));
                cmd.Parameters.Add(new SqlParameter("@driver", driverID));
                cmd.Parameters.Add(new SqlParameter("@policy", policy));
                cmd.ExecuteNonQuery();
            }

            myConnection.Close();

            Response.Redirect("premiumCalculation.aspx?policyID=" + policy + "&primary=" + primary + "&start=" + start);

        }
    }

}