<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="AuthorSearch.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.AuthorSearch" %>
<%@ Register Src="/Controls/PagingUserControl.ascx" TagName="PagingUserControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
    <span class="pageHeader"><asp:Literal runat="server" ID="litHeader">Author Search</asp:Literal></span>&nbsp;&nbsp;
    <a href="/AuthorEdit.aspx?id=0" class="small">Add New Author</a>
    <hr />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
	<table cellpadding="0" cellspacing="0">
	<tr>
	<td>
	<asp:Panel ID="searchPanel" Height="30px" CssClass="box" DefaultButton="searchButton" runat="server">
		<div id="simpleSearchPanelDiv">
			<table cellpadding="3" class="SearchText">
				<tr>
					<td style="white-space: nowrap">
						Author ID:
					</td>
					<td>
						<asp:TextBox ID="txtAuthorID" runat="server" CssClass="SearchText" />
					</td>
					<td style="white-space: nowrap">
						Author Name:
					</td>
					<td>
						<asp:TextBox ID="txtName" runat="server" CssClass="SearchText" />
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
					RowStyle-BackColor="white" Width="100%">
					<Columns>
						<asp:HyperLinkField HeaderText="Author ID" DataNavigateUrlFields="AuthorID" DataNavigateUrlFormatString="/AuthorEdit.aspx?id={0}" DataTextField="AuthorID"
							NavigateUrl="/AuthorEdit.aspx" ControlStyle-Width="100%" HeaderStyle-HorizontalAlign="Left" />
						<asp:BoundField DataField="NameExtended" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" />
					    <asp:TemplateField HeaderText="Is Active" HeaderStyle-HorizontalAlign="Left">
						    <ItemTemplate>
						        <asp:CheckBox ID="isActiveCheckBox" Enabled="false" Checked='<%#(Convert.ToInt32(Eval("IsActive")) == 1)%>' runat="server" />
						    </ItemTemplate>
					    </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a id="hypTitles" href="#" onclick="javascript:window.open('AuthorTitlesList.aspx?id=<%# Eval( "AuthorID" ) %>', '', 'width=800,height=600,location=0,status=0,scrollbars=1');">View Titles</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a id="hypSegments" href="#" onclick="javascript:window.open('AuthorSegmentsList.aspx?id=<%# Eval( "AuthorID") %>', '', 'width=800,height=600,location=0,status=0,scrollbars=1');">View Segments</a>
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
