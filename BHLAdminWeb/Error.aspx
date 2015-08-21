<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<div id="message" class="column-wrap">
    <section>
        <h1>Internal Server Error</h1>
        <p>We're sorry, but an unexpected error has occurred. Please try your request again shortly.</p>
        <span class="arrow message"></span>
    </section>
    <aside>
        <img src="/images/image_general.jpg" height="413" width="620" />
    </aside>
</div>
<div class="floatclear"></div>
</asp:Content>
