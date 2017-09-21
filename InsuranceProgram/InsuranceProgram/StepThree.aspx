<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StepThree.aspx.cs" Inherits="InsuranceProgram.StepThree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motor Insurance Calculator</title>
    <link href="Content/Site.css" rel="stylesheet" />
    <%-- JQuery script code - Reference: http://jqueryui.com/datepicker/ --%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
         jQuery(function ($) {
             $("#dobirth").datepicker({
                 dateFormat: "dd/mm/yy"
             });
             $("#d3dob").datepicker({
                 dateFormat: "dd/mm/yy"
             });
             $("#d4dob").datepicker({
                 dateFormat: "dd/mm/yy"
             });
             $("#d5dob").datepicker({
                 dateFormat: "dd/mm/yy"
             });
         });
    </script>
    <%-- End JQuery --%>
</head>
<body>
    <h1>Motor Insurance Calculator</h1>

    <form id="entryForm" runat="server">

        <%-- Centre column (additional drivers): --%>
        <div class="entry">
            <asp:Label ID="Instruction" runat="server" Text="Please enter any additional driver details:" CssClass="instruction" />
            <br />

            <%-- Visible additional Drivers area --%>
            <div id="claims" style="width: 100%; clear: both;">
               
                <asp:Label ID="DriverDeclaration" runat="server" Text="I don't need any additional drivers" /><asp:CheckBox ID="driverCheck" runat="server" CssClass="input" OnCheckedChanged="noDrivers" AutoPostBack="true" />
                <asp:Button ID="driverbtn" runat="server" CssClass="claimb" Text="Add Drivers" OnClick="showAddDrivers" />
            </div>

            <%-- Add Drivers panel, will show if box is ticked: --%>
            <asp:Panel ID="moreDrivers" runat="server" Visible="false" Style="width: 100%; clear: both">
                <br />

                <%-- Drop down list for user to select the number of drivers to add (Max of 4) --%>
                <asp:DropDownList ID="driverNums" runat="server" OnSelectedIndexChanged="driverNumsChange" AutoPostBack="True">
                    <asp:ListItem Enabled="true" Text="How many drivers would you like to add to the policy?" Value="-1" />
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="ReqDriverNum" runat="server" ControlToValidate="driverNums" InitialValue="-1" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck" />
                <br />

                <br />

                <%-- Second driver div, hidden and will be shown based on user's dropdown selection --%>
                <div id="newDriver2" runat="server" visible="false" style="width: 48%; float: left; margin-right: 10px;">
                    <u>Second Driver:</u>
                    <br />
                    <%-- left div: --%>
                    <div id="labels" style="float: left; width: 40%;">
                        <asp:Label ID="fnamelabel" runat="server" Text="Forename:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="fName" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="requiredFName" runat="server" ControlToValidate="fName" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>

                        <br />

                        <asp:Label ID="occupation" runat="server" Text="Occupation:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="occ" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="requiredOcc" runat="server" ControlToValidate="occ" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />
                    </div>

                    <%-- right div: --%>
                    <div id="inputs" style="float: right; width: 40%;">
                        <asp:Label ID="lnamelabel" runat="server" Text="Surname:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="lName" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="requiredLName" runat="server" ControlToValidate="lName" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />

                        <asp:Label ID="dateofbirth" runat="server" Text="Date of Birth:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="dobirth" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="requiredDOB" runat="server" ControlToValidate="dobirth" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator><br />

                    </div>

                </div>
                <%-- End second driver div --%>

                <%-- Third driver div, hidden and will be shown based on user's dropdown selection --%>
                <div id="newDriver3" runat="server" visible="false" style="width: 48%; float: left;">
                    <u>Third Driver:</u>
                    <br />
                    <%-- left div: --%>
                    <div id="labels" style="float: left; width: 40%;">
                        <asp:Label ID="fName3" runat="server" Text="Forename:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="d3fname" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="d3fname" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="occ3" runat="server" Text="Occupation:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="d3occ" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="d3occ" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />
                    </div>

                    <%-- right div: --%>
                    <div id="inputs" style="float: right; width: 40%;">
                        <asp:Label ID="lname3" runat="server" Text="Surname:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="d3lname" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="d3lname" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />

                        <asp:Label ID="dob3" runat="server" Text="Date of Birth:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="d3dob" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="d3dob" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator><br />

                    </div>
                   
                </div>
                <%-- End of third driver div --%>

                <%-- Fourth driver div, hidden and will be shown based on user's dropdown selection --%>
                <div id="newDriver4" runat="server" visible="false" style="width: 48%; float: left; margin-right: 10px;">
                    <br />
                    <u>Fourth Driver:</u>
                    <br />
                    <%-- left div: --%>
                    <div id="labels" style="float: left; width: 40%;">
                        <asp:Label ID="fName4" runat="server" Text="Forename:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="d4fname" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="d4fname" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="occ4" runat="server" Text="Occupation:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="d4occ" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="d4occ" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />
                    </div>

                    <%-- right div: --%>
                    <div id="inputs" style="float: right; width: 40%;">
                        <asp:Label ID="lname4" runat="server" Text="Surname:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="d4lname" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="d4lname" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />

                        <asp:Label ID="dob4" runat="server" Text="Date of Birth:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="d4dob" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="d4dob" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator><br />

                    </div>
                    
                </div>
                <%-- End Fourth driver div --%>


                <%-- Fifth driver div, hidden and will be shown based on user's dropdown selection --%>
                <div id="newDriver5" runat="server" visible="false" style="width: 48%; float: left;">
                    <br />
                    <u>Fifth Driver:</u>
                    <br />
                    <%-- left div: --%>
                    <div id="labels" style="float: left; width: 40%;">
                        <asp:Label ID="fName5" runat="server" Text="Forename:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="d5fname" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="d5fname" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="occ5" runat="server" Text="Occupation:" CssClass="labels"></asp:Label>

                        <asp:TextBox ID="d5occ" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="d5occ" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />
                    </div>

                    <%-- right div: --%>
                    <div id="inputs" style="float: right; width: 40%;">
                        <asp:Label ID="lname5" runat="server" Text="Surname:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="d5lname" runat="server" CssClass="additonalinput" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="d5lname" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                        <br />

                        <asp:Label ID="dob5" runat="server" Text="Date of Birth:" CssClass="labels"></asp:Label>
                        <asp:TextBox ID="d5dob" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="d5dob" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator><br />

                    </div>

                </div>


            </asp:Panel>
            <%-- End hidden additional driver panel --%>
            <br />
            <asp:Button runat="server" CssClass="submitbtn" Text="Next" OnClick="checkDrivers" ValidationGroup="detailsCheck" />
            <br />
            <asp:Label ID="Decline" runat="server" Text=" " Visible="false" ForeColor="Red"></asp:Label>
        </div>
        <%-- End Central Div --%>

        <%-- Previous approved details column: --%>
        <div class="col1">
            <asp:Label ID="CurrentLabel" runat="server" Text="Current policy details:" CssClass="instruction" /><br />
            Chosen Start Date:
            <asp:Label ID="chosenStart" runat="server" CssClass="savedDetails" /><br />
            <br />
            Primary Driver:
            <asp:Label ID="nameDriver" runat="server" CssClass="savedDetails" /><br />
            Occupation:
            <asp:Label ID="occDriver" runat="server" CssClass="savedDetails" /><br />
            DOB:
            <asp:Label ID="dobDriver" runat="server" CssClass="savedDetails" /><br />
            <br />
            <asp:Button runat="server" CssClass="cancelbtn" Text="Cancel" OnClick="Cancel" />
        </div>

    </form>


</body>
</html>
