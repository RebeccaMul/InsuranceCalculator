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
        // jQuery(function ($) {
        //     $("#dobirth").datepicker({
        //         dateFormat: "dd/mm/yy"
        //     });
        // });
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
                <br />
                <asp:Label ID="DriverDeclaration" runat="server" Text="I don't need any additional drivers" /><asp:CheckBox ID="driverCheck" runat="server" CssClass="input" OnCheckedChanged="noDrivers" AutoPostBack="true" />
                <asp:Button ID="driverbtn" runat="server" CssClass="claimb" Text="Add a Driver" OnClick="showAddDrivers" />
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

                <%-- Additional Driver entry detail section: --%>
                <asp:Label ID="driverDetailLabel" runat="server" Text="Driver Details:" Visible="false" /><br />

                <div ID="newDriver2" runat="server" Visible="false" Style="width: 100%; clear: both">

                    <%-- First driver Text labels div --%>
                    <div id="labels" style="float: left; width: 30%;">
                        <asp:Label ID="fnamelabel" runat="server" Text="Forename:" CssClass="labels"></asp:Label>
                        <br />
                        <asp:Label ID="fNameDecline" runat="server" Text=" " Visible="false" ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Label ID="surname" runat="server" Text="Surname:" CssClass="labels"></asp:Label>
                        <br />
                        <asp:Label ID="lNameDecline" runat="server" Text=" " Visible="false" ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Label ID="occupation" runat="server" Text="Occupation:" CssClass="labels"></asp:Label>
                        <br />
                        <asp:Label ID="occDecline" runat="server" Text=" " Visible="false" ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Label ID="dateofbirth" runat="server" Text="Date of Birth:" CssClass="labels"></asp:Label>
                        <br />
                        <asp:Label ID="dobDecline" runat="server" Text=" " Visible="false" ForeColor="Red"></asp:Label>
                        <br />
                    </div>

                                <%-- First driver Input fields div --%>
            <div id="inputs" style="float: right; width: 70%;">
                <asp:TextBox ID="fName" runat="server" CssClass="input" ValidationGroup="detailsCheck"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredFName" runat="server" ControlToValidate="fName" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="lName" runat="server" CssClass="input" />
                <asp:RequiredFieldValidator ID="requiredLName" runat="server" ControlToValidate="lName" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="occ" runat="server" CssClass="input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredOcc" runat="server" ControlToValidate="occ" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="dobirth" runat="server" CssClass="input" Placeholder="dd/mm/yyyy" />
                <asp:RequiredFieldValidator ID="requiredDOB" runat="server" ControlToValidate="dobirth" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator><br />
                <asp:RangeValidator ID="DateValidator" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="dobirth" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                <br />
            </div>



                    </div>
               </asp:Panel>

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
