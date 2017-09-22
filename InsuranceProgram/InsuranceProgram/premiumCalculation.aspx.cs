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
    public partial class premiumCalculation : System.Web.UI.Page
    {
        //Premium intial cost, and calculating percentage charges:
        private static double basePremium = 500.00;
        private static double premiumCost = 500.00;
        private static double ten = (basePremium / 100) * 10;
        private static double twenty = (basePremium / 100) * 20;

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

                    //Policy & primary driver details:
                    string query = "SELECT * FROM Policy INNER JOIN Driver ON Policy.policyID = Driver.policyID WHERE Policy.policyID=@rowid AND Driver.driverID = @driver";

                    SqlCommand myCommand = new SqlCommand(query, myConnection);

                    myCommand.Parameters.AddWithValue("@rowid", row);
                    myCommand.Parameters.AddWithValue("@driver", primary);


                    SqlDataReader rdr = myCommand.ExecuteReader();

                    while (rdr.Read())
                    {
                        String pDate = rdr["startDate"].ToString();
                        String fName = rdr["fName"].ToString();
                        String lName = rdr["lName"].ToString();
                        String occ = rdr["occupation"].ToString();
                        DateTime dob = Convert.ToDateTime(rdr["dateOfBirth"]);

                        chosenStart.Text = pDate;
                        nameDriver.Text = fName;
                        nameDriver2.Text = lName;
                        occDriver.Text = occ;
                        dobDriver.Text = dob.ToString();
                    }
                    myConnection.Close();
                }

            }

            //Calling method to load further policy info used for calculations:
            loadSubsequent();

            //Calling method after data is fully loaded, checking if policy has over 3 claims:
            claimThreshold();

        }

        //LOADSUBSEQUENT - uses SQL statements to calculate and place extra info in labels (to use when calculating premium cost)
        protected void loadSubsequent()
        {
            if (Request.QueryString["policyID"] != null)
            {
                //After initial SQL data load, calculate subsequent information
                int policy = int.Parse(Request.QueryString["policyID"]);
                int primaryDriver = int.Parse(Request.QueryString["primary"]);

                //Open another database connection:
                string connectionString2 = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection myConnection2 = new SqlConnection(connectionString2);
                myConnection2.Open();

                //Second Query: Counting number of Claims on Policy and storing amount in a label:
                string query2 = "SELECT COUNT(Claim.claimID) AS claimCount FROM Claim INNER JOIN Policy ON Policy.PolicyID = Claim.policyID WHERE Policy.policyID=@policy";
                SqlCommand myCommand2 = new SqlCommand(query2, myConnection2);
                myCommand2.Parameters.AddWithValue("@policy", policy);
                myCommand2.Parameters.AddWithValue("@driver", primaryDriver);
                SqlDataReader rdr2 = myCommand2.ExecuteReader();

                while (rdr2.Read())
                {
                    String claims = rdr2["claimCount"].ToString();
                    claimsOnPolicy.Text = claims;
                }
                rdr2.Close();
                //End second query

                //Start third query:
                //Youngest driver by birthdate:
                string query3 = "SELECT TOP 1 dateOfBirth FROM Driver WHERE policyID = @policy ORDER BY dateOfBirth DESC";
                SqlCommand myCommand3 = new SqlCommand(query3, myConnection2);
                myCommand3.Parameters.AddWithValue("@policy", policy);
                SqlDataReader rdr3 = myCommand3.ExecuteReader();

                while (rdr3.Read())
                {
                    String youngest = rdr3["dateOfBirth"].ToString();
                    youngBirthday.Text = youngest;
                }
                rdr3.Close();
                //End third query

                //Start fourth query:
                //Claims on Policy:
                string query4 = "SELECT * FROM Claim WHERE policyID=@policy;";
                SqlCommand myCommand4 = new SqlCommand(query4, myConnection2);
                myCommand4.Parameters.AddWithValue("@policy", policy);
                SqlDataReader rdr4 = myCommand4.ExecuteReader();

                while (rdr4.Read())
                {
                    String claimDate = rdr4["cDate"].ToString();
                    claimDates.Text += claimDate + ",";
                }
                rdr4.Close();
                //End Fourth query

                //Fifth Query: Counting number of Drivers on Policy and storing amount in a label:
                string query5 = "SELECT occupation FROM Driver WHERE policyID=@policy;";
                SqlCommand myCommand5 = new SqlCommand(query5, myConnection2);
                myCommand5.Parameters.AddWithValue("@policy", policy);
                SqlDataReader rdr5 = myCommand5.ExecuteReader();

                while (rdr5.Read())
                {
                    String occs = rdr5["occupation"].ToString();
                    driverOccs.Text += occs + ",";
                }
                rdr5.Close();
                //End second query

                myConnection2.Close();
                //End SQLs

                cost.Text = basePremium.ToString();
            }

        }

        //CLAIMTHRESHOLD - takes number of claims, runs through check method. If it fails, redirect to decline page:
        protected void claimThreshold()
        {
            //Once all info is loaded call method to check if policy has over 3 claims, if so, redirect & decline before going further:
            int claimNum = int.Parse(claimsOnPolicy.Text);
            if (policyClaimsCheck(claimNum) == false)
            {
                Response.Redirect("Decline.aspx?PolicyID=" + Request.QueryString["policyID"] + "&primary=" + Request.QueryString["primary"]);
            }

            //If policy has under 3 claims, move to next stage (checking youngest driver age)
            if (policyClaimsCheck(claimNum) == true)
            {
                youngestDriver();
            }
        }

        //YOUNGESTDRIVER - runs ageChecks on youngestDriver on policy, adds to premium if applicable:
        protected void youngestDriver()
        {
            //Getting birthday & calculating age using method:
            DateTime youngDate = Convert.ToDateTime(youngBirthday.Text);
            DateTime start = Convert.ToDateTime(chosenStart.Text);
            int yAge = calculateAge(start, youngDate);

            //Depending on result, add to premium:
            if (checkAge(yAge) == 10)
            {
                //Get current premium:
             //   cost.DataBind();
             //   premiumCost = Convert.ToDouble(cost.Text);
                //Add 10%:
                double newpremium = 500 + ten;
                //update total:
                cost.Text = newpremium.ToString();

                //Saving new price to variable
                premiumCost = Convert.ToDouble(cost.Text);

            }
            else if (checkAge(yAge) == 20)
            {
                //Get current premium:
            //    cost.DataBind();
            //    premiumCost = Convert.ToDouble(cost.Text);
                //Add 20%:
                double newpremium = 500 + twenty;
                //update total:
                cost.Text = newpremium.ToString();

                //Saving new price to variable
                premiumCost = Convert.ToDouble(cost.Text);
            }

            if (checkAge(yAge) == 0)
            {
                //No age charges, keep premium at base:
                double newpremium = 500;
                cost.Text = newpremium.ToString();

                //Saving new price to variable
                premiumCost = Convert.ToDouble(cost.Text);

            }

            claimTiming();
        }

    /* PLEASE NOTE: Code breaks here as charges do not apply (claimTimeCheck method is always returning an increase of 0, despite the if statement logic or any date which is input.) As a result, addClaimCharge method never receives a charge to apply, and premiumCost does not update no matter when the claim is. */

        //CLAIMTIMING - Checking if claim dates added to policy are within 5 years (using claimTimeCheck method). If so, add the relevant charge:
        protected void claimTiming()
        {

            // Commented out, to skip on to Driver Occupation rules methods (checkPrimaryOcc, checkChauffeur, checkAccountant)
/*

            //Getting claim dates by splitting string into an array:
            DateTime start = Convert.ToDateTime(chosenStart.Text);
            String Dates = claimDates.Text;
            string[] s = Dates.Split(',');
            int count = s.Length;
             
            if (count == 2)
            {
                claim1.Text = s[0];
                claim2.Text = s[1];

                //Perform a time check and if charge is required, add it:
                DateTime current = Convert.ToDateTime(claim1.Text);
                addClaimCharge(current);
               
            }
            if (count == 3)
            {
                claim1.Text = s[0];
                claim2.Text = s[1];
                claim3.Text = s[2];

                //Perform a time check and if charge is required, add it:
                DateTime current = Convert.ToDateTime(claim1.Text);
                DateTime current2 = Convert.ToDateTime(claim2.Text);
                DateTime current3 = Convert.ToDateTime(claim2.Text);
                claimTimeCheck(start, current);
               // addClaimCharge(current);
               // addClaimCharge(current2);
               // addClaimCharge(current3);

            }
 */

         //   checkPrimaryOcc();

            //Skipping claim charges (bug), and occupation charges (incomplete) and submitting with age charges (which work successfully):
           submitResult();
        }

        //CLAIMTIMECHECK - If claim has been made within last five years, add a premium charge of 10/20 percent:
        public int claimTimeCheck(DateTime start, DateTime claimDate)
        {
            DateTime lastYear = start.AddYears(-1);
            DateTime twoYears = start.AddYears(-2);
            DateTime fiveYears = start.AddYears(-5);

            int increase = 0;

            if (lastYear <= claimDate && claimDate < start)
            {
                //claim within year of startDate, increase by 20%;
                increase = 20;
            }

            if (fiveYears >= claimDate && claimDate <= twoYears)
            {
                //Claim within 2-5 years of start date, increase by 10%
                increase = 10;
            }
            //If claim older than 6 years, no increase
            if (claimDate > claimDate.AddYears(-6))
            {
                increase = 0;
            }
            
           // claim3.Text = increase.ToString();
            claim3.Text = lastYear.ToString() + ">=" + claimDate.ToString() + "<" + start.ToString() + " " + increase;

            return increase;
        }

        public int addClaimCharge(DateTime claim)
        {
            DateTime start = Convert.ToDateTime(chosenStart.Text);
            
            int charge = claimTimeCheck(start, claim);

            if (charge == 10)
            {
                //Get current premium:
                cost.DataBind();
                premiumCost = Convert.ToDouble(cost.Text);
                //Add 10%:
                double newpremium = premiumCost + ten;
                //update total:
                cost.Text = newpremium.ToString();
            }
            if (charge == 20)
            {
                //Get current premium:
                cost.DataBind();
                premiumCost = Convert.ToDouble(cost.Text);
                //Add 20%:
                double newpremium = premiumCost + twenty;
                //update total:
                cost.Text = newpremium.ToString();
            }

            return charge;
        }

        //checkPrimaryOcc - takes occupation and runs it against the chauffeur and accountant check methods.
        //Incomplete - require an extra method for the additional driver occupations to be checked & premium updated also
        protected void checkPrimaryOcc()
        {

            //Getting driver details:
            String occ = occDriver.Text;

            //occupation checks:
            if (checkChauffeur(occ) == true)
            {
                //Add a 10% charge if a Chauffeur:
                //Get current premium:
//                cost.DataBind();
//                premiumCost = Convert.ToDouble(cost.Text);
                double newpremium = premiumCost + ten;
                //update total:
                cost.Text = newpremium.ToString();
            }
            else if (checkAccountant(occ) == true)
            {
                //Remove 10% if an accountant:
                //Get current premium:
 //               cost.DataBind();
 //               premiumCost = Convert.ToDouble(cost.Text);
                //Add 10%:
                double newpremium = premiumCost - ten;
                //update total:
                cost.Text = newpremium.ToString();

            }

            if (checkAccountant(occ) == false & checkChauffeur(occ) == false)
            {
                //No changes
            }

            //Missing additional driver occupation checks, but redirect to result for UI purposes:
            submitResult();

        }

        //CALCULATEAGE - takes start date and date of birth to calculate and return current age in years.
        public static int calculateAge(DateTime sDate, DateTime birthdate)
        {
            int age = sDate.Year - birthdate.Year;
            if (sDate < birthdate.AddYears(age))
                age--;

            return age;
        }

        //CHECKAGE - takes youngest driver age for premium calculation:
        public static int checkAge(int age)
        {
            int premiumtoAdd = 0;

            //If youngest driver is 21-25, add 20%
            if (age >= 21 && age <= 25)
            {
                premiumtoAdd = 20;
            }
            //If youngest driver is 26-27, add 10%
            if (age >= 26 && age <= 27)
            {
                premiumtoAdd = 10;
            }
            if (age > 27)
            {
                premiumtoAdd = 0;
            }

            return premiumtoAdd;
        }


        //POLICYCLAIMSCHECK - takes total count of policies and passes if under three, if over three then it fails.
        public static bool policyClaimsCheck(int claimCount)
        {
            bool result = true;

            //If 3 or more claims, return false to fail
            if (claimCount > 3)
            {
                result = false;
            }

            //true = pass, false = fail(decline Policy) 
            return result;

        }

        //CHECKCHAUFFEUR - Checking if occupation is a Chauffeur, if so set increase to True and return
        public static bool checkChauffeur(String job)
        {
            bool increase;

            if (job.Contains("Chauffeur"))
            {
                increase = true;
            }
            if (job.Contains("chauffeur"))
            {
                increase = true;
            }
            else
            {
                increase = false;
            }

            return increase;
        }

        //CHECKACCOUNTANT - Checking if occupation is an Accountant, if so set decrease to True and return
        public static bool checkAccountant(String job)
        {
            bool decrease;

            if (job.Contains("Chauffeur"))
            {
                decrease = true;
            }
            if (job.Contains("chauffeur"))
            {
                decrease = true;
            }
            else
            {
                decrease = false;
            }

            return decrease;
        }
        
        //SUBMITRESULT - once all calculations are complete and final premium has been calculated, redirect to Result page with price of policy.
        protected void submitResult()
        {
            int policy = int.Parse(Request.QueryString["policyID"]);
            int primary = int.Parse(Request.QueryString["primary"]);
            cost.DataBind();
            String premium = cost.Text;
            Response.Redirect("Result.aspx?premium=" + premium + "&policyID=" + policy + "&primary=" + primary);
        }
    }

}