<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContributorPage.aspx.cs" Inherits="MOBOT.BHL.Web2.ContributorPage" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<%@ Register TagPrefix="bb" TagName="BookBrowseControl" Src="~/Controls/BookBrowseControl.ascx" %>
<%@ Register TagPrefix="sb" TagName="SectionBrowseControl" Src="~/Controls/SectionBrowseControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<nav id="page-title-contribs">
    <div class="column-wrap">
        <div><asp:Literal ID="litHeader" runat="server" Text='Contributions from "{0}"'></asp:Literal></div>
    </div>
</nav>
<nav id="sub-nav">
    <div class="column-wrap">
      
        <div id="linkbar" class="lettersandnumbers">
            <ul>
                <li class="first-child"><a class="<%= SetClass("a") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/a">A</a></li>
                <li><a class="<%= SetClass("b") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/b">B</a></li>
                <li><a class="<%= SetClass("c") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/c">C</a></li>
                <li><a class="<%= SetClass("d") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/d">D</a></li>
                <li><a class="<%= SetClass("e") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/e">E</a></li>
                <li><a class="<%= SetClass("f") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/f">F</a></li>
                <li><a class="<%= SetClass("g") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/g">G</a></li>
                <li><a class="<%= SetClass("h") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/h">H</a></li>
                <li><a class="<%= SetClass("i") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/i">I</a></li>
                <li><a class="<%= SetClass("j") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/j">J</a></li>
                <li><a class="<%= SetClass("k") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/k">K</a></li>
                <li><a class="<%= SetClass("l") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/l">L</a></li>
                <li><a class="<%= SetClass("m") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/m">M</a></li>
                <li><a class="<%= SetClass("n") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/n">N</a></li>
                <li><a class="<%= SetClass("o") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/o">O</a></li>
                <li><a class="<%= SetClass("p") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/p">P</a></li>
                <li><a class="<%= SetClass("q") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/q">Q</a></li>
                <li><a class="<%= SetClass("r") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/r">R</a></li>
                <li><a class="<%= SetClass("s") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/s">S</a></li>
                <li><a class="<%= SetClass("t") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/t">T</a></li>
                <li><a class="<%= SetClass("u") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/u">U</a></li>
                <li><a class="<%= SetClass("v") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/v">V</a></li>
                <li><a class="<%= SetClass("w") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/w">W</a></li>
                <li><a class="<%= SetClass("x") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/x">X</a></li>
                <li><a class="<%= SetClass("y") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/y">Y</a></li>
                <li><a class="<%= SetClass("z") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/z">Z</a></li>
                <li><a class="<%= SetClass("0") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/0">#</a></li>
                <li><a class="<%= SetClass("All") %>" href="/browse/contributor/<%= contributor.InstitutionCode %>/all">All</a></li>
            </ul>
        </div>
    </div>
</nav>
<nav id="page-title" style="height:45px;line-height:45px;" class="no-js-hide">
    <div class="column-wrap">
        <div id="sub-sub-nav-tabs">
            <div id="linkbar">
            <h3><asp:Literal ID="ltlContributorHeader" runat="server" EnableViewState="false" /></h3>
            <ul>
                <li runat="server" id="spanTitleSummary" class="titles">
                    <a href="#/titles" title="Books"><% if (Start.ToUpper() == "ALL") { %>All <span class="highlight-tab">Books/Journals</span><% } else { %><span class="highlight-tab">Books/Journals</span> beginning with <%: displayStart %><% } %> (<%: count%>)</a>
                </li>
                <li runat="server" id="spanSectionSummary" class="sections">
                    <a href="#/sections" title="Parts"><% if (Start.ToUpper() == "ALL") { %>All <span class="highlight-tab">Chapters/Articles/Treatments</span><% } else { %><span class="highlight-tab">Chapters/Articles/Treatments</span> beginning with <%: displayStart %><% } %> (<%: segmentcount%>)</a>
                </li>
                </ul>
            </div>
        </div>
    </div>
</nav>
<div id="sortbar">
<div class="column-wrap">
<span>Sort By:</span> 
    <ul>
    <li class="<%= SetSortClass("title") %>"><a href="/browse/contributor/<%= contributor.InstitutionCode %>/<%: Start %>/title">Title</a></li>
    <li class="<%= SetSortClass("author") %>"><a href="/browse/contributor/<%= contributor.InstitutionCode %>/<%: Start %>/author">Author</a></li>
    <li class="<%= SetSortClass("year") %>"><a href="/browse/contributor/<%= contributor.InstitutionCode %>/<%: Start %>/year">Year</a></li>
    </ul>
    <div class="floatclear"></div>
</div>
</div>
<div id="content" class="column-wrap clearfix">
    <section>
        <asp:Literal runat="server" ID="ltlContributorStats" Visible="false" Text="<p>This contributor has added <span style='font-weight:bolder'>{0}</span> volumes from <span style='font-weight:bolder'>{1}</span> titles, containing <span style='font-weight:bolder'>{2}</span> pages.<br/>This contributor has added <span style='font-weight:bolder'>{3}</span> chapters/articles/treatments.</p>"></asp:Literal>
        <div id="sub-sections" class="column-wrap clearfix">
	        <asp:Panel ID="titles" Visible="true" runat="server" ClientIDMode="Static">	
                <div id="titlecontent" class="content">
                    <section>
                        <bb:BookBrowseControl ID="BookBrowse" runat="server" />
                    </section>
                </div>
            </asp:Panel>
            <asp:Panel ID="sections" ClientIDMode="Static" Visible="true" runat="server" CSSclass="floatclear">
                <div id="segmentcontent" class="content">
                    <section>
                        <sb:SectionBrowseControl ID="SectionBrowse" runat="server" />
                    </section>
                </div>
            </asp:Panel>
        </div>
    </section>
    <aside>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>
</asp:Content>
<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/jquery.history.min.js"></script>
<script src="/js/libs/jquery.hoverintent.min.js"></script>
<script src="/js/libs/jquery.text-overflow.min.js"></script>
<script type="text/javascript">
  //<![CDATA[
    $(document).ready(function () {
        var subSections = $('#sub-sections').children().hide();
        $('#searchaside').hide();

        // Navigate to the default sub-section if no hash
        if (!location.hash) {
            if (<%: count%> > 0) {
                window.location.replace('#/titles');
            }
            else if (<%: segmentcount%> > 0) {
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
