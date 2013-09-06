<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialLinksPanel.ascx.cs" Inherits="MOBOT.BHL.Web2.SocialLinksPanel" %>

<span id="divLike" runat="server" >
    <div style="width:47px;overflow:hidden;display:inline;">
        <iframe id="ifrmFB" runat="server" src="http://www.facebook.com/plugins/like.php?locale=en_US&href={0}&amp;send=false&amp;layout=button_count&amp;width=49&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=tahoma&amp;height=21" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:49px; height:21px;" allowtransparency="true"></iframe>
    </div>
    <a id="aTweet" runat="server" href="http://twitter.com/share" class="twitter-share-button" data-count="none" title="Tweet">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>&nbsp;
</span>
<span id="divDemo" runat="server" visible="false">
    <img src="/images/facebook_like_normal.png" alt="Like on facebook"/><img src="/images/tweet_like_normal.png" alt="tweet this"/>
</span>