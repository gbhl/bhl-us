<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="TitleImport.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.TitleImport" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <a href="/TitleSearch.aspx">&lt; Go to Title Search/Import</a><br />
    <a href="/TitleImportHistory.aspx">&lt; View Title Import History</a><br />
    <br />
	<span class="pageHeader">Title Import</span><hr />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
	<asp:Button ID="importButton" runat="server" Text="Import Batch" OnClick="importButton_Click" /> &nbsp;
	<asp:Button ID="cancelButton" runat="server" Text="Cancel Batch" OnClick="cancelButton_Click" /> 
    <div style="overflow:auto;height:600px;width:817px;border-style:none;">
	    <asp:GridView ID="titlesList" runat="server" 
	        AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
	        AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
		    Width="800px" CssClass="boxTable" OnRowCommand="creatorsList_RowCommand"
		    DataKeyNames="MarcID">
		    <Columns>
		        <asp:TemplateField ItemStyle-Width="50px" ItemStyle-VerticalAlign="Top">
		            <ItemTemplate>
		                <a href="#" onclick="window.open('marcdetails.aspx?id=<%# Eval("MarcID") %>', '', 'width=600,height=600,location=0,status=0,scrollbars=1');">Details</a>
		            </ItemTemplate>
		        </asp:TemplateField>
			    <asp:TemplateField HeaderText="Title" ItemStyle-VerticalAlign="Top">
				    <ItemTemplate>
					    <%# Eval( "TitlePart1" ) %>
					    <%# Eval( "TitlePart2" ) != "" ? " " + Eval("TitlePart2") : ""%>
					    <%# Eval("Responsible") != "" ? " " + Eval("Responsible") : ""%>
					    <%# Eval( "Number" ) != "" ? " " + Eval("Number") : ""%>
					    <%# Eval( "Part" ) != "" ? " " + Eval("Part") : ""%>
				    </ItemTemplate>
			    </asp:TemplateField>
			    <asp:TemplateField HeaderText="Matching Title" ItemStyle-VerticalAlign="Top">
			        <ItemTemplate>
			            <%# "<a target='_blank' href='/bibliography/" + Eval("BhlTitleId") + "'>" + Eval("BhlShortTitle") + "</a>"%>
			        </ItemTemplate>
			    </asp:TemplateField>
			    <asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Top" />
		    </Columns>
		    <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
	    </asp:GridView>
    </div>
</asp:Content>
