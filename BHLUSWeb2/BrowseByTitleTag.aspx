<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseByTitleTag.aspx.cs" Inherits="MOBOT.BHL.Web2.BrowseByTitleTag" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<%@ Register TagPrefix="bb" TagName="BookBrowseControl" Src="~/Controls/BookBrowseControl.ascx" %>
<%@ Register TagPrefix="sb" TagName="SectionBrowseControl" Src="~/Controls/SectionBrowseControl.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar ID="NavBar1" runat="server" />
<div id="browseContainerDiv">
    <nav id="sub-nav-tabs" class="no-js-hide">
        <div class="column-wrap">
            <div id="linkbar">
            <h3>Subject "<%: Subject %>"</h3>
            <ul>
                <li runat="server" id="spanTitleSummary" class="titles">
                    <a href="#/titles" title="Books">Books/Journals (<%:count%>)</a>
                </li>
                <li runat="server" id="spanSectionSummary" class="sections">
                    <a href="#/sections" title="Parts">Chapters/Articles/Treatments (<%:segmentcount%>)</a>
                </li>
                </ul>
            </div>
        </div>
    </nav>

    <div id="sortbar" clientidmode="Static">
        <div class="column-wrap">
            <span>Sort By:</span> 
            <ul>
            <li class="<%= SetSortClass("title") %>"><a href="/subject/<%: HttpUtility.UrlEncode(Subject) %>/title">Title</a></li>
            <li class="<%= SetSortClass("author") %>"><a href="/subject/<%: HttpUtility.UrlEncode(Subject) %>/author">Author</a></li>
            <li class="<%= SetSortClass("year") %>"><a href="/subject/<%: HttpUtility.UrlEncode(Subject) %>/year">Year</a></li>
            </ul>
            <div class="floatclear"></div>
        </div>
    </div>

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
            $('#linkbar a').removeClass('active');
            $('#linkbar .' + stateName + ' a').addClass('active').blur();

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
