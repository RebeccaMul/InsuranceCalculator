<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InsuranceProgram.Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Motor Insurance Calculator</title>
    <link href="Content/Site.css" rel="stylesheet" />
    
</head>
<body>
    <h1>Motor Insurance Calculator</h1>

    <form id="entryForm" runat="server">

          <%-- Additional Driver divs: --%>
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


        <div class="entry">

            <asp:Label ID="Instruction" runat="server" Text="Please enter policy details for the Primary Driver:" CssClass="instruction"></asp:Label> <br />
            <br />

         
            <%-- First driver Text labels div --%>            
            <div id="labels" style="float:left; width:30%;"> 
                  <asp:Label ID="fnamelabel" runat="server" Text="Forename:" CssClass="labels"></asp:Label> <br />
                <br />
            <asp:Label ID="surname" runat="server" Text="Surname:" CssClass="labels"></asp:Label><br /> 
                <br />
                 <asp:Label ID="occupation" runat="server" Text="Occupation:" CssClass="labels"></asp:Label> <br />
                <br />
                <asp:Label ID="dateofbirth" runat="server" Text="Date of Birth:" CssClass="labels"></asp:Label> <br />
           <br />
            <asp:Label ID="sDate" runat="server" Text="Policy start date:" CssClass="labels"></asp:Label> <br />
          
            </div>


            <%-- First driver Input fields div --%>
            <div id="inputs" style="float:right; width:70%;"> 
              <asp:TextBox ID ="fName" runat="server" CssClass="input"></asp:TextBox>
                <br />            
             <asp:TextBox ID ="lName" runat="server" CssClass="input"></asp:TextBox>
                <br />
                 <asp:TextBox ID ="occ" runat="server" CssClass="input"></asp:TextBox>
            <br />
                   <asp:TextBox ID ="dob" runat="server" CssClass="input" Text="dd/mm/yyyy"></asp:TextBox>
           <br />
                
             <asp:TextBox ID ="startDate" runat="server" CssClass="input" Text="dd/mm/yyyy"></asp:TextBox>

            </div>
            <br />
            
             <%-- Claim div area --%>
            <div id="claims" style="width:100%; clear:both;">
                    <asp:Label ID="ClaimsLabel" runat="server" Text="Please add any previous insurance claims, if applicable:"></asp:Label><br /> 
                <asp:Label ID="Label5" runat="server" Text="I have no previous claims"></asp:Label><asp:CheckBox ID="claimCheck" runat="server" CssClass="input" OnCheckedChanged="claimCheck_CheckedChanged"/>
                <br />  <asp:Button runat="server" CssClass="claimb" Text="Add a claim"/> 

            </div>
           
             <%-- Claim panel, will show if box is ticked: --%>
             <asp:Panel ID="addClaim1" runat="server" Visible="false" style="width:100%; clear:both">
                fill out //test
            </asp:Panel>
            <br />

            <asp:Button runat="server" CssClass="submitbtn" OnClick="Calculate" Text="Next"> 
            </asp:Button>

            
        <%-- End Central Div --%>
             </div>

    </form>
</body>
</html>
