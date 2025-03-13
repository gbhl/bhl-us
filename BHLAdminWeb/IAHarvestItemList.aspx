<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="IAHarvestItemList.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.IAHarvestItemList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
	<a href="/IAHarvestDashboard.aspx">&lt; Return to IA Harvest Dashboard</a><br />
    <br />
    <span class="pageHeader">IA Harvest Item List</span><hr />
    <p>This page is used to manage the import status of new items being brought into BHL from Internet Archive.  Note that BHL dates in the table below are from the BHL Import database.</p>
	<p>
        <table>
            <tr>
                <td><b>Show Items in Status:</b></td>
                <td style="padding-right:50px"><asp:DropDownList ID="ddlStatusView" runat="server"></asp:DropDownList>&nbsp;&nbsp;<a class="small" href="#" title="About" onclick="window.open('IAHarvestItemStatusAbout.aspx', 'About', 'resizeable=0,scrollbars=1,height=500,width=500,status=0,toolbar=0,menubar=0,location=0');">Status descriptions</a></td>
                <td><b>Show Items Where IA Identifier Starts With:</b></td>
                <td><asp:TextBox ID="txtIAIdentifier" runat="server" placeholder="(Optional)"></asp:TextBox></td>
            </tr>
        </table>
    </p>
    <p>
        <asp:Button ID="btnView" runat="server" Text="Show Items" OnClick="btnView_Click" />&nbsp;
        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" />&nbsp;
        <span style="font-size:10px">Note: Downloads include additional metadata for each item.</span>
    </p>
    <asp:Literal ID="litDisplayed" runat="server"></asp:Literal>
    <div id="listDiv" style="height:100%; overflow:auto;">
        <table id="list"></table>
        <div id="pager"></div>
    </div>
    Change Status of Selected Items to:&nbsp;<asp:DropDownList ID="ddlStatusChange" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="btnChange" runat="server" Text="Update" OnClientClick="return getSelected();" OnClick="btnChange_Click" />
    <asp:HiddenField ID="hidSelected" runat="server" ClientIDMode="Static" /><br />
    <asp:Literal ID="litUpdateResult" runat="server"></asp:Literal>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#list").jqGrid({
                url: 'services/dataharvestservice.ashx?id=<%=statusId%>&iaid=<%=iaId%>', // tells where to get the data
                datatype: 'xml',    // format of the data (xml,json,jsonp,array,xmlstring,jsonstring,script,function)
                mtype: 'GET',   // specify if AJAX call is a GET or POST
                colNames: ['ID', 'Status (IA)', 'Status (BHL)', 'HoldingInstitution', 'Created (IA)', 'Scanned (IA)', 'Last Modified (IA)', 'Created (BHL)', 'Last Download (BHL)', 'Production (BHL)', 'Last Modified (BHL)', 'Created By', 'Last Modified By'],    // column names
                colModel: [
                  { name: 'IAIdentifier', index: 'IAIdentifier', width: '200' },
                  { name: 'ExternalStatus', index: 'ExternalStatus', width: '100' },
                  { name: 'Status', index: 'Status' },
                  { name: 'HoldingInstitution', index: 'HoldingInstitution', width: '300' },
                  { name: 'IAAddedDate', index: 'IAAddedDate' },
                  { name: 'ScanDate', index: 'ScanDate' },
                  { name: 'IADateStamp', index: 'IADateStamp' },
                  { name: 'CreatedDate', index: 'CreatedDate' },
                  { name: 'LastXMLDataHarvestDate', index: 'LastXMLDataHarvestDate' },
                  { name: 'LastProductionDate', index: 'LastProductionDate' },
                  { name: 'LastModifiedDate', index: 'LastModifiedDate' },
                  { name: 'CreatedUser', index: 'CreatedUser' },
                  { name: 'LastModifiedUser', index: 'LastModifiedUser' },
                ],  // model of the columns to display
                pager: '#pager',    // show a pager bar for record navigation
                rowNum: 200,    // rows in grid
                rowList: [100, 200, 300],  // options in select box for changing number of rows displayed
                sortname: 'IAIdentifier',  // sort column
                sortorder: 'asc',  // sort direction
                viewrecords: true,  // display total number of records
                caption: '',    // grid caption; blank to hide
                loadui: 'block',    // block actions on the grid while data is being retrieved
                multiselect: true   // allow multiple row selection (adds checkbox at left)
            });
        });

        // Resize the grid to fill the space
        jQuery(window).on("load", function () { resizeGrid(); });
        jQuery(window).on("resize", function () { resizeGrid(); });

        function resizeGrid() {
            jQuery("#list").setGridWidth(jQuery(window).width() - 40);
            jQuery("#list").setGridHeight(jQuery(window).height() - 350);
        }

        function getSelected() {
            if (confirm("Are you sure you want to change the status of the selected items?")) {

                jQuery("#hidSelected").attr("value", "");
                var selected = jQuery(".cbox:checked");
                for (x = 0; x < selected.length; x++) {
                    var hidValue = jQuery("#hidSelected").attr("value");
                    if (hidValue.length > 0) hidValue += "|";
                    jQuery("#hidSelected").attr("value", hidValue += selected[x].id);
                }

                return true;
            }
            else {
                return false
            }
        }
    </script>
</asp:Content>
