<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContributorListControl.ascx.cs" Inherits="MOBOT.BHL.Web2.controls.ContributorListControl" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>

<div id="page-title">    
    <h1 class="column-wrap">
        All <asp:Literal ID="litNumContributors" runat="server"></asp:Literal> <span class="highlight">Contributors</span>
    </h1>
</div>

<div id="content" class="column-wrap clearfix">
    <section>
        <p>
            The following contributors have cooperated to digitize content from their collections for free and 
            open access via the BHL and the <a href="https://archive.org" target="_blank">Internet Archive</a>. 
            For a list of licensors who have generously provided permission to include in-copyright titles in 
            the BHL, please see our <a href="https://biodivlib.wikispaces.com/permissions" target="_blank">Permissions page</a>.
        </p>
        <h2>BHL Participating Contributors</h2>
        <asp:DataList ID="dlMembers" runat="server" RepeatDirection="Vertical" Width="100%" ItemStyle-Wrap="true" role="presentation" >
            <ItemTemplate>
                <div style="margin-bottom:14px">
                    <a href='/browse/contributor/<%# Eval("InstitutionCode") %>'><%# Eval("InstitutionName") %></a>
                </div>
            </ItemTemplate>
            <HeaderStyle CssClass="data contributors" />
        </asp:DataList>

        <h2>Other Contributors</h2>
        <asp:DataList ID="dlNonMembers" runat="server" RepeatDirection="Vertical" RepeatColumns="2" Width="100%" ItemStyle-Wrap="true" role="presentation">
            <ItemTemplate>
                <div style="margin-bottom:14px">
                    <a href='/browse/contributor/<%# Eval("InstitutionCode") %>'><%# Eval("InstitutionName") %></a>
                </div>
            </ItemTemplate>
            <HeaderStyle CssClass="data contributors" />
        </asp:DataList>
    </section>
    <aside>
      <uc:FeatureBox runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>