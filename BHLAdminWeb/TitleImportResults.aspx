<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="TitleImportResults.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.TitleImportResults" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
	<span class="pageHeader">Title Import Results</span><hr />
	<mobot:ErrorControl runat="server" id="errorControl" Visible="false"></mobot:ErrorControl>
	<br />
	<table style="width:100%">
	<tr><td class="BlackHeading">Titles Inserted</td><td><asp:Literal runat="server" ID="litInsertedCount"></asp:Literal></td></tr>
	<tr><td class="BlackHeading">Titles Updated</td><td><asp:Literal runat="server" ID="litUpdatedCount"></asp:Literal></td></tr>
	<tr><td class="BlackHeading">Errors</td><td><asp:Literal runat="server" ID="litErrorCount"></asp:Literal></td></tr>
	</table>
    <p>&nbsp;</p>
    <p>
        <asp:Literal runat="server" ID="litInserted" Text="<b>Inserted Titles</b>" Visible="false"></asp:Literal>
        <asp:DataGrid AutoGenerateColumns="false" ShowHeader="true" runat="server" ID="dlInserted" GridLines="None" CellPadding="3" HeaderStyle-Font-Italic="true">
            <Columns>
                <asp:BoundColumn DataField="TitleID" HeaderText="ID" ></asp:BoundColumn>
                <asp:BoundColumn DataField="ShortTitle" HeaderText="Title"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </p>
    <p>
        <asp:Literal runat="server" ID="litUpdated" Text="<b>Updated Titles</b>" Visible="false"></asp:Literal>
        <asp:DataGrid AutoGenerateColumns="false" ShowHeader="true" runat="server" ID="dlUpdated" GridLines="None" CellPadding="3" HeaderStyle-Font-Italic="true">
            <Columns>
                <asp:BoundColumn DataField="TitleID" HeaderText="ID" ></asp:BoundColumn>
                <asp:BoundColumn DataField="ShortTitle" HeaderText="Title"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </p>
	<p>
        <asp:Literal runat="server" ID="litErrHeader" Text="<font color='red'><b>Error Details</b></font>" Visible="false"></asp:Literal>
	    <asp:DataGrid AutoGenerateColumns="true" ShowHeader="false" runat="server" ID="dlErrors" GridLines="None" CellPadding="3" ItemStyle-ForeColor="Red"></asp:DataGrid>
    </p>
</asp:Content>
