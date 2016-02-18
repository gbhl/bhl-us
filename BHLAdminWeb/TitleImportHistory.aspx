<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="TitleImportHistory.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.TitleImportHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
    <span class="pageHeader">Title Import History</span><hr />
	<br />
    <table cellpadding="3" width="750">
        <tr>
            <td>
                Contributor:
                <asp:DropDownList ID="listInstitutions" runat="server" DataTextField="InstitutionName" DataValueField="InstitutionCode" AutoPostBack="True" OnSelectedIndexChanged="listInstitutions_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal Visible="false" ID="litNoRecords" runat="server" Text="No records found"></asp:Literal>
                <asp:GridView ID="gvwImportStats" runat="server" AutoGenerateColumns="False" CssClass="box"	CellPadding="2" 
	                GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="100%" 
	                OnRowDataBound="gvwImportStats_RowDataBound" HeaderStyle-VerticalAlign="Bottom">
	                <Columns>
	                    <asp:TemplateField HeaderText="Batch ID">
	                        <ItemTemplate>
	                            <asp:HyperLink ID="batchLink" runat="server" NavigateUrl="#" Text='<%# Eval("MarcImportBatchID") %>'></asp:HyperLink>
	                        </ItemTemplate>
	                    </asp:TemplateField>
		                <asp:BoundField DataField="FileName" HeaderText="File" />
		                <asp:BoundField DataField="CreationDate" HeaderText="Date" />
		                <asp:BoundField DataField="NewBatch" HeaderText="New" />
		                <asp:BoundField DataField="PendingImport" HeaderText="Pending<br>Import" HtmlEncode="false" />
		                <asp:BoundField DataField="Complete" HeaderText="Complete" />
		                <asp:BoundField DataField="Error" HeaderText="Error" />
	                </Columns>
	                <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />        
</asp:Content>
