<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ImageServerEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ImageServerEdit" 
    ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Image Server</span><hr />
    <br />
    <asp:Label runat="server" ID="lblMessage" ForeColor="Blue" />
	<p>
        Select the location from which to serve page images.
    </p>
    <asp:RadioButtonList runat="server" ID="rdoListServers" OnSelectedIndexChanged="rdoListServers_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Text="Internet Archive" Value="http://www.archive.org" />
        <asp:ListItem Text="BHL Cluster (Woods Hole)" Value="http://cluster.biodiversitylibrary.org" />
        <asp:ListItem Text="Image Load Director (MOBOT)" Value="http://192.104.39.52:443" />
        <asp:ListItem Text="Other" Value="Other" />
    </asp:RadioButtonList>
    <div runat="server" id="divAddress" visible="false"><asp:TextBox runat="server" ID="txtAddress" Columns="30" />&nbsp;Please specify the base domain (i.e. http://cluster.biodiversitylibrary.org)</div>
    <br /><br />
	<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
</asp:Content>
