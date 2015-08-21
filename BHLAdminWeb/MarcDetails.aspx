<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarcDetails.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.MarcDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MARC Details</title>
	<link rel="stylesheet" type="text/css" runat="server" id="link1" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
	    <asp:GridView ID="marcList" runat="server" 
	        AutoGenerateColumns="False" CellPadding="2" GridLines="None" 
	        AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
		    CssClass="boxTable" Font-Size="Small" HeaderStyle-CssClass="boxHeader">
		    <Columns>
		    <asp:BoundField HeaderText="Tag" DataField="DataFieldTag" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
		    <asp:BoundField HeaderText="Ind1" DataField="Indicator1" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
		    <asp:BoundField HeaderText="Ind2" DataField="Indicator2" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
		    <asp:BoundField HeaderText="Code" DataField="Code" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
		    <asp:BoundField HeaderText="Value" DataField="SubFieldValue" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
		    </Columns>
        </asp:GridView>    
    </div>
    </form>
</body>
</html>
