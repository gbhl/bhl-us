<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CollectionPage.aspx.cs" Inherits="MOBOT.BHL.Web2.CollectionPage" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<%@ Register TagPrefix="bb" TagName="BookBrowseControl" Src="~/Controls/BookBrowseControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />

<nav id="sub-nav">
    <div class="column-wrap">
      
        <div id="linkbar" class="letters">
            <ul>
                <li class="first-child"><a class="<%= SetClass("a") %>" href="/browse/collection/<%= collection.PreferredUrl %>/a">A</a></li>
                <li><a class="<%= SetClass("b") %>" href="/browse/collection/<%= collection.PreferredUrl %>/b">B</a></li>
                <li><a class="<%= SetClass("c") %>" href="/browse/collection/<%= collection.PreferredUrl %>/c">C</a></li>
                <li><a class="<%= SetClass("d") %>" href="/browse/collection/<%= collection.PreferredUrl %>/d">D</a></li>
                <li><a class="<%= SetClass("e") %>" href="/browse/collection/<%= collection.PreferredUrl %>/e">E</a></li>
                <li><a class="<%= SetClass("f") %>" href="/browse/collection/<%= collection.PreferredUrl %>/f">F</a></li>
                <li><a class="<%= SetClass("g") %>" href="/browse/collection/<%= collection.PreferredUrl %>/g">G</a></li>
                <li><a class="<%= SetClass("h") %>" href="/browse/collection/<%= collection.PreferredUrl %>/h">H</a></li>
                <li><a class="<%= SetClass("i") %>" href="/browse/collection/<%= collection.PreferredUrl %>/i">I</a></li>
                <li><a class="<%= SetClass("j") %>" href="/browse/collection/<%= collection.PreferredUrl %>/j">J</a></li>
                <li><a class="<%= SetClass("k") %>" href="/browse/collection/<%= collection.PreferredUrl %>/k">K</a></li>
                <li><a class="<%= SetClass("l") %>" href="/browse/collection/<%= collection.PreferredUrl %>/l">L</a></li>
                <li><a class="<%= SetClass("m") %>" href="/browse/collection/<%= collection.PreferredUrl %>/m">M</a></li>
                <li><a class="<%= SetClass("n") %>" href="/browse/collection/<%= collection.PreferredUrl %>/n">N</a></li>
                <li><a class="<%= SetClass("o") %>" href="/browse/collection/<%= collection.PreferredUrl %>/o">O</a></li>
                <li><a class="<%= SetClass("p") %>" href="/browse/collection/<%= collection.PreferredUrl %>/p">P</a></li>
                <li><a class="<%= SetClass("q") %>" href="/browse/collection/<%= collection.PreferredUrl %>/q">Q</a></li>
                <li><a class="<%= SetClass("r") %>" href="/browse/collection/<%= collection.PreferredUrl %>/r">R</a></li>
                <li><a class="<%= SetClass("s") %>" href="/browse/collection/<%= collection.PreferredUrl %>/s">S</a></li>
                <li><a class="<%= SetClass("t") %>" href="/browse/collection/<%= collection.PreferredUrl %>/t">T</a></li>
                <li><a class="<%= SetClass("u") %>" href="/browse/collection/<%= collection.PreferredUrl %>/u">U</a></li>
                <li><a class="<%= SetClass("v") %>" href="/browse/collection/<%= collection.PreferredUrl %>/v">V</a></li>
                <li><a class="<%= SetClass("w") %>" href="/browse/collection/<%= collection.PreferredUrl %>/w">W</a></li>
                <li><a class="<%= SetClass("x") %>" href="/browse/collection/<%= collection.PreferredUrl %>/x">X</a></li>
                <li><a class="<%= SetClass("y") %>" href="/browse/collection/<%= collection.PreferredUrl %>/y">Y</a></li>
                <li><a class="<%= SetClass("z") %>" href="/browse/collection/<%= collection.PreferredUrl %>/z">Z</a></li>
                <li><a class="<%= SetClass("All") %>" href="/browse/collection/<%= collection.PreferredUrl %>/all">All</a></li>
            </ul>
        </div>
    </div>
</nav>
<div id="page-title">    
    <h1 class="column-wrap">
        <asp:Literal ID="ltlCollectionHeader" runat="server" EnableViewState="false" />
    </h1>
</div>
<div id="sortbar">
<div class="column-wrap">
<span>Sort By:</span> 
    <ul>
    <li class="<%= SetSortClass("title") %>"><a href="/browse/collection/<%= collection.PreferredUrl %>/<%= Start %>/title">Title</a></li>
    <li class="<%= SetSortClass("author") %>"><a href="/browse/collection/<%= collection.PreferredUrl %>/<%= Start %>/author">Author</a></li>
    <li class="<%= SetSortClass("year") %>"><a href="/browse/collection/<%= collection.PreferredUrl %>/<%= Start %>/year">Year</a></li>
    </ul>
    <div class="floatclear"></div>
</div>
</div>
<div id="content" class="column-wrap clearfix">
    <section>
        <asp:Literal runat="server" ID="ltlCollectionStats" Visible="false" Text="<p>This collection contains <span style='font-weight:bolder'>{0}</span> volumes from <span style='font-weight:bolder'>{1}</span> titles, containing <span style='font-weight:bolder'>{2}</span> pages.</p>"></asp:Literal>
        <bb:BookBrowseControl ID="BookBrowse" runat="server" />
    </section>
    <aside>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>
</asp:Content>

<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/jquery.hoverintent.min.js"></script>
<script src="/js/libs/jquery.text-overflow.min.js"></script>
</asp:Content>
