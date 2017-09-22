<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="InsuranceProgram.Result" %>

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
            Congratulations, <asp:Label ID="Name" runat="server" Text="" ></asp:Label> your policy which is due to start on the <asp:Label ID="Date" runat="server" Text="" ></asp:Label> has been <b>approved.</b><br />
            <br />
            Policy Price <b><asp:Label ID="Price" runat="server" Text="" ></asp:Label><br /></b>
            <br />
            <asp:Button runat="server" CssClass="submitbtn" Text="Calculate another" OnClick="Redirect"/>
            <br />
            </div>

    </form>
</body>
</html>
