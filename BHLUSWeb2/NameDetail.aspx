<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NameDetail.aspx.cs" Inherits="MOBOT.BHL.Web2.NameDetail" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="browseContainerDiv">
    <nav id="sub-nav-tabs" class="no-js-hide">
        <div class="column-wrap">
            <div id="linkbar">
            <h3>Name "<%= NameClean %>"</h3>
            </div>
        </div>
    </nav>

    <div id="sub-sections" class="column-wrap clearfix">
	    <asp:Panel ID="titles" Visible="true" runat="server" ClientIDMode="Static">	
            <div id="namedetails" class="content">
                <section>
                    
                    <p><a href="/name/<%=NameParam %>">Browse the bibliography</a> for "<%= NameClean %>".</p>
                    <asp:Repeater runat="server" ID="rptNameDetails">
                        <ItemTemplate>
                            <li class="titlelisting">
                                <div class="titledetails">
                                    <h3><%# Eval("DataSourceTitle") %></h3>
                                </div>
                                <div class="titledetails">Name: 
                                    <%# Eval("Url") == string.Empty ? "" : "<a target=\"_blank\" href=\"" + Eval("Url")  + "\">" %>
                                        <%# Eval("NameString") %>
                                    <%# Eval("Url") == string.Empty ? "" : "</a>" %>
                                </div>
                                <%# Eval("ClassificationPath") == string.Empty ? "" : "<div class=\"titledetails\">Classification Path: " + Eval("ClassificationPath").ToString().Replace("|", " | ") + "</div>" %>
                                <%# Eval("LocalID") == string.Empty ? "" : "<div class=\"titledetails\">Local Identifier: " + Eval("LocalID") + "</div>"%>
                            </li>
                        </ItemTemplate>
	                    <HeaderTemplate>
		                    <ol  class="data titles">
	                    </HeaderTemplate>
	                    <FooterTemplate>
		                    </ol>
	                    </FooterTemplate>

                    </asp:Repeater>

                    <asp:Literal runat="server" ID="litDetails"></asp:Literal>
                </section>
            </div>
        </asp:Panel>
    </div>
    <aside id="searchaside">
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
