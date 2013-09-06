<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MendeleyShareControl.ascx.cs" Inherits="MOBOT.BHL.Web2.controls.MendeleyShareControl" %>
<a id="aMendeley" class="MendeleyButton" visible="false" title="Add this to your Mendeley library" href="http://www.mendeley.com/import/?url={0}" runat="server" ClientIDMode="Static"><img src="/images/mendeley.png" alt="Add this to your Mendeley library" /></a>
<script type="text/javascript">
    function updateMendeleyLink(type, id) {
        var mendeleyLink = document.getElementById("aMendeley");
        var mendeleyUrl = 'http://www.mendeley.com/import/?url={0}';
        var itemLinkUrl = '<%: ItemPageUrl %>';
        var partLinkUrl = '<%: PartPageUrl %>';

        if (mendeleyLink) {
            if (type === "item") {
                mendeleyLink.href = mendeleyUrl.replace('{0}', itemLinkUrl.replace("{0}", id));
            }
            if (type === "part") {
                mendeleyLink.href = mendeleyUrl.replace('{0}', partLinkUrl.replace("{0}", id));
            }
        }
    }
</script>
