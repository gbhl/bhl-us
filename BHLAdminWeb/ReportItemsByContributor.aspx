﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReportItemsByContributor.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportItemsByContributor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Items By Content Provider</span><hr />
	<p>
        This report shows recent items from a specific content provider.  To view ALL items for a content provider, choose the "Download All" button.
    </p>
    <p>
        NOTE: Use this report to find items not assigned to a particular content provider by selecting "Unknown" from the Content Provider drop-down.
    </p>
    <table>
    <tr>
    <td><b>Content Provider:</b></td>
    <td><asp:DropDownList ID="listInstitutions" runat="server" DataTextField="InstitutionName" DataValueField="InstitutionCode"/></td>
    </tr>
    <tr>
    <td><b>Since:</b> </td>
    <td>    
        <select id="selSince" runat="server">
        <option value="25">Last 25</option>
        <option value="50">Last 50</option>
        <option value="100" selected>Last 100</option>
        <option value="250">Last 250</option>
        </select>
    </td>
    </tr>
    <tr>
    <td><b>Order By:</b></td>
    <td>
        <select id="selSortBy" runat="server">
        <option value="Date" selected>Date</option>
        <option value="Title">Title + Volume</option>
        </select>
    </td>
    </tr>    
    </table>
    <p>
    <asp:Button ID="btnShow" runat="server" Text="Show Items" /> or 
    <asp:Button ID="btnDownload" runat="server" Text="Download All (CSV)" OnClick="btnDownload_Click" />
    </p>
    <p></p>
    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
    <div style="word-break:break-word">
	<asp:GridView ID="itemList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
        AlternatingRowStyle-BackColor="#F7FAFB" Width="1200px" CssClass="boxTable" HeaderStyle-VerticalAlign="Bottom">
		<Columns>
			<asp:BoundField DataField="ItemID" HeaderText="Item ID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10px" />
			<asp:BoundField DataField="BarCode" HeaderText="IA Identifier" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="65px" ItemStyle-Wrap="false" />
			<asp:BoundField DataField="TitleName" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="75px" />
			<asp:BoundField DataField="Volume" HeaderText="Volume" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="true" ItemStyle-Width="35px" />
			<asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" ItemStyle-Width="15px" />
			<asp:BoundField DataField="AuthorListString" HeaderText="Authors" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="50px" />
			<asp:BoundField DataField="CopyrightStatus" HeaderText="Copyright Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="true" ItemStyle-Width="75px" />
			<asp:BoundField DataField="Rights" HeaderText="Rights" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="true" ItemStyle-Width="75px" />
			<asp:BoundField DataField="LicenseUrl" HeaderText="License Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="true" ItemStyle-Width="75px" />
			<asp:BoundField DataField="DueDiligence" HeaderText="Due Diligence" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="true" ItemStyle-Width="75px" />
			<asp:BoundField DataField="CreationDate" HeaderText="Date Added" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="true" ItemStyle-Width="30px" HeaderStyle-Wrap="false" />
		</Columns>
		<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
	</asp:GridView>
    </div>
</asp:Content>
