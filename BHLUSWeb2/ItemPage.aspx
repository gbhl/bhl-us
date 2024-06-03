<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemPage.aspx.cs" Inherits="MOBOT.BHL.Web2.ItemPage" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">
<h1 class="ellipsis column-wrap"><%: BhlTitle.ShortTitle%> <%: BhlBook.Volume %></h1>
</div>
<div id="content" class="column-wrap clearfix">
<section>
    <h2>Parts</h2>
    <div class="data titles">
    <asp:Repeater ID="segmentRepeater" runat="server">
		<ItemTemplate>
			<li>
            <div style="display:inline-block; width:620px">
                <div style="float:left">
                    <%# (Eval("StartPageID") == null && Eval("URL").ToString() == string.Empty) ?  Eval("Title") : "" %>
                    <%# (Eval("StartPageID") != null) ? "<a class=\"title\" href=\"/page/" + Eval("StartPageID").ToString() + "\" \\>" + Eval("Title").ToString() + "</a>" : "" %>
                    <%# (Eval("StartPageID") == null && Eval("URL").ToString() != string.Empty) ? "<a target=\"_blank\" rel=\"noopener noreferrer\" class=\"title ExtLinkBrowse\" href=\"" + Eval("URL").ToString() + "\">" + Eval("Title").ToString() + "</a>" : "" %>
                </div>
                <div style="float:right">
                    <a class="titleviewbook" href="/part/<%# Eval("SegmentID")%>">View Metadata</a>
                </div>
            </div>
            <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
            <%# Eval("GenreName") == string.Empty ? "" : "<div class=\"titledetails\">Type: " + Eval("GenreName").ToString()+"</div>" %>
            <%# Eval("ContainerTitle") == string.Empty ? "" : "<div class=\"titledetails\">In: " + Eval("ContainerTitleExtended").ToString()+"</div>" %>
            <%# Eval("Volume") == string.Empty ? "" : "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>"%>
            <%# Eval("Series") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Series") + "</div>"%>
            <%# Eval("Issue") == string.Empty ? "" : "<div class=\"titledetails\">Issue: " + Eval("Issue") + "</div>"%>
            <%# Eval("Date") == string.Empty ? "" : "<div class=\"titledetails\">Date: " + Eval("Date") + "</div>"%>
            <%# Eval("PageRange") == string.Empty ? "" : "<div class=\"titledetails\">Page Range: " + Eval("PageRange") + "</div>"%>
            <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
		</li>
		</ItemTemplate>
		<HeaderTemplate>
			<ol>
		</HeaderTemplate>
		<FooterTemplate>
			</ol>
		</FooterTemplate>
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
