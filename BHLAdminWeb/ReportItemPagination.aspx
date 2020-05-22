<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="true" Codebehind="ReportItemPagination.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportItemPagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Pagination Report</span><hr />
	<div>
    <div style="margin:3px;">View Items Held by <asp:DropDownList ID="listInstitutions" runat="server" DataTextField="InstitutionName" DataValueField="InstitutionCode"/></div>
    <div style="margin:3px;">In Pagination Status:&nbsp;
        <asp:CheckBox ID="chkPStatNew" ClientIDMode="Static" runat="server" Text="New" />&nbsp;
        <asp:CheckBox ID="chkPStatIncomplete" ClientIDMode="Static" runat="server" Text="Incomplete" />&nbsp;
        <asp:CheckBox ID="chkPStatInProgress" ClientIDMode="Static" runat="server" Text="In Progress" Checked="true" />&nbsp;
        <asp:CheckBox ID="chkPStatComplete" ClientIDMode="Static" runat="server" Text="Complete" />&nbsp;
        <a class="small" href="#" title="About" onclick="window.open('PaginationStatusAbout.html', 'About', 'resizeable=0,scrollbars=1,height=300,width=300,status=0,toolbar=0,menubar=0,location=0');">Status descriptions</a>
    </div>
    <div style="margin:3px;">With a Pagination Status Date Between:&nbsp;<asp:TextBox ID="txtStartDate" Width="75px" runat="server"></asp:TextBox> and <asp:TextBox ID="txtEndDate" Width="75px" runat="server"></asp:TextBox></div>
    <div style="margin:3px;"><asp:Checkbox ID="chkPublished" ClientIDMode="Static" runat="server" Text="Limit the List to Published Items" Checked="true" />&nbsp;
    <asp:Button ID="btnView" runat="server" Text="Go" OnClick="btnView_Click" /></div>
    </div>
    <asp:Literal ID="litDisplayed" runat="server"></asp:Literal>
    <b><a id="lnkDownloadResults" runat="server" title="Download Results" style="float:right" visible="false" href="#">Download Results</a></b>

    <div id="listDiv" style="height:100%; overflow:auto;">
        <table id="list"></table>
        <div id="pager"></div>
    </div>
    <span runat="server" id="spanStatusChange">
    Change Pagination Status of Selected <b>In Progress</b> Items to:&nbsp;<asp:DropDownList ID="ddlStatusChange" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="btnChange" runat="server" Text="Update" OnClientClick="return getSelected();" OnClick="btnChange_Click" />
    <asp:HiddenField ID="hidSelected" runat="server" ClientIDMode="Static" /></span>
    <b><a id="lnkDownloadAll" title="DownloadAll" style="float:right" href="Services/ItemPaginationDownloadService.ashx?pub=0&inst=&psid=5|10|20|30&sdate=1/1/1980&edate=12/31/2099">Click here to download pagination data for ALL items</a></b>
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
                url: 'services/itemservice.ashx?op=ItemPaginationReport&pub=<%=publishedOnly%>&inst=<%=institutionCode%>&psid=<%=statusId%>&sdate=<%=startDate%>&edate=<%=endDate%>', // tells where to get the data
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
                multiselect: true,   // allow multiple row selection (adds checkbox at left)
                loadComplete: function () {
                    // we make all rows "protected", so that will be not selectable
                    var cbs = $("tr.jqgrow > td > input.cbox", jQuery("#grid")[0]);
                    cbs.attr("disabled", "disabled");
                    cbs.toggle();

                    /*
                    var lista = $(”#list”).getDataIDs();
                    for(i=0;i<lista.length;i++){
                        rowData=$(”#list″).getRowData(lista[i]);
                        rowData.fieldName1
                        rowData.fieldName2
                     }
                    */

                    /*
                    function loadCompleted() {
                        var $grid = jQuery("#jqGrid"), rows = $grid[0].rows;

                        for (var i = 1; i < rows.length; i++) {
                            var row = rows[i];
                            var id = row.cells[0].innerHTML;

                            $(row.cells[1]).html("<a href='#' onclick='deleteApp(" + id + "); return false;'>Delete</a>");
                        }
                    }
                     */

                    $("#jqgh_list_cb").toggle();    // turn off the "select all" checkbox
                },                
                rowattr: function (rowData, currentObj, rowid) {
                    if (rowData['Pagination Status'].trim() === "In Progress") {
                        var cbs = $("tr#" + rowid + ".jqgrow > td > input.cbox", jQuery("#grid")[0]);
                        console.log(cbs);
                        cbs.attr("disabled", "disabled");
                        cbs.toggle();
                    }
                    
                    //if (parseInt(item.ID, 10) % 3 === 0) {
                        //return { "class": "ui-state-disabled ui-jqgrid-disablePointerEvents" };
                    //}
                },
                beforeSelectRow: function (rowid, e) {
                    var cbsdis = $("tr#" + rowid + ".jqgrow > td > input.cbox:disabled", jQuery("#grid")[0]);
                    if (cbsdis.length === 0) {
                        return true;    // allow select the row
                    } else {
                        return false;   // not allow select the row
                    }
                },
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
