<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemPage.aspx.cs" Inherits="MOBOT.BHL.Web2.ItemPage" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">
<h1 class="column-wrap"><%: BhlTitle.ShortTitle%> <%: BhlItem.Volume %></h1>
</div>
<div id="content" class="column-wrap clearfix">
<section>
    <!--
   <p>Title: <a href="/bibliography/<%: BhlTitle.TitleID %>"><%: BhlTitle.FullTitle %> <%: BhlTitle.PartNumber %> <%: BhlTitle.PartName %></a></p>
   <p>Volume: <%: BhlItem.Volume %></p>
   -->

    <h2>Parts</h2>
    <div class="data titles">
    <asp:Repeater ID="segmentRepeater" runat="server">
						<ItemTemplate>
							<li>
                            <a href="/part/<%# Eval("SegmentID")%>" title="Segment"><%# Eval("Title") %></a>
                            <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
                            <%# Eval("GenreName") == string.Empty ? "" : "<div class=\"titledetails\">Type: " + Eval("GenreName").ToString()+"</div>" %>
                            <%# Eval("ContainerTitle") == string.Empty ? "" : "<div class=\"titledetails\">In: " + Eval("ContainerTitle").ToString()+"</div>" %>
                            <%# Eval("Volume") == string.Empty ? "" : "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>"%>
                            <%# Eval("Series") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Series") + "</div>"%>
                            <%# Eval("Issue") == string.Empty ? "" : "<div class=\"titledetails\">Issue: " + Eval("Issue") + "</div>"%>
                            <%# Eval("Date") == string.Empty ? "" : "<div class=\"titledetails\">Date: " + Eval("Date") + "</div>"%>
                            <%# Eval("PageRange") == string.Empty ? "" : "<div class=\"titledetails\">Page Range: " + Eval("PageRange") + "</div>"%>
                            <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
                            <%# (Eval("StartPageID") == null) ? "":"<a class=\"titleviewbook\" href=\"/page/" + Eval("StartPageID") + "\">View "+  Eval("GenreName")+ "</a> " %>
                            <%# Eval("URL") == string.Empty ? "":"<a class=\"titleviewbook\" href=\"" + Eval("URL")+ "\">View "+  Eval("GenreName")+ " (External)</a>" %>
                            <%# Eval("DownloadURL") == string.Empty ? "":"<a class=\"titleviewbook\" href=\"" + Eval("DownloadURL")+ "\">Download "+  Eval("GenreName")+ "</a>" %>
						</li>
						</ItemTemplate>
						<HeaderTemplate>
							<ol>
						</HeaderTemplate>
						<FooterTemplate>
							</ol></FooterTemplate>    
    </asp:Repeater>
    
    </div>


</section>
<aside>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
</aside>

</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
