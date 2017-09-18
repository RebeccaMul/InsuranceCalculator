<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StepThree.aspx.cs" Inherits="InsuranceProgram.StepThree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motor Insurance Calculator</title>
    <link href="Content/Site.css" rel="stylesheet" />
    
</head>
<body>
     <h1>Motor Insurance Calculator</h1>

    <form id="entryForm" runat="server">

    
          <%-- Additional Driver column: --%>
                <div class="addDrivers">
                    Additional Drivers: <br />
                                <asp:Button id="driver2" runat="server" Text="+ Add a Driver" CssClass="driverbtn" OnClick="addDriver2"/>

                        <%-- Driver panel 2, will show if button is clicked: --%>
             <asp:Panel ID="addDriverTwo" runat="server" Visible="false" style="width:100%; clear:both">
                
                   <%-- Text labels div --%>            
            <div id="labelsTwo" style="float:left; width:30%;"> 
                  <asp:Label ID="Label1" runat="server" Text="Forename:" CssClass="labels"></asp:Label> <br />
                <br />
            <asp:Label ID="Label2" runat="server" Text="Surname:" CssClass="labels"></asp:Label><br /> 
                <br />
                 <asp:Label ID="Label3" runat="server" Text="Occupation:" CssClass="labels"></asp:Label> <br />
                <br />
                <asp:Label ID="Label4" runat="server" Text="Date of Birth:" CssClass="labels"></asp:Label> <br />
           <br />
            <asp:Label ID="driverTwoClaim" runat="server" Text="Claim date:" CssClass="labels" Visible="false"></asp:Label> <br />
          
            </div>


            <%-- Input fields div --%>
            <div id="inputsTwo" style="float:right; width:70%;"> 
              <asp:TextBox ID ="TextBox1" runat="server" CssClass="input"></asp:TextBox>
                <br />            
             <asp:TextBox ID ="TextBox2" runat="server" CssClass="input"></asp:TextBox>
                <br />
                 <asp:TextBox ID ="TextBox3" runat="server" CssClass="input"></asp:TextBox>
            <br />
                   <asp:TextBox ID ="TextBox4" runat="server" CssClass="input" Text="dd/mm/yyyy"></asp:TextBox>
           <br />
                
             <asp:TextBox ID ="claimDate" runat="server" CssClass="input" Text="dd/mm/yyyy" Visible="false"></asp:TextBox>

            </div>
            <br />
           
                 <br />

             <asp:Button id="remove2" runat="server" Text="Remove" CssClass="driverbtn" OnClick="removeDriver2"/>
             <asp:Button id="driver3" runat="server" Text="+ Add a Driver" CssClass="driverbtn" OnClick="addDriver3"/>
                 <br />
            </asp:Panel>

            <%-- Driver panel 3, will show if button is clicked: --%>
             <asp:Panel ID="addDriverThree" runat="server" Visible="false" style="width:100%; clear:both">
                3 driver details<br />
             <asp:Button id="remove3" runat="server" Text="Remove" CssClass="driverbtn" OnClick="removeDriver3"/>
             <asp:Button id="driver4" runat="server" Text="+ Add a Driver" CssClass="driverbtn" OnClick="addDriver4"/>
                 <br />
            </asp:Panel>
            
            <%-- Driver panel 4, will show if button is clicked: --%>
             <asp:Panel ID="addDriverFour" runat="server" Visible="false" style="width:100%; clear:both">
                4 driver details<br />
             <asp:Button id="remove4" runat="server" Text="Remove" CssClass="driverbtn" OnClick="removeDriver4"/>
             <asp:Button id="driver5" runat="server" Text="+ Add a Driver" CssClass="driverbtn" OnClick="addDriver5"/>
                 <br />
            </asp:Panel>

            <%-- Driver panel 5, will show if button is clicked: --%>
             <asp:Panel ID="addDriverFive" runat="server" Visible="false" style="width:100%; clear:both">
                5 driver details<br />
             <asp:Button id="Remove" runat="server" Text="Remove" CssClass="driverbtn" OnClick="removeDriver5"/>
                 <br />
            </asp:Panel>
        </div>


    </div>
    </form>
</body>
</html>
