<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="VaultEdit.aspx.cs" 
Inherits="MOBOT.BHL.AdminWeb.VaultEdit"	ValidateRequest="false" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Vaults</span><hr />
	<br />	
	<div>
		Vaults:
		<asp:DropDownList ID="ddlVaults" runat="server" OnSelectedIndexChanged="ddlVaults_SelectedIndexChanged" AutoPostBack="True" />
	</div>
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
	<div class="box" style="padding: 5px;margin-right:5px">
		<table cellpadding="4px" width="100%">
			<tr>
				<td style="white-space: nowrap" align="right">
					ID:
				</td>
				<td width="100%">
					<asp:TextBox ID="idTextBox" runat="server"></asp:TextBox>
					<asp:HiddenField ID="idHid" runat="server" />
					(Vault IDs cannot be changed once they are saved)
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Server:
				</td>
				<td>
					<asp:TextBox ID="serverTextBox" runat="server" MaxLength="30"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Folder Share:
				</td>
				<td>
					<asp:TextBox ID="folderShareTextBox" runat="server" MaxLength="30"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Web Virtual Folder:
				</td>
				<td>
					<asp:TextBox ID="webVirtualFolderTextBox" runat="server" MaxLength="30"></asp:TextBox>
				</td>
			</tr>
            <tr>
                <td align="right" style="white-space: nowrap">
                    OCR Folder Share:</td>
                <td>
                    <asp:TextBox ID="OCRFolderShareTextBox" runat="server" MaxLength="100" Width="525px"></asp:TextBox></td>
            </tr>
		</table>
		<br />
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
		<asp:Button ID="clearButton" runat="server" Text="Clear" OnClick="clearButton_Click" />
		<asp:Button ID="saveAsNewButton" runat="server" Text="Save As New" OnClick="saveAsNewButton_Click" />
	</div>
</asp:Content>
