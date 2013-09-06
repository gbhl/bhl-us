<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TitleList.aspx.cs" Inherits="MOBOT.BHL.Web2.TitleList" %>
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
      
            <div id="linkbar" class="letters">
                <ul>
                    <li class="first-child"><a class="<%= SetClass("a") %>" href="/browse/titles/a">A</a></li>
                    <li><a class="<%= SetClass("b") %>" href="/browse/titles/b">B</a></li>
                    <li><a class="<%= SetClass("c") %>" href="/browse/titles/c">C</a></li>
                    <li><a class="<%= SetClass("d") %>" href="/browse/titles/d">D</a></li>
                    <li><a class="<%= SetClass("e") %>" href="/browse/titles/e">E</a></li>
                    <li><a class="<%= SetClass("f") %>" href="/browse/titles/f">F</a></li>
                    <li><a class="<%= SetClass("g") %>" href="/browse/titles/g">G</a></li>
                    <li><a class="<%= SetClass("h") %>" href="/browse/titles/h">H</a></li>
                    <li><a class="<%= SetClass("i") %>" href="/browse/titles/i">I</a></li>
                    <li><a class="<%= SetClass("j") %>" href="/browse/titles/j">J</a></li>
                    <li><a class="<%= SetClass("k") %>" href="/browse/titles/k">K</a></li>
                    <li><a class="<%= SetClass("l") %>" href="/browse/titles/l">L</a></li>
                    <li><a class="<%= SetClass("m") %>" href="/browse/titles/m">M</a></li>
                    <li><a class="<%= SetClass("n") %>" href="/browse/titles/n">N</a></li>
                    <li><a class="<%= SetClass("o") %>" href="/browse/titles/o">O</a></li>
                    <li><a class="<%= SetClass("p") %>" href="/browse/titles/p">P</a></li>
                    <li><a class="<%= SetClass("q") %>" href="/browse/titles/q">Q</a></li>
                    <li><a class="<%= SetClass("r") %>" href="/browse/titles/r">R</a></li>
                    <li><a class="<%= SetClass("s") %>" href="/browse/titles/s">S</a></li>
                    <li><a class="<%= SetClass("t") %>" href="/browse/titles/t">T</a></li>
                    <li><a class="<%= SetClass("u") %>" href="/browse/titles/u">U</a></li>
                    <li><a class="<%= SetClass("v") %>" href="/browse/titles/v">V</a></li>
                    <li><a class="<%= SetClass("w") %>" href="/browse/titles/w">W</a></li>
                    <li><a class="<%= SetClass("x") %>" href="/browse/titles/x">X</a></li>
                    <li><a class="<%= SetClass("y") %>" href="/browse/titles/y">Y</a></li>
                    <li><a class="<%= SetClass("z") %>" href="/browse/titles/z">Z</a></li>
                    <li><a class="<%= SetClass("0") %>" href="/browse/titles/0">#</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <div id="page-title">   
        <h1 class="column-wrap">
        <div id="sub-sub-nav-tabs">
            <div id="linkbar">
                <ul>
                    <li class="titles"><a href="#/titles" title="Books"><% if (string.IsNullOrEmpty(Start)) { %>All <span class="highlight-tab">Titles</span> (<%= Count%>)<% } else { %><span class="highlight-tab">Titles</span> beginning with <%= displayStart %><% } %> (<%= Count%>)</a></li>
                    <li class="sections"><a href="#/sections" title="Parts"><% if (string.IsNullOrEmpty(Start)) { %>All <span class="highlight-tab">Articles/Chapters/Treatments</span> (<%= SegmentCount%>)<% } else { %><span class="highlight-tab">Articles/Chapters/Treatments</span> beginning with <%= displayStart %><% } %> (<%= SegmentCount%>)</a></li>
                </ul>
            </div>
        </div>
        </h1>
    </div>
    <div id="sortbar">
        <div class="column-wrap">
            <span>Sort By:</span> 
            <ul>
            <li class="<%= SetSortClass("title") %>"><a href="/browse/titles/<%= Start %>/title">Title</a></li>
            <li class="<%= SetSortClass("author") %>"><a href="/browse/titles/<%= Start %>/author">Author</a></li>
            <li class="<%= SetSortClass("year") %>"><a href="/browse/titles/<%= Start %>/year">Year</a></li>
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
