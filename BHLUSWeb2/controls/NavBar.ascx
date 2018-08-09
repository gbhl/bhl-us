<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavBar.ascx.cs" Inherits="MOBOT.BHL.Web2.NavBar" %>
<nav id="content-nav">
    <div class="column-wrap">        
        <div id="browsebar">
            <h3>Browse by:</h3>
            <ul>
                <li class="titles first-child"><a class="png_bg" href="/browse/titles/a">Title</a></li>
                <li class="authors"><a class="png_bg" href="/browse/authors/a">Author</a></li>
                <li class="year"><a class="png_bg" href="/browse/year">Date</a></li>
                <li class="collection"><a class="png_bg" href="/browse/collections">Collection</a></li>
                <li class="contributor last-child"><a class="png_bg" href="/browse/contributors">Contributor</a></li>
			</ul>
        </div>
        <div id="searchbar">
            <span id="searchbar-searchtype">
                <input name="rdoSearchType" runat="server" id="rdoSearchTypeF" ClientIDMode="Static" type="radio" value="F" checked /> Full-text
                <input name="rdoSearchType" runat="server" id="rdoSearchTypeC" ClientIDMode="Static" type="radio" value="C" /> Catalog
            </span>
            <a href="/advsearch" title="Advanced Search">advanced search</a>
        	<asp:TextBox id="tbSearchTerm" CssClass="field" runat="server" ClientIDMode="Static" />
            <asp:Button id="btnSearchSubmit" CssClass="button" runat="server" Text="submit" OnClick="btnSearchSubmit_Click" ClientIDMode="Static" />
        </div>

    </div>
</nav>