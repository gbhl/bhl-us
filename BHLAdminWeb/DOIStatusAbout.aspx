<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DOIStatusAbout.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.DOIStatusAbout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DOI Statuses</title>
	<link rel="stylesheet" type="text/css" runat="server" id="link1" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>DOI Item Statuses</h3>
        <asp:DataList ID="dlStatus" runat="server">
        <HeaderTemplate>
        <table><tr><td><b>Status</b></td><td>&nbsp;</td><td><b>Description</b></td><td nowrap><b># of DOIs</b></td></tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr valign="top"><td nowrap><%# DataBinder.Eval(Container.DataItem, "DOIStatusName") %></td><td></td><td><%# DataBinder.Eval(Container.DataItem, "DOIStatusDescription")%></td><td align="right"><%# DataBinder.Eval(Container.DataItem, "NumberOfDOIs")%></td></tr>
        </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
