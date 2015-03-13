<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="NoteTypeEdit.aspx.cs" 
Inherits="MOBOT.BHL.AdminWeb.NoteTypeEdit"	ValidateRequest="false" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Title Note Types</span><hr />
	<br />	
	<div>
		Title Note Types:
		<asp:DropDownList ID="ddlNoteTypes" runat="server" OnSelectedIndexChanged="ddlNoteTypes_SelectedIndexChanged" AutoPostBack="True" />
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
					<asp:Label ID="idLabel" runat="server"></asp:Label>
					<asp:HiddenField ID="idHid" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Note Type Name:
				</td>
				<td>
					<asp:TextBox ID="noteTypeNameTextBox" runat="server" MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Note Type Display:
				</td>
				<td>
					<asp:TextBox ID="noteTypeDisplayTextBox" runat="server" MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					MARC Data Field Tag:
				</td>
				<td>
					<asp:TextBox ID="marcDataFieldTagTextBox" runat="server" MaxLength="5"></asp:TextBox>
				</td>
			</tr>
            <tr>
                <td align="right" style="white-space: nowrap">
                    MARC Indicator 1:</td>
                <td>
                    <asp:TextBox ID="marcIndicator1TextBox" runat="server" MaxLength="5"></asp:TextBox></td>
            </tr>
		</table>
		<br />
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
		<asp:Button ID="clearButton" runat="server" Text="Clear" OnClick="clearButton_Click" />
		<asp:Button ID="saveAsNewButton" runat="server" Text="Save As New" OnClick="saveAsNewButton_Click" />
	</div>
</asp:Content>
