<%@ Page Title="Biodiversity Heritage Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MOBOT.BHL.Web2._Default" %>
    <%@ MasterType VirtualPath="~/Site.master" %>
    <%@ Register TagPrefix="MOBOT" Assembly="MOBOT.BHL.Web.Utilities" Namespace="MOBOT.BHL.Web.Utilities" %>
    <%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
    <asp:Content ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
    <link href="/js/jMyCarousel/jMyCarousel.css?v=2" type="text/css" rel="stylesheet">

    </asp:Content>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
<div class="column-wrap" style="position:relative">
<div id="promo-home">
<div id="heroimage-home"><img title="<%= homeHeroText %>" alt="<%= homeHeroText %>" src="images/0<%= homeHeroImage %>_home_pic_img.png" /></div>
<div id="promobox-home" class="featurebox-home">
<h3>Help Support <span>BHL</span></h3>
<p>BHL's existence depends on the financial support of its patrons. Help us keep this free resource alive!</p>
<a  class="featurebutton-home" target="_blank" href="https://donate.sil.si.edu/v/DonateBHL.asp">Donate Now</a>
</div>
</div>
</div>
<div id="intro-home" class="column-wrap">

<p class="intro-home-tagline">Inspiring discovery through free access to biodiversity knowledge.</p>
<p>The Biodiversity Heritage Library improves research methodology by collaboratively making biodiversity literature openly available to the world as part of a global biodiversity community.</p>
<p>BHL also serves as the foundational literature component of the Encyclopedia of Life (<a href="http://www.eol.org" target="_blank" title="EOL"><img src="/images/eol_11px.png" /></a>).</p>

</div>


<div id="searchbar-home">
	    <div id="searchbox-home" class="column-wrap">
            <span>Search across books and journals, scientific names, authors and subjects</span>
            <asp:TextBox id="tbSearchTerm" CssClass="field" runat="server" ClientIDMode="Static" Text="Search" />
            <asp:Button id="btnSearchSubmit" CssClass="button" runat="server" Text="submit" OnClick="btnSearchSubmit_Click" ClientIDMode="Static" />
            <!--[if lt IE 9 ]> <asp:TextBox runat="server" class="hidden"></asp:TextBox> <![endif]-->
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
             <MOBOT:RssFeedControlVersion2 ID="rssFeed" runat="server" MaxRecords="3" Target="_blank" NoItemsFoundText="No BHL blog items found."
											ShowDescription="true" DescriptionLimit="100" />
             </span>
            <a target="_blank" class="featurebutton-home" href="http://blog.biodiversitylibrary.org/">View More Blog Posts</a>
        </div>
             <uc:FeatureBox ID="featurebox3" runat="server" FeatureType="flickr" SpecialClass="featurebox-home" ClientIDMode="Static" specialID="featuretwo-home"></uc:FeatureBox>
        
      <!--  <div id="featurethree-home" class="featurebox-home"> -->
             <uc:FeatureBox ID="FeatureBox4" runat="server" FeatureType="collection" SpecialClass="featurebox-home" ClientIDMode="Static"></uc:FeatureBox>
      <!--  </div> -->
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="scriptContentPlaceHolder" runat="server" >
<script src="/js/libs/jquery.text-overflow.min.js"></script>
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {
        var searchDefaultText = "Search";

        $('#tbSearchTerm')
            .val(searchDefaultText)
            .focus(function () {
                if ($(this).val() == searchDefaultText) {
                    $(this).val("");
                }
            })
            .blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).val(searchDefaultText);
                }
            });

        $('#btnSearchSubmit').click(function (e) {
            if ($('#tbSearchTerm').val() == searchDefaultText) {
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