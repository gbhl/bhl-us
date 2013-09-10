<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="TitleSearch.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.TitleSearch"
	ValidateRequest="false" %>

<%@ Register Src="/Controls/PagingUserControl.ascx" TagName="PagingUserControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
    <span class="pageHeader"><asp:Literal runat="server" ID="litHeader">Title</asp:Literal></span><hr />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
    
    <div id="tabs">
    <ul>
    <li><a href="#fragment-1"><span>Search</span></a></li>
    <li runat="server" id="liImport"><a href="#fragment-2"><span>Import</span></a></li>  <!-- remove "fragment-2" to disable -->
    </ul>
	<div id="fragment-1">
	
	<span class="pageSubHeader">Search For Existing Title</span>
	<table cellpadding="0" cellspacing="0">
	<tr>
	<td>

	<asp:Panel ID="searchPanel" Height="30px" CssClass="box" DefaultButton="searchButton" runat="server">
		<div id="simpleSearchPanelDiv">
			<table cellpadding="3" class="SearchText">
				<tr>
					<td style="white-space: nowrap">
						Title ID:
					</td>
					<td>
						<asp:TextBox ID="titleidTextBox" runat="server" CssClass="SearchText" />
					</td>
					<td style="white-space: nowrap">
						MARC Bib ID:
					</td>
					<td>
						<asp:TextBox ID="bibidTextBox" runat="server" CssClass="SearchText" />
					</td>
					<td style="white-space: nowrap">
						Full Title:
					</td>
					<td>
						<asp:TextBox ID="titleTextBox" runat="server" CssClass="SearchText" />
					</td>
					<td>
						<asp:Button ID="searchButton" runat="server" OnClick="searchButton_Click" Text="Search" CssClass="SearchText" />
					</td>
					<td>
						<asp:Button ID="searchExactButton" runat="server" OnClick="searchExactButton_Click" Text="Search Exact" CssClass="SearchText" />
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
	<br />
	<table cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
				<mobot:PagingUserControl id="pagingUserControl" runat="server" onsearch="pagingUserControl_Search" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:GridView ID="gvwResults" runat="server" AutoGenerateColumns="False" CssClass="box"	CellPadding="2" 
				GridLines="None" OnSorting="gvwResults_Sorting" AllowSorting="true" AlternatingRowStyle-BackColor="#F7FAFB"
					RowStyle-BackColor="white" Width="100%" OnRowDataBound="gvwResults_RowDataBound">
					<Columns>
						<asp:BoundField DataField="TitleID" HeaderText="Title ID" SortExpression="TitleID" />
						<asp:BoundField DataField="MARCBibID" HeaderText="MARC Bib ID" SortExpression="MARCBibID" />
						<asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="TitleID" DataNavigateUrlFormatString="/TitleEdit.aspx?id={0}" DataTextField="SortTitle"
							NavigateUrl="/TitleEdit.aspx" SortExpression="SortTitle" ControlStyle-Width="100%" />
					</Columns>
					<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
				</asp:GridView>
			</td>
		</tr>
	</table>
	</td>
	</tr>
	</table>
	
	</div>
	<div runat="server" id="divImport">
    <div id="fragment-2">  <!-- style="display:none" to disable -->
    
	<span class="pageSubHeader">Import Titles From MARC</span>
	<asp:Panel ID="importPanel" Height="130px" CssClass="box" DefaultButton="importButton" runat="server">
		<div id="importDiv">
			<table cellpadding="3" class="SearchText">
				<tr>
					<td style="white-space: nowrap">
						File:
					</td>
					<td>
					    <asp:FileUpload ID="fileUpload" runat="server" Width="400px" />
					</td>
				</tr>
				<tr>
					<td style="white-space: nowrap">
						Format:
					</td>
					<td>
					    <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal">
					        <asp:ListItem Enabled="False" Selected="False" Text="MARC21" Value="1"></asp:ListItem>
					        <asp:ListItem Selected="True" Text="MARCXML" Value="2"></asp:ListItem>
					    </asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<td style="white-space: nowrap">
						Contributing Institution:
					</td>
					<td>
					    <asp:DropDownList ID="listInstitutions" runat="server" DataTextField="InstitutionName" DataValueField="InstitutionCode"></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button ID="importButton" runat="server" OnClick="importButton_Click" Text="Import" CssClass="SearchText" />
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
	
	</div>
	</div>
	</div>
	
    <script type="text/javascript">
        $(document).ready(function() {
            $("#tabs").tabs();
        });
    </script>

</asp:Content>
