<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="true" Codebehind="ReportItemPagination.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportItemPagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Pagination Report</span><hr />
	<p>
    View Items in Pagination Status:&nbsp;<asp:DropDownList ID="ddlStatusView" runat="server"></asp:DropDownList>&nbsp;&nbsp;<a class="small" href="#" title="About" onclick="window.open('PaginationStatusAbout.html', 'About', 'resizeable=0,scrollbars=1,height=300,width=300,status=0,toolbar=0,menubar=0,location=0');">Status descriptions</a><br />
    With a Pagination Status Date Between:&nbsp;<asp:TextBox ID="txtStartDate" Width="75px" runat="server"></asp:TextBox> and <asp:TextBox ID="txtEndDate" Width="75px" runat="server"></asp:TextBox>&nbsp;
    <asp:Button ID="btnView" runat="server" Text="Go" OnClick="btnView_Click" />
    </p>
    <asp:Literal ID="litDisplayed" runat="server"></asp:Literal>
    <b><a id="lnkDownloadResults" runat="server" title="Download Results" style="float:right" visible="false" href="#">Download Results</a></b>

    <div id="listDiv" style="height:100%; overflow:auto;">
        <table id="list"></table>
        <div id="pager"></div>
    </div>
    <span runat="server" id="spanStatusChange">
    Change Pagination Status of Selected Items to:&nbsp;<asp:DropDownList ID="ddlStatusChange" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="btnChange" runat="server" Text="Update" OnClientClick="return getSelected();" OnClick="btnChange_Click" />
    <asp:HiddenField ID="hidSelected" runat="server" ClientIDMode="Static" /></span>
    <b><a id="lnkDownloadAll" title="DownloadAll" style="float:right" href="Services/ItemPaginationDownloadService.ashx?psid=0&sdate=1/1/1980&edate=12/31/2099">Click here to download pagination data for ALL items</a></b>
    <br />
    <asp:HiddenField ID="hidStatus" runat="server" />
    <asp:HiddenField ID="hidStartDate" runat="server" />
    <asp:HiddenField ID="hidEndDate" runat="server" />
    <asp:Literal ID="litUpdateResult" runat="server"></asp:Literal>
    <style>
        .ui-jqgrid .ui-jqgrid-htable th div {
            height:auto;
            overflow:hidden;
            padding-right:4px;
            padding-top:2px;
            position:relative;
            vertical-align:text-top;
            white-space:normal !important;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#list").jqGrid({
                url: 'services/itemservice.ashx?op=ItemPaginationReport&psid=<%=statusId%>&sdate=<%=startDate%>&edate=<%=endDate%>', // tells where to get the data
                datatype: 'xml',    // format of the data (xml,json,jsonp,array,xmlstring,jsonstring,script,function)
                mtype: 'GET',   // specify if AJAX call is a GET or POST
                colNames: ['Title ID', 'Title', 'Bib Level', 'Item ID', 'Volume', 'Year', 'Item Status', 'Scanning Date', 'Holding Institution', 'Pagination Status', 'Pagination Status Date', 'Pagination User', '# Pages'],    // column names
                colModel: [
                  { name: 'TitleID', index: 'PrimaryTitleID', width: '40px' },
                  { name: 'Title', index: 'SortTitle', width: '100px' },
                  { name: 'Bibliographic Level', index: 'BibliographicLevel', width: '50px' },
                  { name: 'ItemID', index: 'ItemID', width: '40px' },
                  { name: 'Volume', index: 'Volume', width: '50px' },
                  { name: 'Year', index: 'Year', width: '40px' },
                  { name: 'Item Status', index: 'ItemStatusName', width: '55px' },
                  { name: 'Scanning Date', index: 'ScanningDate', width: '50px' },
                  { name: 'Holding Institution', index: 'InstitutionName', width: '100px' },
                  { name: 'Pagination Status', index: 'PaginationStatusName', sortable: false, width: '55px' },
                  { name: 'Pagination Status Date', index: 'PaginationStatusDate', width: '80px' },
                  { name: 'Pagination User', index: 'PaginationUserName', width: '80px' },
                  { name: '# of Pages', index: 'NumPages', sortable: false, width: '30px' },
                ],  // model of the columns to display
                pager: '#pager',    // show a pager bar for record navigation
                rowNum: 200,    // rows in grid
                rowList: [100, 200, 300],  // options in select box for changing number of rows displayed
                sortname: 'ItemID',  // sort column
                sortorder: 'desc',  // sort direction
                viewrecords: true,  // display total number of records
                caption: '',    // grid caption; blank to hide
                loadui: 'block',    // block actions on the grid while data is being retrieved
                multiselect: true   // allow multiple row selection (adds checkbox at left)
            });
        });

        // Resize the grid to fill the space
        jQuery(window).load(function () { resizeGrid(); });
        jQuery(window).resize(function () { resizeGrid(); });

        function resizeGrid() {
            jQuery("#list").setGridWidth(jQuery(window).width() - 40);
            jQuery("#list").setGridHeight(jQuery(window).height() - 315);
        }

        function getSelected() {
            if (confirm("Are you sure you want to change the pagination status of the selected items?")) {

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
