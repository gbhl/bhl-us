<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebStatsNavControl.ascx.cs" Inherits="MOBOT.BHL.AdminWeb.Controls.WebStatsNavControl" %>
<table cellpadding="5" cellspacing="0" style="width: 150px; white-space: nowrap" class="boxTable">
	<tr>
		<td class="boxHeader">
			<asp:Literal ID="litTitle" runat="server"></asp:Literal>
		</td>
	</tr>
	<tr>
		<td class="ItemText">
            <asp:HyperLink ID="dailyLink" runat="server" NavigateUrl="/WebStatsDaily.aspx?id={0}&mid={1}">Daily</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td class="ItemText">
			<asp:HyperLink ID="hourlyLink" runat="server" NavigateUrl="/WebStatsHourly.aspx?id={0}&mid={1}">Hourly</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td class="ItemText">
			<asp:HyperLink ID="ipLink" runat="server" NavigateUrl="/WebStatsIP.aspx?id={0}&mid={1}">IP Address</asp:HyperLink>
		</td>
	</tr>
	<tr runat="server" id="userMenu" visible="false">
		<td class="ItemText">
			<asp:HyperLink ID="userLink" runat="server" NavigateUrl="/WebStatsUser.aspx?id={0}&mid={1}">User</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td class="ItemText">
			<asp:HyperLink ID="historyLink" runat="server" NavigateUrl="/WebHistory.aspx?id={0}&mid={1}">History</asp:HyperLink>
		</td>
	</tr>
	<tr runat="server" id="resultsMenu" visible="false">
		<td class="ItemText">
			<asp:HyperLink ID="resultsLink" runat="server" NavigateUrl="/OpenUrlResultStats.aspx?id={0}&mid={1}">Results</asp:HyperLink>
		</td>
	</tr>
</table>
