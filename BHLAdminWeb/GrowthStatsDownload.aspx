<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrowthStatsDownload.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.GrowthStatsDownload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BHL Growth Stats</title>
    <meta http-equiv="Content-Type" content="application/vnd.ms-excel" />
    <meta http-equiv="Content-disposition" content="attachment; filename="BHLGrowthStats.xls" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal runat="server" ID="litInstitution"></asp:Literal>
    <br /><b>Titles Created</b><br />
    <div>
        <asp:GridView ID="gvTitles" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Titles" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Items Created</b><br />
    <div>
        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Items" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Pages Created</b><br />
    <div>
        <asp:GridView ID="gvPages" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Pages" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Names Created</b><br />
    <div>
        <asp:GridView ID="gvNames" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Names" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Segments Created</b><br />
    <div>
        <asp:GridView ID="gvSegments" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Segments" />
        </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
