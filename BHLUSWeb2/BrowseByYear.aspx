<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseByYear.aspx.cs" Inherits="MOBOT.BHL.Web2.BrowseByYear" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="BookBrowseControl" Src="~/Controls/BookBrowseControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="SectionBrowseControl" Src="~/Controls/SectionBrowseControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="browseContainerDiv">
    <nav id="sub-nav">
        <div class="column-wrap">
            <div id="linkbar">
                <ul>
                    <li class="first-child"><a class="<%= SetClass("browse/year") %>" href="/browse/year">1450-1580</a></li>
                    <li><a class="<%= SetClass("1581/1699") %>" href="/browse/year/1581/1699">1581-1699</a></li>
                    <li><a class="<%= SetClass("1700/1799") %>" href="/browse/year/1700/1799">1700-1799</a></li>
                    <li><a class="<%= SetClass("1800/1849") %>" href="/browse/year/1800/1849">1800-1849</a></li>
                    <li><a class="<%= SetClass("1850/1899") %>" href="/browse/year/1850/1899">1850-1899</a></li>
                    <li><a class="<%= SetClass("1900/1924") %>" href="/browse/year/1900/1924">1900-1924</a></li>
                    <li><a class="<%= SetClass("1925/1949") %>" href="/browse/year/1925/1949">1925-1949</a></li>
                    <li><a class="<%= SetClass("1950/1974") %>" href="/browse/year/1950/1974">1950-1974</a></li>
                    <li><a class="<%= SetClass("1975/1999") %>" href="/browse/year/1975/1999">1975-1999</a></li>
                    <li><a class="<%= SetClass("2000/" + DateTime.Now.Year) %>" href="/browse/year/2000/<%= DateTime.Now.Year %>">2000-<%= DateTime.Now.Year %></a></li>
                </ul>
            </div>
        </div>
    </nav>
    <div id="page-title">    
        <h1 class="column-wrap">
        <div id="sub-sub-nav-tabs">
            <div id="linkbar">
                <ul>
                   <li class="titles"><a href="#/titles" title="Books"><span class="highlight-tab">Titles</span> between <%= StartDate %> and <%= EndDate %> (<%=Count%>)</a></li>
                   <li class="sections"><a href="#/sections" title="Parts"><span class="highlight-tab">Chapters/Articles/Treatments</span> between <%= StartDate %> and <%= EndDate %> (<%=SegmentCount%>)</a></li>
                </ul>
            </div>
        </div>
        </h1>
    </div>
    <div id="sortbar">
    <div class="column-wrap">
    <span>Sort By:</span> 
        <ul>
        <li class="<%= SetSortClass("title") %>"><a href="<%=sortBaseURL %>/title">Title</a></li>
        <li class="<%= SetSortClass("author") %>"><a href="<%=sortBaseURL %>/author">Author</a></li>
        <li class="<%= SetSortClass("year") %>"><a href="<%=sortBaseURL %>/year">Year</a></li>
        </ul>
        <div class="floatclear"></div>
    </div>
    </div>
    <div id="sub-sections" class="column-wrap clearfix">
	    <asp:Panel ID="titles" Visible="true" runat="server" ClientIDMode="Static">	
        <div id="titlecontent" class="content">
            <section>
                <uc:BookBrowseControl ID="BookBrowse" runat="server" />
            </section>
        </div>
        </asp:Panel>
	    <asp:Panel ID="sections" Visible="true" runat="server" ClientIDMode="Static">	
        <div id="segmentcontent" class="content">
            <section>
                <uc:SectionBrowseControl ID="SectionBrowse" runat="server" />
            </section>
        </div>
        </asp:Panel>
    </div>
    <aside id="searchaside">
        <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
        <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>
</asp:Content>

<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/jquery.history.min.js"></script>
<script src="/js/libs/jquery.hoverintent.min.js"></script>
<script src="/js/libs/jquery.text-overflow.min.js"></script>
<script src="/js/libs/jquery-ui-1.8.11.custom.min.js" type="text/javascript"></script>
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {
        var subSections = $('#sub-sections').children(); //.hide();
        $('#searchaside').hide();

        // Navigate to the default sub-section if no hash
        if (!location.hash) {
            if (<%: Count%> > 0) {
                window.location.replace('#/titles');
            }
            else if (<%: SegmentCount%> > 0) {
                window.location.replace('#/sections');
            }
        }

        $.History.bind(function (state) {
            if (!$('#sub-sections').is(':visible')) {
                $('#sub-sections').show();
            }
            var stateName = state.replace(/[^a-zA-Z0-9\s]/gi, '');
            var subSection = $('#' + stateName);
            subSections.hide();

            // Highlight selected link
            $('#sub-sub-nav-tabs a').removeClass('active');
            $('#sub-sub-nav-tabs .' + stateName + ' a').addClass('active').blur();

            // Set the hash on the main nav links
            $('#sub-nav a').each(function () {
                $(this).attr('href', $(this).attr('href').replace('#/sections', '').replace('#/titles', '') + '#/' + stateName);
            });

            // Set the hash on the sort links
            $('#sortbar a').each(function () {
                $(this).attr('href', $(this).attr('href').replace('#/sections', '').replace('#/titles', '') + '#/' + stateName);
            });

            // If no default sub-section found then head on to the default otherwise show selected sub-section
            if (!subSection.length) {
                $.History.go('/titles');
                return false;
            } else {
                subSection.show();
            }
            $('#searchaside').show();
        });
    });
//]]>
</script>
</asp:Content>
