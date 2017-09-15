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

            if (IsPostBack)
            {
                //Postback actions:
            }
            else
            {
                //Do nothing
            }

        }

        //Calculate policy price method
        protected void Calculate(object sender, EventArgs e)
        {

            //If driver X div visible, include their details in insert
            //if claim X visible, insert to claims, attached to driver

            //Declaring Strings to store personal info:
            String primaryName, primaryLName, primaryOccupation, primaryDOB, sDate;
            /*
            fName.Text = primaryName;
            lName.Text = primaryLName;
            occ.Text = primaryOccupation;
            dob.Text = primaryDOB; */
            startDate.DataBind();
            sDate = startDate.Text;

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            string sql = "INSERT INTO Policy (startDate) VALUES (@start)";
            SqlCommand cmd = new SqlCommand(sql, myConnection);
            cmd.Parameters.Add(new SqlParameter("@start", sDate));
            int rowsAffected = cmd.ExecuteNonQuery();
            myConnection.Close();
           
            /*
            //second add driver, after policy created
            string sql2 = "INSERT INTO driver (name, lname, occupation, dob) VALUES (@params)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@name", fName.Text));
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close(); */
        //    }

        
        }


        //Add driver methods, displays a new driver details panel when one of the 'Add driver' buttons are clicked - for up to 4 additonal drivers.
        protected void addDriver2(object sender, EventArgs e)
        {
            addDriverTwo.Visible = true;
            driver2.Visible = false;
        }

        protected void addDriver3(object sender, EventArgs e)
        {
            addDriverThree.Visible = true;
        }

        protected void addDriver4(object sender, EventArgs e)
        {
            addDriverFour.Visible = true;
        }

        protected void addDriver5(object sender, EventArgs e)
        {
            addDriverFive.Visible = true;
        }

        //Remove driver methods, removes a driver details panel which has been previously added
        protected void removeDriver2(object sender, EventArgs e)
        {
            addDriverTwo.Visible = false;
            driver2.Visible = true;
        }
        protected void removeDriver3(object sender, EventArgs e)
        {
            addDriverThree.Visible = false;
        }
        protected void removeDriver4(object sender, EventArgs e)
        {
            addDriverFour.Visible = false;
        }
        protected void removeDriver5(object sender, EventArgs e)
        {
            addDriverFive.Visible = false;
        }


        //Method if checkbox changes, show the claims panel
        protected void claimCheck_CheckedChanged(object sender, EventArgs e)
        {
            claimCheck.DataBind();
            ;
            if (addClaim1.Visible == false)
            {
                addClaim1.Visible = true;
            }
            else if (addClaim1.Visible == true)
            {
                addClaim1.Visible = false;
            }

        }
    }
}