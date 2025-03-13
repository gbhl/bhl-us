<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="BioStorHarvestItemList.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.BioStorHarvestItemList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
    <span class="pageHeader">BioStor Harvest Item List</span><hr />
	<p>
    View Items in Status:&nbsp;<asp:DropDownList ID="ddlStatusView" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="btnView" runat="server" Text="Go" OnClick="btnView_Click" />&nbsp;&nbsp;<a class="small" href="#" title="About" onclick="window.open('BioStorHarvestItemStatusAbout.aspx', 'About', 'resizeable=0,scrollbars=1,height=500,width=500,status=0,toolbar=0,menubar=0,location=0');">Status descriptions</a>
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
                url: 'services/dataharvestservice.ashx?id=<%=statusId%>&type=bsstatus', // tells where to get the data
                datatype: 'xml',    // format of the data (xml,json,jsonp,array,xmlstring,jsonstring,script,function)
                mtype: 'GET',   // specify if AJAX call is a GET or POST
                colNames: ['BHL Item ID', 'Title', 'Volume', 'Total Segments', 'Published Segments', 'Skipped Segments', 'Creation Date'],    // column names
                colModel: [
                  { name: 'BHLItemID', index: 'BHLItemID', sortable: false, width: '50px' },
                  { name: 'Title', index: 'Title', width: '300px' },
                  { name: 'Volume', index: 'Volume', sortable: false, width: '100px' },
                  { name: 'TotalSegments', index: 'TotalSegments', sortable: false, width: '60px' },
                  { name: 'PublishedSegments', index: 'PublishedSegments', sortable: false, width: '60px' },
                  { name: 'SkippedSegments', index: 'SkippedSegments', sortable: false, width: '60px' },
                  { name: 'CreationDate', index: 'CreationDate', width: '80px' },
                ],  // model of the columns to display
                pager: '#pager',    // show a pager bar for record navigation
                rowNum: 200,    // rows in grid
                rowList: [100, 200, 300],  // options in select box for changing number of rows displayed
                sortname: 'CreationDate',  // sort column
                sortorder: 'desc',  // sort direction
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
            jQuery("#list").setGridHeight(jQuery(window).height() - 315);
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
