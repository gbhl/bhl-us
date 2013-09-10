<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="QueueForHarvest.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.QueueForHarvest" 
    ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <a href="/IAHarvestDashboard.aspx">&lt; Return to IA Harvest Dashboard</a><br />
	<br />
	<span class="pageHeader">Queue Item For Download (from Internet Archive)</span><hr />
	<br />	
    <div>
		Internet Archive Identifier:
        <asp:TextBox ID="txtIdentifier" runat="server" MaxLength="50" Width="200"></asp:TextBox>
        <asp:Button ID="btnQueue" runat="server" Text="Queue Item" onclick="btnQueue_Click" />
	</div>
	<br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
	<br />
</asp:Content>
