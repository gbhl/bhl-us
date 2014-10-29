<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContributorListControl.ascx.cs" Inherits="MOBOT.BHL.Web2.controls.ContributorListControl" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>

<div id="page-title">    
    <h1 class="column-wrap">
        All <asp:Literal ID="litNumContributors" runat="server"></asp:Literal> <span class="highlight">Contributors</span>
    </h1>
</div>

<div id="content" class="column-wrap clearfix">
    <section>
        <h2>BHL Participating Institutions</h2>
        <asp:DataList ID="dlMembers" runat="server" RepeatDirection="Vertical" RepeatColumns="2" CellPadding="3" Width="100%" ItemStyle-Wrap="true">
            <ItemTemplate>
                <%# Eval("InstitutionName")%>
                <br /><a href='/browse/contributor/<%# Eval("InstitutionCode") %>'>View All</a>
                <%# Eval("InstitutionUrl") != string.Empty ? "&nbsp;&nbsp;<a href='" + Eval("InstitutionUrl") + "' target='_blank'>More Information</a>" : ""%>
                <br /><br />
            </ItemTemplate>
            <HeaderStyle CssClass="data contributors" />
        </asp:DataList>

        <h2>Other Contributors</h2>
        <asp:DataList ID="dlNonMembers" runat="server" RepeatDirection="Vertical" RepeatColumns="2" CellPadding="3" Width="100%" ItemStyle-Wrap="true">
            <ItemTemplate>
                <%# Eval("InstitutionUrl") != string.Empty ? "<a href='" + Eval("InstitutionUrl") + "' target='_blank' class='TitleTag'>" : ""%>
                <%# Eval("InstitutionName")%>
                <%# Eval("InstitutionUrl") != string.Empty ? "</a>" : ""%>
                <br /><a href='/browse/contributor/<%# Eval("InstitutionCode") %>' class='small'>View All</a>
                <br /><br />
            </ItemTemplate>
            <HeaderStyle CssClass="data contributors" />
        </asp:DataList>
    </section>
    <aside>
      <uc:FeatureBox runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>