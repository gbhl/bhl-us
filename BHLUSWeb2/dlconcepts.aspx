<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dlconcepts.aspx.cs" Inherits="MOBOT.BHL.Web2.dlconcepts" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">    
    <h1 class="column-wrap">
      <a class="headinglink" href="/collection/darwinlibrary" title="Darwin's Library Homepage">Charles Darwin's Library</a> Concepts - General Index  
    </h1>
</div>
<div id="content-noaside" class="column-wrap">

    <section>
            <div id="browseInnerDivHeader">
                <div><p>The terms in this list have been used to classify the annotations found in the collection of 
                Darwin's Library.  Click a term to view materials the collection that are related to that term.</p>
                </div>
            </div>
            <div id="browseInnerDiv" style="height:100%; overflow:auto;" class="column3">
                <asp:Repeater ID="rptConcepts1" runat="server">
                <ItemTemplate>
                  
                        <a href='/DLIndexBrowse.aspx?concept=<%# Eval("AnnotationConceptCode") %>' title="Concept"><%# Eval("ConceptText") %></a>
                    
                </ItemTemplate>
                </asp:Repeater>
            </div>
             <div id="Div1" style="height:100%; overflow:auto;" class="column3">
                <asp:Repeater ID="rptConcepts2" runat="server">
                <ItemTemplate>
                    
                        <a href='/DLIndexBrowse.aspx?concept=<%# Eval("AnnotationConceptCode") %>' title="Concept"><%# Eval("ConceptText") %></a>
                    
                </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="Div2" style="height:100%; overflow:auto;" class="column3">
                <asp:Repeater ID="rptConcepts3" runat="server">
                <ItemTemplate>
                    
                        <a href='/DLIndexBrowse.aspx?concept=<%# Eval("AnnotationConceptCode") %>' title="Concept"><%# Eval("ConceptText") %></a>
                    
                </ItemTemplate>
                </asp:Repeater>
            </div>
    </section>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
