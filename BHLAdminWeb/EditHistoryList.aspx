<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditHistoryList.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.EditHistoryList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit History</title>
	<link rel="stylesheet" type="text/css" runat="server" id="link1" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Edit History for <%:entityName%> <%:entityID%></h2>
	        <asp:GridView ID="historyList" runat="server" 
	            AutoGenerateColumns="False" CellPadding="2" GridLines="None" 
	            AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
		        CssClass="boxTable" Font-Size="Small" HeaderStyle-CssClass="boxHeader" Width="100%">
		        <Columns>
		            <asp:BoundField HeaderText="Date" DataField="EditDate" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" />
		            <asp:BoundField HeaderText="Entity" DataField="EntityNameClean" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
		            <asp:BoundField HeaderText="Detail" DataField="EntityDetail" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Operation" DataField="Operation" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="User" DataField="User" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" />
		        </Columns>
            </asp:GridView>    
        </div>
    </form>
</body>
</html>
