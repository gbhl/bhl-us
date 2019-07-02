<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemUnavailable.aspx.cs" Inherits="MOBOT.BHL.Web2.ItemUnavailable" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="message" class="column-wrap">
    <section>
        <h1>Item Unavailable</h1>
        <p>The content you are trying to access is no longer available through the Biodiversity Heritage Library.</p>
        <p>We apologize for the inconvenience. Please <a href="<%= System.Configuration.ConfigurationManager.AppSettings["WikiPageItemUnavailable"] %>">click here</a> for more information.</p>
        <span class="arrow message"></span>
    </section>
    <aside>
        <img src="/images/bhlau images/image_general.jpg" height="413" width="620" />
    </aside>
</div>
<div class="floatclear"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
