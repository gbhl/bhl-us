<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportIAItemsPendingApproval.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportIAItemsPendingApproval" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IA Items Pending Approval</title>
    <meta http-equiv="Content-Type" content="application/vnd.ms-excel" />
    <meta http-equiv="Content-disposition" content="attachment; filename="ItemPendingApproval.xls" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvPendingApproval" runat="server" AutoGenerateColumns=false>
        <Columns>
        <asp:BoundField DataField="IAIdentifier" HeaderText="IA Identifier" />
        <asp:BoundField DataField="Sponsor" HeaderText="Sponsor" />
        <asp:BoundField DataField="SponsorName" HeaderText="Sponsor Name" />
        <asp:BoundField DataField="ScanningCenter" HeaderText="Scanning Center" />
        <asp:BoundField DataField="CallNumber" HeaderText="Call Number" />
        <asp:BoundField DataField="ImageCount" HeaderText="Image Count" />
        <asp:BoundField DataField="IdentifierAccessUrl" HeaderText="URL" />
        <asp:BoundField DataField="Volume" HeaderText="Volume" />
        <asp:BoundField DataField="Note" HeaderText="Note" />
        <asp:BoundField DataField="ScanOperator" HeaderText="Scan Operator" />
        <asp:BoundField DataField="ScanDate" HeaderText="Scan Date" />
        <asp:BoundField DataField="IAAddedDate" HeaderText="IA Added Date" />
        </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
