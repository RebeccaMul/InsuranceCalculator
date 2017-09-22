<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StepThreeClaim.aspx.cs" Inherits="InsuranceProgram.StepThreeClaim" %>

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
                 $("#twoclaim1").datepicker({
                     dateFormat: "dd/mm/yy"
                 });
                 $("#twoclaim2").datepicker({
                     dateFormat: "dd/mm/yy"
                 });
                 $("#threeclaim1").datepicker({
                     dateFormat: "dd/mm/yy"
                 });
                 $("#threeclaim2").datepicker({
                     dateFormat: "dd/mm/yy"
                 });
                 $("#fourclaim1").datepicker({
                     dateFormat: "dd/mm/yy"
                 });
                 $("#fourclaim2").datepicker({
                     dateFormat: "dd/mm/yy"
                 });
                 $("#fiveclaim1").datepicker({
                     dateFormat: "dd/mm/yy"
                 });
                 $("#fiveclaim2").datepicker({
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
            <asp:Label ID="Instruction" runat="server" Text="Please enter any additional driver claim details:" CssClass="instruction" />
            <br />

            <%-- Visible additional Drivers area --%>
            <div id="claims" style="width: 100%; clear: both;">
                <asp:Label ID="DriverDeclaration" runat="server" Text="I don't need any additional claims" /><asp:CheckBox ID="noClaimCheck" runat="server" CssClass="input" OnCheckedChanged="removeClaims" AutoPostBack="true" />


            </div>

            <%-- Add Claims panel, will disappear if box is ticked: --%>
            <asp:Panel ID="moreClaims" runat="server" Style="width: 100%; clear: both">
                <br />

                <%-- Second driver claim div --%>
                <div id="driverdiv2" runat="server" visible="false" style="width: 48%; float: left; margin-right: 10px;">
                    <u>Second Driver:</u>
                    <br />
                    <asp:Label ID="ClaimDeclaration" runat="server" Text="No Claims"></asp:Label><asp:CheckBox ID="claimCheck2" runat="server" CssClass="input" AutoPostBack="true" OnCheckedChanged="hideClaims" />
                    <br />
                    <br />

                    <div id="twoClaims" runat="server">
                        Previous claims:<br />
                        <asp:TextBox ID="twoclaim1" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                         <asp:RangeValidator ID="DateValidator" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="twoclaim1" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                        <asp:TextBox ID="twoclaim2" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck"/>
                         <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="twoclaim2" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                    </div>
                </div>
                <%-- End second driver div --%>

                <%-- Third driver claim div --%>
                <div id="driverdiv3" runat="server" visible="false" style="width: 48%; float: left; margin-right: 10px;">
                    <u>Third Driver:</u>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="No Claims"></asp:Label><asp:CheckBox ID="claimCheck3" runat="server" CssClass="input" AutoPostBack="true" OnCheckedChanged="hideClaims" />
                    <br />
                    <br />

                    <div id="threeClaims" runat="server">
                        Previous claims:<br />
                        <asp:TextBox ID="threeclaim1" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                         <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="threeclaim1" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                        <asp:TextBox ID="threeclaim2" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                         <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="threeclaim2" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                    </div>
                </div>
                <%-- End third driver div --%>

           <%-- Fourth driver claim div --%>
                <div id="driverdiv4" runat="server" visible="false" style="width: 48%; float: left; margin-right: 10px;">
                    <u>Fourth Driver:</u>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="No Claims"></asp:Label><asp:CheckBox ID="claimCheck4" runat="server" CssClass="input" AutoPostBack="true" OnCheckedChanged="hideClaims" />
                    <br />
                    <br />

                    <div id="fourClaims" runat="server">
                        Previous claims:<br />
                        <asp:TextBox ID="fourclaim1" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                         <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="fourclaim1" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                        <asp:TextBox ID="fourclaim2" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                         <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="fourclaim2" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                    </div>
                </div>
                <%-- End fourth driver div --%>

                <%-- Fifth driver claim div --%>
                <div id="driverdiv5" runat="server" visible="false" style="width: 48%; float: left; margin-right: 10px;">
                    <u>Fifth Driver:</u>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="No Claims"></asp:Label><asp:CheckBox ID="claimCheck5" runat="server" CssClass="input" AutoPostBack="true" OnCheckedChanged="hideClaims" />
                    <br />
                    <br />

                    <div id="fiveClaims" runat="server">
                        Previous claims:<br />
                        <asp:TextBox ID="fiveclaim1" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                         <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="fiveclaim1" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                        <asp:TextBox ID="fiveclaim2" runat="server" CssClass="additonalinput" Placeholder="dd/mm/yyyy" ValidationGroup="detailsCheck" />
                         <asp:RangeValidator ID="RangeValidator7" runat="server" ErrorMessage="Please enter a valid date" ControlToValidate="fiveclaim2" Type="Date" MinimumValue="1/1/1900" MaximumValue="1/1/9000" ForeColor="Red"/>
                    </div>
                </div>
                <%-- End fifth driver div --%>

            </asp:Panel>
            <%-- End claimpanel --%>
            <br />
            <asp:Button runat="server" CssClass="submitbtn" Text="Next" OnClick="addClaims" ValidationGroup="detailsCheck" />
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
