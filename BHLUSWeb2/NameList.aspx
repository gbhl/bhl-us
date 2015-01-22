<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NameList.aspx.cs" Inherits="MOBOT.BHL.Web2.NameList" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">    
    <h1 class="column-wrap">
        <span class="links">
            <asp:Literal runat="server" ID="litEOLLink"></asp:Literal>&nbsp;&nbsp;
            <a class="button" href="/namedetail/<%= NameParam %>">View Name Sources</a>
            <a class="button" href="/namelistdownload/?type=c&name=<%= NameParam %>">Download CSV</a>
            <a class="button" href="/namelistdownload/?type=b&name=<%= NameParam %>">Download BibTeX</a>
            <a class="button" href="/namelistdownload/?type=e&name=<%= NameParam %>">Download EndNote</a>
        </span>
        Bibliography for <%= TitleLink  %> by Page 
    </h1>
</div>

<div id="content" class="column-wrap clearfix">
    <table id="name-list"></table>
    <div id="pager"></div>
</div>
        <div id="imagePopup" style="height:600px;width:600px;text-align:center;overflow:hidden">
            <a id="imagePopupClose" class="imagePopupClose" title="Close">Close Image</a>
            <div style="height:580px;width:600px;text-align:center;overflow:auto">
            <img id="pageImage" style="display:none;" alt="Page Image" />
            </div>
        </div>
        <div id="imagePopupBackground"></div>
</asp:Content>

<asp:Content ID="PageHeaderIncludes" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
<link rel="stylesheet" type="text/css" href="/css/jqgridtheme/jquery-ui-1.8.10.custom.css" />
<link rel="stylesheet" type="text/css" href="/css/jqgridtheme/ui.jqgrid.css" />
</asp:Content>

<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/grid.locale-en.js" type="text/javascript"></script>
<script src="/js/libs/jquery.jqGrid.min.js" type="text/javascript"></script>
<style>
.ui-jqgrid tr.ui-row-ltr td {  border-left: 1px solid #b6c1cc;  }
</style>
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {
        $("#name-list").jqGrid({
            url: '/namelist/?name=<%= NameParam %>', // tells where to get the data
            datatype: 'xml',    // format of the data (xml,json,jsonp,array,xmlstring,jsonstring,script,function)
            mtype: 'GET',   // specify if AJAX call is a GET or POST
            colNames: ['Title', 'Authors', 'Volume', 'Date', 'Page #', 'View'],    // column names
            colModel: [
              { name: 'ShortTitle', index: 'ShortTitle' },
              { name: 'Authors', index: 'Authors', sortable: false },
              { name: 'Volume', index: 'Volume', sortable: false, width: 120 },
              { name: 'Date', index: 'Date', width: 30 },
              { name: 'IndicatedPages', index: 'IndicatedPages', sortable: false, width: 60 },
              { name: 'ShowPreview', index: 'ShowPreview', align: 'center', sortable: false, width: 20 }
            ],  // model of the columns to display
            pager: '#pager',    // show a pager bar for record navigation
            rowNum: 200,    // rows in grid
            rowList: [100, 200, 300],  // options in select box for changing number of rows displayed
            sortname: 'ShortTitle',  // sort column
            sortorder: 'asc',  // sort direction
            viewrecords: true,  // display total number of records
            caption: '',    // grid caption; blank to hide
            loadui: 'block',    // block actions on the grid while data is being retrieved
            autowidth: true,
            height: '600',
            altRows: true,
            altclass: 'alt',
            hoverrows: false
        });
    });

    $(document).ready(function () {
        $("#imagePopupClose").click(function () { disablePopup(); });
        $(document).keypress(function (e) { if (e.keyCode == 27 && popupStatus == 1) { disablePopup(); } });
    });

    function showPreview(pageID) {
        $("#pageImage").attr("src", "/pagethumb/" + pageID + ",500,800").bind("load", function () { $("#pageImage").show(); });
        centerPopup("#imagePopup");
        loadPopup("#imagePopup");
        return false;
    }

    var popupStatus = 0;
    function loadPopup(popup) { if (popupStatus == 0) { $("#imagePopupBackground").fadeIn("fast"); $(popup).fadeIn("fast"); popupStatus = 1; } }
    function disablePopup() {
        if (popupStatus == 1) {
            $("#imagePopupBackground").fadeOut("fast");
            $("#imagePopup").fadeOut("fast");
            $("#imageZoom").fadeOut("fast");
            popupStatus = 0;
            $("#pageImage").hide();
        }
    }
    function centerPopup(popup) {
        var windowWidth = document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;
        var popupHeight = $(popup).height();
        var popupWidth = $(popup).width();

        $(popup).css({ "position": "absolute", "top": windowHeight / 2 - popupHeight / 2, "left": windowWidth / 2 - popupWidth / 2 });
        $("#imagePopupBackground").css({ "height": windowHeight });
    }
//]]>
</script>
</asp:Content>
