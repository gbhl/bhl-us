<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetApiKey.aspx.cs" Inherits="MOBOT.BHL.Web2.GetApiKey" %>

<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">
    <h1 class="column-wrap">Get an API Key</h1>
</div>

<div id="content" class="column-wrap clearfix">
    <section class="search">

        <div class="ui-tabs">
    		<div id="FeedbackForm" style="display:block" class="ui-tabs-panel" runat="server" ClientIDMode="static">
            <p>
                To obtain a key for use when calling the BHL API methods, please submit your name and email address.  A new API key
                will be generated and emailed to the address that you provide.  This key must be included in each call to the BHL API.
            </p>
            <p>
                <u>If you have lost your key</u>, please re-submit your name and email address (you must use the same email address
                that was used when the key was obtained).
            </p>

            <ol>
                <div class="tabbed">
                    <li><span id="spanErrorText" class="ErrorText" style="display:block !important" runat="server"></span></li>
                </div>
                <li>
                    <label class="caption">Contact Name:</label><asp:TextBox ID="txtContactName" runat="server" Width="400" class="field"></asp:TextBox>
                </li>
                <li>
                    <label class="caption">Email Address:</label><asp:TextBox ID="txtEmailAddress" runat="server" Width="400"></asp:TextBox>
                </li>
            </ol>
            <div class="tabbed">
                <asp:Button ID="btnGetApiKey" runat="server" Text="Get API Key" OnClick="btnGetApiKey_Click" />
            </div>
            </div>
            <div runat="server" id="divDone" visible="false" style="display:block" class="ui-tabs-panel" ClientIDMode="static">
                <p>
                    Your API key has been mailed to <asp:Literal runat="server" ID="litEmail"></asp:Literal>.  Please check your email and
                    then refer to the <a href="<%= MOBOT.BHL.Web.Utilities.AppConfig.WikiPageAPI %>">API documentation</a> to learn how to 
                    begin using the BHL API.
                </p>
            </div>
        </div>
        
    </section>
    <aside></aside>
</div>

</asp:Content>
<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
