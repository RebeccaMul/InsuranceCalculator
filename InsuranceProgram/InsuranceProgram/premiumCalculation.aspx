<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="premiumCalculation.aspx.cs" Inherits="InsuranceProgram.premiumCalculation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motor Insurance Calculator</title>
    <link href="Content/Site.css" rel="stylesheet" />
 
</head>
<body>
    <h1>Motor Insurance Calculator</h1>

    <form id="entryForm" runat="server">

        <div class="entry">
            <br />
            <asp:Label ID="LoadingSign" runat="server" Text="Calculating..." CssClass="loading"></asp:Label><br />
            <br />

               <asp:Label ID="cost" runat="server" CssClass="savedDetails" style="font-size:x-large; color:red;"/>

            <asp:ListView ID="ListView1" runat="server"></asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        <%-- End Central Div --%>

        <%-- Previous approved details column: --%>
        <div class="col1">
            <asp:Label ID="CurrentLabel" runat="server" Text="Current policy details:" CssClass="instruction" /><br />
            Chosen Start Date:
            <asp:Label ID="chosenStart" runat="server" CssClass="savedDetails" /><br />
            Primary Driver:<br />
            <asp:Label ID="nameDriver" runat="server" CssClass="savedDetails" />            
            <asp:Label ID="nameDriver2" runat="server" CssClass="savedDetails" /><br />
            Occupation:
            <asp:Label ID="occDriver" runat="server" CssClass="savedDetails" /><br />
            DOB:
            <asp:Label ID="dobDriver" runat="server" CssClass="savedDetails" /><br />
            <br />
            No. of Claims: <asp:Label ID="claimsOnPolicy" runat="server" CssClass="savedDetails" />   <br />
            Youngest: <asp:Label ID="youngBirthday" runat="server" CssClass="savedDetails" /> <br />
            Claim Dates: <asp:Label ID="claimDates" runat="server" CssClass="savedDetails" /> <br />      
            Occs:<asp:Label ID="driverOccs" runat="server" CssClass="savedDetails" />

            <asp:Label ID="claim1" runat="server" CssClass="savedDetails" ForeColor="red"/>
            <asp:Label ID="claim2" runat="server" CssClass="savedDetails" />
            <asp:Label ID="claim3" runat="server" CssClass="savedDetails" />

        </div>


    </form>
</body>

</html>
