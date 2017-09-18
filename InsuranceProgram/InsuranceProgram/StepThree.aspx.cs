using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InsuranceProgram
{
    public partial class StepThree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

    }
}