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
        <asp:ListItem Text="Other" Value="Other" />
    </asp:RadioButtonList>
    <div runat="server" id="divAddress" visible="false" style="margin-left:20px">
        <asp:TextBox runat="server" ID="txtAddress" Columns="50" Text="https://bhl-open-data.s3.us-east-2.amazonaws.com" /><br />
        <div style="padding-top:2px">Please specify the base domain (i.e. http://cluster.biodiversitylibrary.org)</div>
    </div>
    <br /><br />
	<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
</asp:Content>
