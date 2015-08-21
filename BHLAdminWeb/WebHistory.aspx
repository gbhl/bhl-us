<%@ Page Title="Historical Stats" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="WebHistory.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.WebHistory" %>
<%@ Register Src="/Controls/WebStatsNavControl.ascx" TagName="WebStatsNavControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
	<table cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td style="width: 150px; white-space: nowrap" valign="top">
				<mobot:WebStatsNavControl runat="server" id="webStatsNavControl" />
			</td>
			<td style="width: 10px">
				&nbsp;
			</td>
			<td style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px" valign="top" class="boxTable">
				<p>
					<span class="pageHeader">History</span>
				</p>
				<hr />
				<br />
				<span class="ItemText">Start Date</span>
				<asp:TextBox ID="startDateTextBox" runat="server" Width="75px"></asp:TextBox>
				<span class="ItemText">End Date</span>
				<asp:TextBox ID="endDateTextBox" runat="server" Width="75px"></asp:TextBox>
				<span class="ItemText">Request Type</span>
                <asp:DropDownList ID="RequestTypeList" runat="server" DataTextField="RequestTypeName" DataValueField="RequestTypeID" CssClass="SearchText" />
				<asp:Button ID="showButton" runat="server" Text="Show Usage" OnClick="showButton_Click" />
				<br />
				<div style="padding-top: 3px; padding-bottom: 3px;">
					<mobot:ErrorControl runat="server" id="errorControl" />
				</div>
                <div id='chart_div' style='width: 100%; height: 461px;'></div>
			</td>
		</tr>
	</table>
</asp:Content>
