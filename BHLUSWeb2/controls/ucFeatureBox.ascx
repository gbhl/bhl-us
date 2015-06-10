<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFeatureBox.ascx.cs" Inherits="MOBOT.BHL.Web2.ucFeatureBox" %>

<asp:Panel ID="panBlog" runat="server" Visible="false">

</asp:Panel>
<asp:Panel ID="panSupport" runat="server"  Visible="false">
        <div  class="<%= SpecialClass %> support" id="<%= specialID %>">
        <h3>Help Support <span>BHL</span></h3>
        <p>BHL's existence depends on the support of its patrons. Help us keep this free resource alive!</p>
                <a  class="featurebutton-home" target="_blank" href="https://donate.sil.si.edu/v/DonateBHL.asp">Donate Now</a>
        </div>
</asp:Panel>
<asp:Panel ID="panFlickr" runat="server"  Visible="false">
<div class="<%= SpecialClass %>" id="<%= specialID %>" >
            <h3>Today's Picks</h3>
            <h4>Flickr <span>Stream</span></h4>
            <span class="content">        <div class="jMyCarousel">
            <asp:Literal ID="flickrList" runat="server"></asp:Literal>
             </div> 
        
            </span>
             <a target="_blank" class="featurebutton-home" href="http://www.flickr.com/photos/biodivlibrary/sets/">View More Images on Flickr</a>
        </div>

</asp:Panel>
<asp:Panel ID="panCollection" runat="server"  Visible="false">
    <div class="<%= SpecialClass %>"  id="<%= specialID %>" >
        <h3>Featured Content</h3>
        <h4><%= title %></h4>
         <span class="content">
         <a id="lnkFeaturedCollectionImage" title="" href="{0}/collection/{1}" runat="server">
<img id="imgFeaturedCollection" alt="Shark Week Collection" src=""  runat="server"/>
</a></span>
         <a id="lnkCollectionButton" class="featurebutton-home" href="{0}/collection/{1}" runat="server">Explore</a>
        </div>
</asp:Panel>
