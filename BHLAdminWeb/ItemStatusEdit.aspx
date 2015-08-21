<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="true" Codebehind="ItemStatusEdit.aspx.cs" 
Inherits="MOBOT.BHL.AdminWeb.ItemStatusEdit"	ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
<span class="pageHeader">Item Statuses</span>
<br /><br />
	<div>
		Item Statuses:
		<asp:DropDownList ID="ddlItemStatuses" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemStatuses_SelectedIndexChanged" />
	</div>
	<br />
	<br />
	<div class="box" style="padding: 5px">
		<table cellpadding="4">
			<tr>
				<td style="white-space: nowrap" align="right">
					ID:
				</td>
				<td>
					<asp:TextBox ID="idTextBox" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hidId" runat="server" />
					(IDs cannot be changed once they are saved)
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Status:
				</td>
				<td>
					<asp:TextBox ID="statusTextBox" runat="server" MaxLength="30"></asp:TextBox>
				</td>
			</tr>
		</table>
		<br />
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
		<asp:Button ID="clearButton" runat="server" Text="Clear" OnClick="clearButton_Click" />
		<asp:Button ID="saveAsNewButton" runat="server" Text="Save As New" OnClick="saveAsNewButton_Click" />
	</div>
	<br />
	<br />
	<fieldset id="errorPanel" visible="false" runat="server" style="border-color: red;">
	</fieldset>
</asp:Content>
