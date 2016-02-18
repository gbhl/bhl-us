<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.Stats"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
    <a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Expanded Stats</span><hr />

    <br />Contributor:
    <asp:DropDownList ID="ddlInstitutions" runat="server">
    <asp:ListItem Value="" Text="(All)"></asp:ListItem>
    </asp:DropDownList><br /><br />
    <asp:CheckBox ID="chkShowMonthly" runat="server" Text="Show Statistics By Month" />
    <br /><br />
    <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />
    <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" />

	<table cellpadding="7" cellspacing="7"><tr><td valign="top">
        <span class="tableHeader">Titles In Production</span><br />
        <div style="overflow:auto;height:225px;width:510px;border-style:solid;border-color:Black;border-width:1px">
            <asp:GridView ID="gvProductionTitles" runat="server" 
                AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" 
                GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true"
                OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="StatValue" HeaderText="Titles" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
            <asp:GridView ID="gvProductionTitlesByMonth" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField DataField="StatValue" HeaderText="Titles" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
        </div>
    </td>
    <td valign="top">
        <span class="tableHeader">Segments In Production</span><br />
        <div style="overflow:auto;height:225px;width:510px;border-style:solid;border-color:Black;border-width:1px">
            <asp:GridView ID="gvProductionSegments" runat="server" 
                AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" 
                GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true"
                OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="StatValue" HeaderText="Titles" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
            <asp:GridView ID="gvProductionSegmentsByMonth" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField DataField="StatValue" HeaderText="Segments" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
        </div>
    </td></tr><tr><td valign="top">
        <span class="tableHeader">Items In Production</span><br />
        <div style="overflow:auto;height:225px;width:510px;border-style:solid;border-color:Black;border-width:1px">
            <asp:GridView ID="gvProductionItems" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none"  ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="StatValue" HeaderText="Items" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
            <asp:GridView ID="gvProductionItemsByMonth" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField DataField="StatValue" HeaderText="Items" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
        </div>
    </td><td valign="top">
        <span class="tableHeader">Items Scanned</span><br />
        <div style="overflow:auto;height:225px;width:510px;border-style:solid;border-color:Black;border-width:1px">
            <asp:GridView ID="gvScannedItems" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="StatValue" HeaderText="Items" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
            <asp:GridView ID="gvScannedItemsByMonth" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField DataField="StatValue" HeaderText="Items" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
        </div>
    </td></tr><tr><td valign="top">
        <span class="tableHeader">Pages In Production</span><br />
        <div style="overflow:auto;height:225px;width:510px;border-style:solid;border-color:Black;border-width:1px">
            <asp:GridView ID="gvProductionPages" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="StatValue" HeaderText="Pages" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
            <asp:GridView ID="gvProductionPagesByMonth" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField DataField="StatValue" HeaderText="Pages" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
        </div>
    </td><td valign="top">
        <span class="tableHeader">Names In Production</span><br />
        <div style="overflow:auto;height:225px;width:510px;border-style:solid;border-color:Black;border-width:1px">
            <asp:GridView ID="gvProductionNames" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="StatValue" HeaderText="Names" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
            <asp:GridView ID="gvProductionNamesByMonth" runat="server" AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="InstitutionName" HeaderText="Contributor" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="left" />
                <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField DataField="StatValue" HeaderText="Names" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right"  FooterStyle-HorizontalAlign="Right" />
            </Columns>
            </asp:GridView>
        </div>
    </td></tr></table>
</asp:Content>
