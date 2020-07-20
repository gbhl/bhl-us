<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="SegmentSearch.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.SegmentSearch" %>
<%@ Register Src="/Controls/PagingUserControl.ascx" TagName="PagingUserControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
    <span class="pageHeader"><asp:Literal runat="server" ID="litHeader">Segment Search</asp:Literal></span>&nbsp;&nbsp;
    <a href="/SegmentEdit.aspx?id=0" class="small">Add New Segment</a>
    <hr />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
	<table cellpadding="0" cellspacing="0">
	<tr>
	<td>
	<asp:Panel ID="searchPanel" Height="30px" Width="99%" CssClass="box" DefaultButton="searchButton" runat="server">
		<div id="simpleSearchPanelDiv">
			<table cellpadding="3" class="SearchText">
				<tr>
					<td style="white-space: nowrap">
						Segment ID:
					</td>
					<td>
						<asp:TextBox ID="txtSegmentID" runat="server" CssClass="SearchText" />
					</td>
					<td style="white-space: nowrap">
						Segment Title:
					</td>
					<td>
						<asp:TextBox ID="txtTitle" runat="server" CssClass="SearchText" />
					</td>
					<td>
						<asp:Button ID="searchButton" runat="server" OnClick="searchButton_Click" Text="Search" CssClass="SearchText" />
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
	<br />
	<table cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
				<asp:GridView ID="gvwResults" runat="server" AutoGenerateColumns="False" 
                    CssClass="box"	CellPadding="4"
				    GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
					RowStyle-BackColor="white" Width="99%">
					<Columns>
						<asp:HyperLinkField HeaderText="Segment ID" DataNavigateUrlFields="SegmentID" DataNavigateUrlFormatString="/SegmentEdit.aspx?id={0}" DataTextField="SegmentID"
							NavigateUrl="/SegmentEdit.aspx" ControlStyle-Width="100%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" />
                        <asp:BoundField DataField="GenreName" HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
						<asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                        <asp:BoundField DataField="Authors" HeaderText="Authors" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
						<asp:BoundField DataField="ContainerTitle" HeaderText="Container" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                        <asp:BoundField DataField="Volume" HeaderText="Vol" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                        <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Top" />
					    <asp:TemplateField HeaderText="Is Active" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top">
						    <ItemTemplate>
						        <asp:CheckBox ID="isActiveCheckBox" Enabled="false" Checked='<%#(Convert.ToInt32(Eval("SegmentStatusID")) <= 20)%>' runat="server" />
						    </ItemTemplate>
					    </asp:TemplateField>
					</Columns>
					<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
				</asp:GridView>
			</td>
		</tr>
	</table>
	</td>
	</tr>
	</table>
</asp:Content>
