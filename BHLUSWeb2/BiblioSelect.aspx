<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BiblioSelect.aspx.cs" Inherits="MOBOT.BHL.Web2.BiblioSelect" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">    
    <h1 class="column-wrap">        
        Select Bibliography To View
    </h1>
</div>
<div id="content" class="column-wrap clearfix">
    <section>
        <ol class="data">        
        <% foreach (Title title in TitleList) { %>
            <li>
                <a href="/bibliography/<%= title.TitleID %>">
				    <%= title.FullTitleExtended %>
				</a>
				<div>Publication info: <%= title.PublicationDetails %></div>
            </li>
        <% } %>
        </ol>
    </section>
    <aside>
       <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
