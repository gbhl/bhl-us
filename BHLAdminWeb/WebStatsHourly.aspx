<%@ Page Title="Hourly Stats" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="WebStatsHourly.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.WebStatsHourly" %>
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
					<span class="pageHeader">Hourly Usage</span>
				</p>
				<hr />
				<br />
				<span class="ItemText">Date</span>
				<asp:TextBox ID="dateTextBox" runat="server" Width="75px"></asp:TextBox>
				<asp:Button ID="showButton" runat="server" Text="Show Usage" OnClick="showButton_Click" />
				<br />
				<div style="padding-top: 3px; padding-bottom: 3px;">
					<mobot:ErrorControl runat="server" id="errorControl" />
				</div>
				<p>
					<asp:Label ID="totalStatsLabel" runat="server" />
				</p>
				<asp:GridView ID="statsList" runat="server" AutoGenerateColumns="False" CssClass="boxTable" HeaderStyle-Wrap="false" CellPadding="3"
					GridLines="None" Width="250px" RowStyle-BackColor="white" AlternatingRowStyle-BackColor="#FFFFD7" AllowSorting="false" OnRowDataBound="gsList_RowDataBound">
					<Columns>
						<asp:TemplateField HeaderText="Hour" HeaderStyle-HorizontalAlign="Left">
							<ItemTemplate>
								<%# GetHour( Container.DataItem ) %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<div style="padding-right: 10px">
									<asp:Hyperlink ID="Total" runat="server" />
								</div>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
							<ItemTemplate>
								<%# GetBar( Container.DataItem ) %>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
					<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
