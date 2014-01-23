<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recent.aspx.cs" Inherits="MOBOT.BHL.Web2.Recent" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar ID="NavBar1" runat="server" />
<div id="page-title">    
    <h1 class="column-wrap" style="color: #CEE4F2;">        
        Recent Additions 
        <asp:Label ID="lblNumberDisplayed" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblLanguage" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblContributor" runat="server" Text=""></asp:Label>
    </h1>
    <div id="content" class="column-wrap clearfix">
        <div>
            Show &nbsp; &nbsp;  
    		<a href="/recent/25" id="lnkRecent25" runat="server" title="Last 25">Last 25</a> | 
    		<a href="/recent/50" id="lnkRecent50" runat="server" title="Last 50">Last 50</a> | 
    		<a href="/recent/100" id="lnkRecent100" runat="server" title="Last 100">Last 100</a> | 
    		<a href="/recent/250" id="lnkRecent250" runat="server" title="Last 250">Last 250</a> | 
    		<a href="/recent/500" id="lnkRecent500" runat="server" title="Last 500">Last 500</a></td>
        </div>

        <p>
            RSS Feed location: 
            <a id="rssFeedLink" runat="server" title="RSS" />&nbsp;&nbsp;&nbsp;<a id="rssFeedImageLink" runat="server"><img src="/images/rss.png" alt="RSS" style="vertical-align: middle;" /></a>
        </p>

        <section class="recentfeed">
			<asp:Repeater ID="rptRecent" runat="server">
				<ItemTemplate>
					<li>
                        <a target="<%# string.IsNullOrWhiteSpace(Eval("ExternalUrl").ToString()) ? "_self" : "_blank" %>" href="/item/<%# Eval("ItemID") %>" class="booktitle" title="Book"><%# Eval("FullTitle") %> <%# Eval("PartNumber") %> <%# Eval("PartName") %> <%# Eval("Volume") %></a>
                        &nbsp; (added: <%# Eval("CreationDate","{0:MM/dd/yyyy}") %> )
                    </li>
				</ItemTemplate>
				<HeaderTemplate>
					<ol>
				</HeaderTemplate>
				<FooterTemplate>
					</ol>
                </FooterTemplate>
			</asp:Repeater>
        </section>

    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
