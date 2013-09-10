<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IAHarvestItemStatusAbout.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.IAHarvestItemStatusAbout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Statuses</title>
	<link rel="stylesheet" type="text/css" runat="server" id="link1" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>IA Harvest Item Statuses</h3>
        <asp:DataList ID="dlStatus" runat="server">
        <HeaderTemplate>
        <table><tr><td><b>Status</b></td><td>&nbsp;</td><td><b>Description</b></td></tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr valign="top"><td nowrap><%# DataBinder.Eval(Container.DataItem, "Status") %></td><td></td><td><%# DataBinder.Eval(Container.DataItem, "Description")%></td></tr>
        </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
