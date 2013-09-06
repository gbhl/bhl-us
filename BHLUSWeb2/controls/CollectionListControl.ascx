<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectionListControl.ascx.cs" Inherits="MOBOT.BHL.Web2.Controls.CollectionListControl" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>

<div id="page-title">    
    <h1 class="column-wrap">
        <span class="arrow collections"></span>
        All <asp:Literal ID="litNumCollections" runat="server"></asp:Literal> <span class="highlight">Collections</span>
    </h1>
</div>
<div id="content" class="column-wrap clearfix">
    <section>
    <asp:Repeater ID="rptCollections" runat="server">
	    <ItemTemplate>
		    <li>
			    <a class="collectiontitle" href="/browse/collection/<%# Eval("CollectionURL") == string.Empty ? Eval("CollectionID").ToString() : Eval("CollectionURL") %>" title="Books">
                <%# Eval("CollectionName").ToString()%></a>
                <p class="collectiondescription"><%# Eval("CollectionDescription") == string.Empty ? "" : Eval("CollectionDescription").ToString()%>
                </p>
                <%# Eval("HtmlContent") == string.Empty ? string.Empty : "<a class=\"collectionfurtherinfo\" href='/collection/" + (Eval("CollectionURL") == string.Empty ? Eval("CollectionID").ToString() : Eval("CollectionURL")) + "' title='Collection'>Further information</a>" %>
               
                
             
		    </li>
	    </ItemTemplate>
	    <HeaderTemplate>
		    <ul class="data collections">
	    </HeaderTemplate>
	    <FooterTemplate>
		    </ul>
	    </FooterTemplate>
    </asp:Repeater>
     </section>
    <aside>
      <uc:FeatureBox runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>