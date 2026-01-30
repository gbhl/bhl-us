<%@ Page Title="Biodiversity Heritage Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MOBOT.BHL.Web2._Default" %>
    <%@ MasterType VirtualPath="~/Site.master" %>
    <%@ Register TagPrefix="rss" Assembly="BHLUSWeb2" Namespace="MOBOT.BHL.Web2" %>
    <%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
    <asp:Content ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
    <link href="/js/jMyCarousel/jMyCarousel.css?v=2" type="text/css" rel="stylesheet">

    </asp:Content>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
<div class="column-wrap" style="position:relative">
<div id="promo-home">
<div id="heroimage-home"><img title="<%= homeHeroText %>" alt="<%= homeHeroText %>" src="images/0<%= homeHeroImage %>_home_pic_img.png" /></div>
<% if (MOBOT.BHL.Web2.AppConfig.ShowNewFuture) { %>
<div id="newfuturebox-home" class="featurebox-home">
    <h3>Future of BHL</h3>
    <p style="margin:10px 0"><%=MOBOT.BHL.Web2.AppConfig.NewFutureNewsText%><br><br></p>
    <a  class="featurebutton-home" target="_blank" rel="noopener noreferrer" href="<%=MOBOT.BHL.Web2.AppConfig.NewFutureNewsUrl%>">Learn More</a>
</div>
<%} else { %>
<div id="promobox-home" class="featurebox-home">
    <h3>Help Support <span>BHL</span></h3>
    <p>BHL's existence depends on the financial support of its patrons. Help us keep this free resource alive!</p>
    <a  class="featurebutton-home" target="_blank" rel="noopener noreferrer" href="<%=MOBOT.BHL.Web2.AppConfig.DonateUrl%>">Donate Now</a>
</div>
<%}%>
</div>
</div>
 <div id="intro-home" class="column-wrap" style="min-height:100px">
    <div style="padding: 5px 0px">
        <p class="intro-home-tagline">Inspiring discovery through free access to biodiversity knowledge.</p>
        <p>The Biodiversity Heritage Library improves research methodology by collaboratively making biodiversity literature openly available to the world as part of a global biodiversity community.</p>
        <p>Please read BHL&apos;s <a title="harmful content" href="https://about.biodiversitylibrary.org/about/harmful-content">Acknowledgment of Harmful Content</a></p>
    </div>
</div>

<div id="searchbar-home">
	    <div id="searchbox-home" class="column-wrap">
            <span>Search across books and journals, scientific names, authors and subjects</span>
            <asp:TextBox id="tbSearchTerm" CssClass="field" runat="server" ClientIDMode="Static" Text="Search the catalog and full-text" />
            <asp:Button id="btnSearchSubmit" CssClass="button" runat="server" Text="submit" OnClick="btnSearchSubmit_Click" ClientIDMode="Static" />
            <div id="searchtype-home">
                <input name="rdoSearchType" runat="server" id="rdoSearchTypeF" ClientIDMode="Static" type="radio" value="F" checked /> <label for="rdoSearchTypeF">Full-text</label>
                <input name="rdoSearchType" runat="server" id="rdoSearchTypeC" ClientIDMode="Static" type="radio" value="C" /> <label for="rdoSearchTypeC">Catalog</label>
                &nbsp;<a target="_blank" rel="noopener noreferrer" style="left:169px" href="https://about.biodiversitylibrary.org/ufaqs/how-do-i-search-the-bhl-collection/"><img src="/images/help.png" alt="Search help" title="What's This?" height="16" width="16" /></a>
            </div>
            <a href="/advsearch" class="advsearch-home">advanced search</a>
        </div>
</div>

    <div id="browsebar-home">
    <div id="browsebox-home" class="column-wrap">
        <h2>Browse by:</h2>
        <ul>
            <li class="titles"><a class="png_bg" href="/browse/titles/a">Title</a></li>
            <li class="authors"><a class="png_bg" href="/browse/authors/a">Author</a></li>
            <li class="year"><a class="png_bg" href="/browse/year">Date</a></li>
            <li class="collection"><a class="png_bg" href="/browse/collections">Collection</a></li>
            <li class="contributor"><a class="png_bg" href="/browse/contributors">Contributor</a></li>
        </ul>
    </div>
    </div>
    <div id="featured-home" class="column-wrap">
        <div id="featureone-home" class="featurebox-home">
            <h3>New on the</h3>
            <h4>BHL <span>Blog</span></h4>
             <span class="content">
             <rss:RssFeedControlVersion2 ID="rssFeed" runat="server" MaxRecords="3" Target="_blank" NoItemsFoundText="No BHL blog items found."
											ShowDescription="true" DescriptionLimit="100" />
             </span>
            <a target="_blank" rel="noopener noreferrer" class="featurebutton-home" href="http://blog.biodiversitylibrary.org/">View More Blog Posts</a>
        </div>
             <uc:FeatureBox ID="featurebox3" runat="server" FeatureType="flickr" SpecialClass="featurebox-home" ClientIDMode="Static" specialID="featuretwo-home"></uc:FeatureBox>
        
      <!--  <div id="featurethree-home" class="featurebox-home"> -->
             <uc:FeatureBox ID="FeatureBox4" runat="server" FeatureType="collection" SpecialClass="featurebox-home" ClientIDMode="Static"></uc:FeatureBox>
      <!--  </div> -->
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="scriptContentPlaceHolder" runat="server" >
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {
        var searchDefaultTexts = ["Search the catalog and full-text", "Search the catalog"];

        $('#tbSearchTerm')
            .val(searchDefaultTexts[0])
            .focus(function () {
                if ($.inArray($(this).val(), searchDefaultTexts) >= 0) {
                    $(this).val("");
                }
            })
            .blur(function () {
                if ($.trim($(this).val()) == "") {
                    if ($("#rdoSearchTypeF").is(":checked")) $(this).val(searchDefaultTexts[0]);
                    if ($("#rdoSearchTypeC").is(":checked")) $(this).val(searchDefaultTexts[1]);
                }
            });

        $('#rdoSearchTypeF')
            .change(function () {
                if ($(this).is(':checked')) {
                    if ($.inArray($('#tbSearchTerm').val(), searchDefaultTexts) >= 0) $('#tbSearchTerm').val(searchDefaultTexts[0]);
                }
            });

        $('#rdoSearchTypeC')
            .change(function () {
                if ($(this).is(':checked')) {
                    if ($.inArray($('#tbSearchTerm').val(), searchDefaultTexts) >= 0) $('#tbSearchTerm').val(searchDefaultTexts[1]);
                }
            });

        $('#btnSearchSubmit').click(function (e) {
            if ($.inArray($("#tbSearchTerm").val(), searchDefaultTexts) >= 0) {
                e.preventDefault();
                return false;
            }
        });
    });
//]]>
</script>
   
        <script type="text/javascript">
            $(function () {
                $('.jMyCarousel').jMyCarousel({
                    visible: '1',
                    auto: false,
                    circular: true,
                    eltByElt: true,
                    evtStart: 'mousedown',
                    evtStop: 'mouseup',
                    height: 210,
                    speed: 200
                });
            });
    </script>

</asp:Content>