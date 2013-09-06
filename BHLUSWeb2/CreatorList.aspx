<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatorList.aspx.cs" Inherits="MOBOT.BHL.Web2.CreatorList" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<nav id="sub-nav">
    <div class="column-wrap">
        <div id="linkbar" class="letters">
            <ul>
                <li class="first-child"><a class="<%= SetClass("a") %>" href="/browse/authors/a">A</a></li>
                <li><a class="<%= SetClass("b") %>" href="/browse/authors/b">B</a></li>
                <li><a class="<%= SetClass("c") %>" href="/browse/authors/c">C</a></li>
                <li><a class="<%= SetClass("d") %>" href="/browse/authors/d">D</a></li>
                <li><a class="<%= SetClass("e") %>" href="/browse/authors/e">E</a></li>
                <li><a class="<%= SetClass("f") %>" href="/browse/authors/f">F</a></li>
                <li><a class="<%= SetClass("g") %>" href="/browse/authors/g">G</a></li>
                <li><a class="<%= SetClass("h") %>" href="/browse/authors/h">H</a></li>
                <li><a class="<%= SetClass("i") %>" href="/browse/authors/i">I</a></li>
                <li><a class="<%= SetClass("j") %>" href="/browse/authors/j">J</a></li>
                <li><a class="<%= SetClass("k") %>" href="/browse/authors/k">K</a></li>
                <li><a class="<%= SetClass("l") %>" href="/browse/authors/l">L</a></li>
                <li><a class="<%= SetClass("m") %>" href="/browse/authors/m">M</a></li>
                <li><a class="<%= SetClass("n") %>" href="/browse/authors/n">N</a></li>
                <li><a class="<%= SetClass("o") %>" href="/browse/authors/o">O</a></li>
                <li><a class="<%= SetClass("p") %>" href="/browse/authors/p">P</a></li>
                <li><a class="<%= SetClass("q") %>" href="/browse/authors/q">Q</a></li>
                <li><a class="<%= SetClass("r") %>" href="/browse/authors/r">R</a></li>
                <li><a class="<%= SetClass("s") %>" href="/browse/authors/s">S</a></li>
                <li><a class="<%= SetClass("t") %>" href="/browse/authors/t">T</a></li>
                <li><a class="<%= SetClass("u") %>" href="/browse/authors/u">U</a></li>
                <li><a class="<%= SetClass("v") %>" href="/browse/authors/v">V</a></li>
                <li><a class="<%= SetClass("w") %>" href="/browse/authors/w">W</a></li>
                <li><a class="<%= SetClass("x") %>" href="/browse/authors/x">X</a></li>
                <li><a class="<%= SetClass("y") %>" href="/browse/authors/y">Y</a></li>
                <li><a class="<%= SetClass("z") %>" href="/browse/authors/z">Z</a></li>
            </ul>
        </div>
    </div>
</nav>
<div id="page-title">    
    <h1 class="column-wrap">
        <span class="arrow authors"></span>
        <% if (string.IsNullOrEmpty(Start)) { %>
            All <%= BhlAuthorList.Count%> <span class="highlight">Authors</span>
        <% } else { %>
            <%= BhlAuthorList.Count%> <span class="highlight">Authors</span> beginning with <q><%= Start.ToUpper()%></q> 
        <% } %>
    </h1>
</div>
<div id="content" class="column-wrap clearfix">
    <section>
    <% if (BhlAuthorList.Count > 0)
       { %>
        <ul class="data authors">
        <% foreach (Author creator in BhlAuthorList)
           { %>
            <li>
                <a href="/creator/<%: creator.AuthorID %>">
                    <%: creator.NameExtended %> 
                </a>
			</li>
        <% } %>
        </ul>
    <% } %>
    </section>
    <aside>
      <uc:FeatureBox runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
