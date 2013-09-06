<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuthorNotFound.aspx.cs" Inherits="MOBOT.BHL.Web2.AuthorNotFound" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="message" class="column-wrap">
    <section>
        <h1>Author not found</h1>
        <p>We’re sorry, but we were not able to find the <a href="/browse/authors">author</a> you requested. Please try browsing our list of authors to try to locate the name you are looking for..</p>
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
