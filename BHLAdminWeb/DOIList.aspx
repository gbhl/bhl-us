<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DOIList.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.DOIList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
    <span class="pageHeader">DOI Status</span><hr />
	<div>
    <div style="margin:3px;">View DOIs Queued By:&nbsp;<asp:DropDownList ID="ddlQueuedBy" runat="server"></asp:DropDownList></div>
    <div style="margin:3px;">In BHL DOI Status:&nbsp;<asp:DropDownList ID="ddlStatusView" runat="server"></asp:DropDownList>&nbsp;&nbsp;<a class="small" href="#" title="About" onclick="window.open('DOIStatusAbout.aspx', 'About', 'resizeable=0,scrollbars=1,height=500,width=500,status=0,toolbar=0,menubar=0,location=0');">Status descriptions</a></div>
    <div style="margin:3px;">With Entity Type:&nbsp;<asp:DropDownList ID="ddlEntityType" runat="server"></asp:DropDownList>&nbsp;&nbsp;and Entity ID: <asp:TextBox ID="txtEntityID" Width="75px" runat="server" placeholder="(optional)"></asp:TextBox></div>
    <div style="margin:3px;">With a Queued Date Between:&nbsp;<asp:TextBox ID="txtStartDate" Width="75px" runat="server"></asp:TextBox> and <asp:TextBox ID="txtEndDate" Width="75px" runat="server"></asp:TextBox>&nbsp;
        <asp:Button ID="btnView" runat="server" Text="Go" OnClick="btnView_Click" /></div>
    </div>
    <div style="margin-top:8px;height:15px">
        <b><a id="lnkDownloadResults" runat="server" title="Download Results" style="float:right" visible="false" href="#">Download Results</a></b>
    </div>
    <div id="listDiv" style="height:100%; overflow:auto;">
        <table id="list"></table>
        <div id="pager"></div>
    </div>
    <p style="margin:0px"><asp:Literal ID="litDisplayed" runat="server"></asp:Literal></p>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#list").jqGrid({
                url: '/services/doiservice.ashx?uid=<%=userId%>&sid=<%=statusId%>&tid=<%=typeId%>&eid=<%=entityId%>&sdate=<%=startDate%>&edate=<%=endDate%>&dl=0', // tells where to get the data
                datatype: 'xml',    // format of the data (xml,json,jsonp,array,xmlstring,jsonstring,script,function)
                mtype: 'GET',   // specify if AJAX call is a GET or POST
                colNames: ['Queued By', 'Status', 'Action', 'Entity', 'Entity Detail', 'Container Title ID', 'DOI Batch ID', 'DOI', 'Message', 'Queued', 'Last Update'],    // column names
                colModel: [
                  { name: 'CreationUserName', index: 'CreationUserName', width: '80px' },
                  { name: 'StatusName', index: 'StatusName', width: '80px' },
                  { name: 'Action', index: 'Action', width: '60px' },
                  { name: 'Entity', index: 'Entity', width: '100px' },
                  { name: 'EntityDetail', index: 'EntityDetail' },
                  { name: 'ContainerTitleID', index: 'ContainerTitleID', width: '100px' },
                  { name: 'DOIBatchID', index: 'DOIBatchID', width: '160px' },
                  { name: 'DOI', index: 'DOI', width: '135px' },
                  { name: 'StatusMessage', index: 'StatusMessage' },
                  { name: 'CreationDate', index: 'CreationDate', width: '125px' },
                  { name: 'LastModifiedDate', index: 'LastModifiedDate', width: '125px' },
                ],  // model of the columns to display
                pager: '#pager',    // show a pager bar for record navigation
                rowNum: 200,    // rows in grid
                rowList: [100, 200, 300],  // options in select box for changing number of rows displayed
                sortname: 'LastModifiedDate',  // sort column
                sortorder: 'desc',  // sort direction
                viewrecords: true,  // display total number of records
                caption: '',    // grid caption; blank to hide
                loadui: 'block',    // block actions on the grid while data is being retrieved
                multiselect: false   // true = allow multiple row selection (adds checkbox at left)
            });
        });

        // Resize the grid to fill the space
        jQuery(window).load(function () { resizeGrid(); });
        jQuery(window).resize(function () { resizeGrid(); });

        function resizeGrid() {
            jQuery("#list").setGridWidth(jQuery(window).width() - 40);
            jQuery("#list").setGridHeight(jQuery(window).height() - 315);
        }
    </script>
</asp:Content>
