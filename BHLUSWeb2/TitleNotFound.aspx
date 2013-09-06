<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TitleNotFound.aspx.cs" Inherits="MOBOT.BHL.Web2.TitleNotFound" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="message" class="column-wrap">
    <section>
        <h1>Title not found</h1>
        <p>We're sorry, but we were not able to find the title you requested. Please try the browsing options above to try to locate the title you want.</p>
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
