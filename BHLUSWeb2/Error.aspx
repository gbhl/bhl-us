<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BHLUSWeb2.Error" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="message" class="column-wrap">
    <section>
        <h1>Sorry!</h1>
        <p>Something unexpected has occurred. Please try your request again shortly.</p>
        <span class="arrow message"></span>
    </section>
    <aside>
        <img src="/images/image_general.jpg" height="413" width="620" />
    </aside>
</div>
<div class="floatclear"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
