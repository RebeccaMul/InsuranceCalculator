<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InsuranceProgram.Default2" %>

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
        $(function () {
            $("#startDate").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
    </script>
    <%-- End JQuery --%>
</head>
<body>
    <h1>Motor Insurance Calculator</h1>

    <form id="entryForm" runat="server">

        <div class="entry">

            <asp:Label ID="Instruction" runat="server" Text="Please enter a start date for your policy:" CssClass="instruction"></asp:Label><br />
            <br />

            <%-- Text label div --%>
            <div id="labels" style="float: left; width: 30%;">

                <asp:Label ID="sDate" runat="server" Text="Start date:" CssClass="labels"></asp:Label>
                <br />

            </div>

            <%-- Input field div --%>
            <div id="inputs" style="float: right; width: 70%;">
                <asp:TextBox ID="startDate" runat="server" CssClass="input" Placeholder="dd/mm/yyyy"></asp:TextBox>

            </div><br />
            <br />
            <asp:Button runat="server" CssClass="submitbtn" OnClick="CheckDate" Text="Next" /><br />
            <br />
            <asp:Label ID="Decline" runat="server" Text=" " Visible="false" CssClass="decline"></asp:Label>

        </div>

    </form>
</body>
</html>
