﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="SegmentTypeEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.SegmentTypeEdit" ValidateRequest="false" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/EditHistoryLink.ascx" TagName="EditHistoryControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Segment Types</span><hr />
	<br />	
	<div>
		Segment Types:
		<asp:DropDownList ID="ddlSegmentTypes" runat="server" OnSelectedIndexChanged="ddlSegmentTypes_SelectedIndexChanged" AutoPostBack="True" />
	</div>
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<asp:Literal id="litMessage" runat="server"></asp:Literal>
	<br />
	<div class="box" style="padding: 5px; width: 625px;">
		<table cellpadding="4px" width="75%">
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
					<asp:TextBox ID="nameTextBox" runat="server" MaxLength="50" Width="325"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap; padding-top:7px" align="right" valign="top">
					Description:
				</td>
				<td>
					<asp:TextBox ID="descriptionTextBox" runat="server" MaxLength="500" Width="525" TextMode="MultiLine" Height="45"></asp:TextBox>
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
