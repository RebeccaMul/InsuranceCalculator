<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Decline.aspx.cs" Inherits="InsuranceProgram.Decline" %>

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

            <b><asp:Label ID="DeclineTitle" runat="server" Text="Policy Declined" ForeColor="Red"></asp:Label><br /></b>
            <br />
            <asp:Label ID="Reason" runat="server" Text="Unfortunately, your policy has been declined for the following reason:"></asp:Label><br />
            <br />
            <i>Policy has more than 3 claims</i><br />
            <br />
            <asp:Button runat="server" CssClass="submitbtn" Text="Return to Home" OnClick="Redirect"/>
            <br />
            </div>

    </form>
</body>

</html>
