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
	<tr><td class="BlackHeading">Titles Inserted</td><td><asp:Literal runat="server" ID="litInserted"></asp:Literal></td></tr>
	<tr><td class="BlackHeading">Titles Updated</td><td><asp:Literal runat="server" ID="litUpdated"></asp:Literal></td></tr>
	<tr><td class="BlackHeading">Errors</td><td><asp:Literal runat="server" ID="litErrors"></asp:Literal></td></tr>
	</table>
	<br />
    <asp:Literal runat="server" ID="litErrHeader" Text="<font color='red'><b>Error Details</b></font>" Visible="false"></asp:Literal>
	<asp:DataGrid AutoGenerateColumns=true ShowHeader=false runat="server" ID="dlErrors" GridLines="None" CellPadding="3" ItemStyle-ForeColor="Red">
	</asp:DataGrid>
</asp:Content>
