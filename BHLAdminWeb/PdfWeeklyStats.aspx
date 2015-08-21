<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PdfWeeklyStats.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.PdfWeeklyStats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/PdfStats.aspx">&lt; Return to PDF Generation Stats</a><br />
	<br />
	<span class="pageHeader">PDF Generation Stats for <asp:Literal ID="litYear" runat="server"></asp:Literal> - Week <asp:Literal ID="litWeek" runat="server"></asp:Literal> (<asp:Literal ID="litStatusName" runat="server"></asp:Literal> Status)</span><hr />

    <asp:GridView ID="gvWeeklyStats" runat="server" 
        AutoGenerateColumns="False" Width="97%" HorizontalAlign="Left" 
        GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true"
        CellPadding="3" AlternatingRowStyle-BackColor="#F7FAFB" >
    <Columns>
        <asp:HyperLinkField DataTextField="PdfID" HeaderText="PDF" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom" DataNavigateUrlFormatString="PdfEdit.aspx?id={0}" DataNavigateUrlFields="PdfID" />
        <asp:HyperLinkField DataTextField="ItemID" HeaderText="Item" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom" DataNavigateUrlFormatString="ItemEdit.aspx?id={0}" DataNavigateUrlFields="ItemID" />
        <asp:BoundField DataField="EmailAddress" HeaderText="Email" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom" />
        <asp:BoundField DataField="NumberOfPages" HeaderText="Total<br>Pages" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Right" />
        <asp:TemplateField>
        <HeaderStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Bottom" />
        <HeaderTemplate>Include<br />OCR</HeaderTemplate>
        <ItemStyle VerticalAlign="Top" Wrap="false" HorizontalAlign="Center" />
        <ItemTemplate>
        <%# (bool)Eval("ImagesOnly") ? "No" : "Yes" %>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ArticleTitle" HeaderText="Article Title" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom" />
        <asp:BoundField DataField="ArticleCreators" HeaderText="Article Creators" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom" />
        <asp:BoundField DataField="ArticleTags" HeaderText="Article Tags" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom" />
        <asp:TemplateField>
        <HeaderStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Bottom" />
        <HeaderTemplate>File</HeaderTemplate>
        <ItemStyle VerticalAlign="Top" Wrap="false" />
        <ItemTemplate>
        <a href="<%# Eval("FileUrl") %>"><%# Eval("FileUrl").ToString().Substring(Eval("FileUrl").ToString().LastIndexOf('/') + 1) %></a>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="MinutesToGenerate" HeaderText="Minutes To<br>Generate" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
        <asp:BoundField DataField="NumberImagesMissing" HeaderText="Missing<br>Images" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Right" />
        <asp:BoundField DataField="NumberOcrMissing" HeaderText="Missing<br>OCR" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Right" />
    </Columns>
    </asp:GridView>

</asp:Content>
