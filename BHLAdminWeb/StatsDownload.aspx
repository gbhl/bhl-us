<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatsDownload.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.StatsDownload" EnableViewState="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BHL Expanded Stats</title>
    <meta http-equiv="Content-Type" content="application/vnd.ms-excel" />
    <meta http-equiv="Content-disposition" content="attachment; filename="BHLExpandedStats.xls" />
</head>
<body>
    <form id="form1" runat="server">
    <br /><b>Titles In Production</b><br />
    <div>
        <asp:GridView ID="gvProductionTitles" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="InstitutionName" HeaderText="Contributor Name" />
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Titles" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Items In Production</b><br />
    <div>
        <asp:GridView ID="gvProductionItems" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="InstitutionName" HeaderText="Contributor Name" />
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Items" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Items Scanned</b><br />
    <div>
        <asp:GridView ID="gvScannedItems" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="InstitutionName" HeaderText="Contributor Name" />
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Items" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Pages In Production</b><br />
    <div>
        <asp:GridView ID="gvProductionPages" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="InstitutionName" HeaderText="Contributor Name" />
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Pages" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Names In Production</b><br />
    <div>
        <asp:GridView ID="gvProductionNames" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="InstitutionName" HeaderText="Contributor Name" />
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Names" />
        </Columns>
        </asp:GridView>
    </div>
    <br /><b>Segments In Production</b><br />
    <div>
        <asp:GridView ID="gvProductionSegments" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="InstitutionName" HeaderText="Contributor Name" />
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="StatValue" HeaderText="Segments" />
        </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
