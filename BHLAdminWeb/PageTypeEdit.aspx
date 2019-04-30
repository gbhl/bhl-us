<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="PageTypeEdit.aspx.cs" 
Inherits="MOBOT.BHL.AdminWeb.PageTypeEdit"	ValidateRequest="false" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/EditHistoryLink.ascx" TagName="EditHistoryControl" TagPrefix="mobot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Page Types</span><hr />
	<br />	
		<div>
		Page Types:
		<asp:DropDownList ID="ddlPageTypes" runat="server" OnSelectedIndexChanged="ddlPageTypes_SelectedIndexChanged" AutoPostBack="True" />
	</div>
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
	<div class="box" style="padding: 5px;margin-right:5px;width:550px;">
		<table cellpadding="4px" width="100%">
			<tr>
				<td style="white-space: nowrap" align="right">
					ID:
				</td>
				<td width="100%">
					<asp:Label ID="idLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Name:
				</td>
				<td>
					<asp:TextBox ID="nameTextBox" runat="server" MaxLength="30"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Description:
				</td>
				<td>
					<asp:TextBox ID="descriptionTextBox" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
				</td>
			</tr>
		</table>
		<br />
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
		<asp:Button ID="clearButton" runat="server" Text="Clear" OnClick="clearButton_Click" />
		<asp:Button ID="saveAsNewButton" runat="server" Text="Save As New" OnClick="saveAsNewButton_Click" />
		<div style="float:right;"><mobot:EditHistoryControl runat="server" id="editHistoryControl" /></div>
	</div>
</asp:Content>
