<%@ Page Title="Detailed Usage Stats" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="WebStatsDetail.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.WebStatsDetail" %>
<%@ Register Src="/Controls/WebStatsNavControl.ascx" TagName="WebStatsNavControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<p>
		<a href="/">&lt; Return to Dashboard</a><br />
		<a href="/monitor">&lt; Return to Server Monitor</a>
    </p>
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
					<span class="pageHeader">Usage Details</span>
				</p>
				<hr />
				<div style="padding-top: 3px; padding-bottom: 3px;">
					<mobot:ErrorControl runat="server" id="errorControl" />
				</div>
				<p>
					<asp:Label ID="totalStatsLabel" runat="server" />
				</p>
				<div class="boxTable" style="width:800px">
					<asp:GridView ID="statsList" runat="server" AutoGenerateColumns="False" CssClass="ItemText" HeaderStyle-Wrap="false" CellPadding="3"
						GridLines="None" RowStyle-BackColor="white" AlternatingRowStyle-BackColor="#FFFFD7" Width="800px" AllowSorting="true"
						 OnRowDataBound="statsList_RowDataBound" OnSorting="statsList_OnSorting">
						<Columns>
						    <asp:TemplateField HeaderText="Date" SortExpression="CreationDate">
							    <ItemTemplate><asp:Label ID="CreationDate" runat="server" /></ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField HeaderText="IP Address" DataField="IPAddress" SortExpression="IPAddress"/>
							<asp:TemplateField HeaderText="User" SortExpression="UserName">
							    <ItemTemplate><asp:Label ID="UserName" runat="server" /></ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Function" SortExpression="RequestTypeName">
							    <ItemTemplate><asp:Label ID="RequestType" runat="server" /></ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Detail" SortExpression="Detail">
							    <ItemTemplate><asp:Label ID="Detail" runat="server" /></ItemTemplate>
							</asp:TemplateField>
						</Columns>
						<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
					</asp:GridView>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
