<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpenURLMultiple.aspx.cs" Inherits="MOBOT.BHL.Web2.OpenURLMultiple" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">    
    <h1 class="column-wrap">        
        OpenUrl Results          
    </h1>
</div>
<div id="content" class="column-wrap clearfix">
    <section>
        <p class="header">Select one of the items below to find the desired citation.</p>
        <% if (BookList.Count > 0) { %>
            <h3>Books/Journals</h3>
            <ol class="data">        
            <% foreach (OpenUrlBookResult book in BookList) { %>
                <li>
                    <a href="<%= book.Url %>"><%= book.Title %></a>
				    <% if (!string.IsNullOrEmpty(book.Volume)) { %><div>Volume: <%= book.Volume%></div><% } %>
				    <% if (!string.IsNullOrEmpty(book.Issue)) { %><div>Issue: <%= book.Issue %></div><% } %>
                    <% if (!string.IsNullOrEmpty(book.Year)) { %><div>Year: <%= book.Year %></div><% } %>
                    <% if (!string.IsNullOrEmpty(book.Pages)) { %><div>Pages: <%= book.Pages %></div><% } %>
                </li>
            <% } %>
            </ol>
        <% } %>

        <% if (SegmentList.Count > 0) { %>
            <h3>Articles/Chapters/Treatments</h3>
            <ol class="data">
            <% foreach (Segment segment in SegmentList) { %>
                <li>
                    <a href="/part/<%= segment.SegmentID %>"><%= segment.Title %></a>
                </li>
            <% } %>
            </ol>
        <% } %>
    </section>
    <aside>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
