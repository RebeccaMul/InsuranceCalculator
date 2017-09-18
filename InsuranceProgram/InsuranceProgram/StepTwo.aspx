<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StepTwo.aspx.cs" Inherits="InsuranceProgram.StepTwo" %>

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
            $("#claimDate").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
    </script>
    <%-- End JQuery --%></head>
<body>
    <h1>Motor Insurance Calculator</h1>

    <form id="entryForm" runat="server">

        <%-- Previous approved details column: --%>
        <div class="col1">
            <asp:Label ID="CurrentLabel" runat="server" Text="Current policy details:" CssClass="instruction" />
            <br />
            Chosen Start Date:
            <asp:Label ID="chosenStart" runat="server" Style="color: black" /><br />
            <br />
            <asp:Button runat="server" CssClass="cancelbtn" Text="Cancel" OnClick="Cancel" />

        </div>

        <div class="entry">

            <asp:Label ID="Instruction" runat="server" Text="Please enter the Primary Driver's Details:" CssClass="instruction"></asp:Label><br />
            <br />

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
                <asp:RequiredFieldValidator ID="requiredDOB" runat="server" ControlToValidate="dobirth" ErrorMessage="*" ForeColor="Red" ValidationGroup="detailsCheck"></asp:RequiredFieldValidator>
                <br />
            </div>

            <br />

            <%-- Visible Claim area --%>
            <div id="claims" style="width: 100%; clear: both;">
                <asp:Label ID="ClaimsLabel" runat="server" Text="Please add any previous insurance claims, if applicable:"></asp:Label><br />
                <asp:Label ID="ClaimDeclaration" runat="server" Text="I have no previous claims"></asp:Label><asp:CheckBox ID="claimCheck" runat="server" CssClass="input" OnCheckedChanged="noClaims" AutoPostBack="true"/>
                <asp:Button ID="claimbtn" runat="server" CssClass="claimb" Text="Add a claim" OnClick="addClaim" />
            </div>

            <%-- Claim panel, will show if box is ticked: --%>
            <asp:Panel ID="addClaims" runat="server" Visible="false" Style="width: 100%; clear: both">
                <br />

                <%-- Drop down list for user to select the number of claims to add (Max of 5) --%>
                <asp:DropDownList ID="claimNums" runat="server" OnSelectedIndexChanged="claimNumsChange" AutoPostBack="True">
                <asp:ListItem Enabled="true" Text="How many claims would you like to add?" Value="-1"></asp:ListItem>
                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="ReqClaimNum" runat="server" ControlToValidate="claimNums" InitialValue="-1" ErrorMessage="*" ForeColor="Red" ValidationGroup="claimsCheck"/>
                <br />

                 <%-- --%>
                <asp:Label ID="claimDateLabel" runat="server" Text="Date of Claim(s):" Visible="false"/><br />
                <asp:TextBox ID="claimDate" runat="server" CssClass="input" Placeholder="dd/mm/yyyy" Visible="false"/>
                <asp:RequiredFieldValidator ID="RequiredClaimDate" runat="server" ControlToValidate="claimDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="claimsCheck"></asp:RequiredFieldValidator>
 
                <asp:TextBox ID="claim2" runat="server" CssClass="input" Placeholder="dd/mm/yyyy" Visible="false"/>
                <asp:RequiredFieldValidator ID="RequiredClaim2" runat="server" ControlToValidate="claimDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="claimsCheck"></asp:RequiredFieldValidator>
                <asp:TextBox ID="claim3" runat="server" CssClass="input" Placeholder="dd/mm/yyyy" Visible="false"/>
                <asp:RequiredFieldValidator ID="RequiredClaim3" runat="server" ControlToValidate="claimDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="claimsCheck"></asp:RequiredFieldValidator>
                <asp:TextBox ID="claim4" runat="server" CssClass="input" Placeholder="dd/mm/yyyy" Visible="false"/>
                <asp:RequiredFieldValidator ID="RequiredClaim4" runat="server" ControlToValidate="claimDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="slaimsCheck"></asp:RequiredFieldValidator>
                <asp:TextBox ID="claim5" runat="server" CssClass="input" Placeholder="dd/mm/yyyy" Visible="false"/>
                <asp:RequiredFieldValidator ID="RequiredClaim5" runat="server" ControlToValidate="claimDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="claimsCheck"></asp:RequiredFieldValidator>
                
            </asp:Panel>
           
            <asp:Button runat="server" CssClass="submitbtn" Text="Next" OnClick="checkDriver" ValidationGroup="detailsCheck" />
            <br />
            <asp:Label ID="Decline" runat="server" Text=" " Visible="false" ForeColor="Red"></asp:Label>
        </div>
        <%-- End Central Div --%>
    </form>
</body>
</html>
