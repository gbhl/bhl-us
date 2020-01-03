<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BioStorSegmentsForItem.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.BioStorSegmentsForItem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Segments For Item</title>
	<link rel="stylesheet" type="text/css" runat="server" id="link1" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Segments</h3>
        <asp:DataList ID="dlSegments" runat="server">
        <HeaderTemplate>
        <table>
            <tr>
                <td><b>Type</b></td>
                <td>&nbsp;</td>
                <td><b>Title</b></td>
                <td><b>Volume</b></td>
                <td><b>Date</b></td>
                <td align="right"><b>Start Page</b></td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr valign="top">
                <td nowrap><%# DataBinder.Eval(Container.DataItem, "Genre") %></td>
                <td>&nbsp;</td>
                <td><%# DataBinder.Eval(Container.DataItem, "Title") %></td>
                <td nowrap align="center"><%# DataBinder.Eval(Container.DataItem, "Volume") %></td>
                <td nowrap><%# DataBinder.Eval(Container.DataItem, "Date") %></td>
                <td nowrap align="right"><%# DataBinder.Eval(Container.DataItem, "StartPageNumber")%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
