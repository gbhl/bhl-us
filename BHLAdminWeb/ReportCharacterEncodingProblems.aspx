<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReportCharacterEncodingProblems.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportCharacterEncodingProblems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
    <br />
    <span class="pageHeader">Character Encoding Problems</span><hr />
	<br />
	<div style="width:800px">This report shows data containing suspected invalid characters.  It DOES NOT show data that is missing characters or that is trucated as a result of character encoding problems.  Those problems need to be identified by other means.</div>
	<br />
    <table cellpadding="3" width="750">
        <tr>
            <td>
                Institution:
                <asp:DropDownList ID="listInstitutions" runat="server" DataTextField="InstitutionName" DataValueField="InstitutionCode"/>&nbsp;&nbsp;
                Show Records Added:
                <asp:DropDownList ID="listAddedSince" runat="server" >
                    <asp:ListItem Value="30" Text="Last 30 Days" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="60" Text="Last 60 Days"></asp:ListItem>
                    <asp:ListItem Value="90" Text="Last 90 Days"></asp:ListItem>
                    <asp:ListItem Value="180" Text="Last 180 Days"></asp:ListItem>
                    <asp:ListItem Value="365" Text="1 Year"></asp:ListItem>
                    <asp:ListItem Value="730" Text="2 Years"></asp:ListItem>
                    <asp:ListItem Value="10000" Text="Since Inception"></asp:ListItem>
                </asp:DropDownList>
                <br /><br />
                Include:
                <asp:CheckBox ID="chkTitle" runat="server" Text="Titles" Checked="true" />&nbsp;
                <asp:CheckBox ID="chkSubject" runat="server" Text="Subjects" Checked="true" />&nbsp;
                <asp:CheckBox ID="chkAssociation" runat="server" Text="Associations" Checked="true" />&nbsp;
                <asp:CheckBox ID="chkAuthor" runat="server" Text="Authors" Checked="true" />&nbsp;
                <asp:CheckBox ID="chkItem" runat="server" Text="Items" Checked="true" />
                <br /><br />
                <asp:Button ID="buttonShow" runat="server" OnClick="buttonShow_Click" Text="Show" /><br />
            </td>
        </tr>
        <tr>
            <td>
                <span id="spanTitle" runat="server" visible="false" class="tableHeader"><br />Titles<br /></span>
                <asp:Literal Visible="false" ID="litNoTitles" runat="server" Text="No titles found<br/>"></asp:Literal>
                <div id="divTitle" runat="server" style="overflow:auto;height:300px;width:800px;border-style:solid;border-color:Black;border-width:1px;text-align:center" visible="false">
                    <asp:GridView ID="gvwTitles" runat="server" AutoGenerateColumns="False" CellPadding="2" 
	                    GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="782px" 
	                    HeaderStyle-VerticalAlign="Bottom" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
	                    <Columns>
		                    <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Left">
		                        <ItemTemplate>
		                            <a class='small' href='/bibliography/<%# Eval("TitleID") %>' target="_blank">View Title</a><br />
		                            <a class='small' href='titleedit.aspx?id=<%# Eval("TitleID")%>'>Edit Title</a><br />
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Suspect" ItemStyle-Width="40px">
		                        <ItemTemplate>
		                            <%# Eval("FullTitleSuspect") %><br />
		                            <%# Eval("Datafield260aSuspect")%><br />
		                            <%# Eval("Datafield260bSuspect")%>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="472px">
		                        <ItemTemplate>
		                            <b>Title: </b><%# (Eval("FullTitle").ToString().Length > 65) ? Eval("FullTitle").ToString().Substring(0, 65) + "..." : Eval("FullTitle") %><br />
		                            <b>MARC260a: </b><%# Eval("Datafield260a") %><br />
		                            <b>MARC260b: </b><%# Eval("Datafield260b") %><br />
		                            <b>Institution: </b><%# Eval("InstitutionName") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Title Identifiers" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="135px">
		                        <ItemTemplate>
		                            <b>OCLC:</b><%# Eval("OCLC") %><br />
		                            <b>ZQuery:</b><%# Eval("ZQuery") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:BoundField DataField="CreationDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px" />
	                    </Columns>
	                    <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
                    </asp:GridView>
                </div>

                <span id="spanSubject" runat="server" visible="false" class="tableHeader"><br />Subjects<br /></span>
                <asp:Literal Visible="false" ID="litNoTitleKeywords" runat="server" Text="No subjects found<br/>"></asp:Literal>
                <div id="divTitleKeyword" runat="server" style="overflow:auto;height:300px;width:800px;border-style:solid;border-color:Black;border-width:1px;text-align:center" visible="false">
                    <asp:GridView ID="gvwTitleKeywords" runat="server" AutoGenerateColumns="False" CellPadding="2" 
	                    GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="782px" 
	                    HeaderStyle-VerticalAlign="Bottom" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
	                    <Columns>
		                    <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Left">
		                        <ItemTemplate>
		                            <a class='small' href='/bibliography/<%# Eval("TitleID") %>' target="_blank">View Title</a><br />
		                            <a class='small' href='titleedit.aspx?id=<%# Eval("TitleID")%>'>Edit Title</a><br />
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:BoundField DataField="KeywordSuspect" HeaderText="Suspect" ItemStyle-Width="40px" />
		                    <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="472px">
		                        <ItemTemplate>
		                            <b>Subject: </b><%# Eval("Keyword") %><br />
		                            <b>Institution: </b><%# Eval("InstitutionName") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Title Identifiers" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="135px">
		                        <ItemTemplate>
		                            <b>OCLC:</b><%# Eval("OCLC") %><br />
		                            <b>ZQuery:</b><%# Eval("ZQuery") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:BoundField DataField="CreationDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px" />
	                    </Columns>
	                    <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
                    </asp:GridView>
                </div>

                <span id="spanAssociation" runat="server" visible="false" class="tableHeader"><br />Title Associations<br /></span>
                <asp:Literal Visible="false" ID="litNoAssociations" runat="server" Text="No title associations found<br/>"></asp:Literal>
                <div id="divAssociation" runat="server" style="overflow:auto;height:300px;width:800px;border-style:solid;border-color:Black;border-width:1px;text-align:center" visible="false">
                    <asp:GridView ID="gvwAssociations" runat="server" AutoGenerateColumns="False" CellPadding="2" 
	                    GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="782px" 
	                    HeaderStyle-VerticalAlign="Bottom" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
	                    <Columns>
		                    <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Left">
		                        <ItemTemplate>
		                            <a class='small' href='/bibliography/<%# Eval("TitleID") %>' target="_blank">View Title</a><br />
		                            <a class='small' href='titleedit.aspx?id=<%# Eval("TitleID")%>'>Edit Title</a><br />
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Suspect" ItemStyle-Width="40px">
		                        <ItemTemplate>
		                            <%# Eval("TitleSuspect") %><br />
		                            <%# Eval("SectionSuspect")%><br />
		                            <%# Eval("VolumeSuspect")%><br />
		                            <%# Eval("HeadingSuspect") %><br />
		                            <%# Eval("PublicationSuspect")%><br />
		                            <%# Eval("RelationshipSuspect")%>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="472px">
		                        <ItemTemplate>
		                            <b>Title: </b><%# (Eval("Title").ToString().Length > 65) ? Eval("Title").ToString().Substring(0, 65) + "..." : Eval("Title") %><br />
		                            <b>Section: </b><%# Eval("Section") %><br />
		                            <b>Volume: </b><%# Eval("Volume") %><br />
		                            <b>Heading: </b><%# Eval("Heading") %><br />
		                            <b>Publication: </b><%# Eval("Publication") %><br />
		                            <b>Relationship: </b><%# Eval("Relationship") %><br />
		                            <b>Institution: </b><%# Eval("InstitutionName") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Title Identifiers" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="135px">
		                        <ItemTemplate>
		                            <b>OCLC:</b><%# Eval("OCLC") %><br />
		                            <b>ZQuery:</b><%# Eval("ZQuery") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:BoundField DataField="CreationDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px" />
	                    </Columns>
	                    <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
                    </asp:GridView>
                </div>

                <span id="spanAuthor" runat="server" visible="false" class="tableHeader"><br />Authors<br /></span>
                <asp:Literal Visible="false" ID="litNoAuthors" runat="server" Text="No authors found<br/>"></asp:Literal>
                <div id="divAuthor" runat="server" style="overflow:auto;height:300px;width:800px;border-style:solid;border-color:Black;border-width:1px;text-align:center" visible="false">
                    <asp:GridView ID="gvwAuthors" runat="server" AutoGenerateColumns="False" CellPadding="2" 
	                    GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="782px" 
	                    HeaderStyle-VerticalAlign="Bottom" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
	                    <Columns>
		                    <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Left">
		                        <ItemTemplate>
		                            <a class='small' href='authoredit.aspx?id=<%# Eval("AuthorID")%>'>Edit Author</a><br />
		                            <a class='small' href='/bibliography/<%# Eval("TitleID") %>' target="_blank">View Title</a><br />
		                            <a class='small' href='titleedit.aspx?id=<%# Eval("TitleID")%>'>Edit Title</a><br />
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Suspect" ItemStyle-Width="40px">
		                        <ItemTemplate>
		                            <%# Eval("NameSuspect")%><br />
		                            <%# Eval("NumerationSuspect")%><%# Eval("UnitSuspect")%><br />
		                            <%# Eval("TitleSuspect") %><%# Eval("LocationSuspect")%><br />
		                            <%# Eval("FullerFormSuspect") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="472px">
		                        <ItemTemplate>
		                            <b>Name: </b><%# Eval("FullName") %><br />
		                            <b>Num/Unit: </b><%# Eval("Numeration")%><%# Eval("Unit")%><br />
		                            <b>Title/Loc: </b><%# Eval("Title")%><%# Eval("Location")%><br />
		                            <b>Fuller Form: </b><%# Eval("FullerForm")%><br />
		                            <b>Institution: </b><%# Eval("InstitutionName") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Title Identifiers" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="135px">
		                        <ItemTemplate>
		                            <b>OCLC:</b><%# Eval("OCLC") %><br />
		                            <b>ZQuery:</b><%# Eval("ZQuery") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:BoundField DataField="CreationDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px" />
	                    </Columns>
	                    <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
                    </asp:GridView>
                </div>

                <span id="spanItem" runat="server" visible="false" class="tableHeader"><br />Items<br /></span>
                <asp:Literal Visible="false" ID="litNoItems" runat="server" Text="No items found<br/>"></asp:Literal>
                <div id="divItem" runat="server" style="overflow:auto;height:300px;width:800px;border-style:solid;border-color:Black;border-width:1px;text-align:center" visible="false">
                    <asp:GridView ID="gvwItems" runat="server" AutoGenerateColumns="False" CellPadding="2" 
	                    GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="782px" 
	                    HeaderStyle-VerticalAlign="Bottom" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
	                    <Columns>
		                    <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Left">
		                        <ItemTemplate>
		                            <a class='small' href='/item/<%# Eval("ItemID") %>' target="_blank">View Item</a><br />
		                            <a class='small' href='itemedit.aspx?id=<%# Eval("ItemID")%>'>Edit Item</a><br />
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Suspect" ItemStyle-Width="40px">
		                        <ItemTemplate>
		                            <br />
		                            <%# Eval("VolumeSuspect")%>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="472px">
		                        <ItemTemplate>
		                            <b>Title: </b><%# (Eval("ShortTitle").ToString().Length > 65) ? Eval("ShortTitle").ToString().Substring(0, 65) + "..." : Eval("ShortTitle") %><br />
		                            <b>Volume: </b><%# Eval("Volume")%><br />
		                            <b>Institution: </b><%# Eval("InstitutionName") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:TemplateField HeaderText="Title Identifiers" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="135px">
		                        <ItemTemplate>
		                            <b>OCLC:</b><%# Eval("OCLC") %><br />
		                            <b>ZQuery:</b><%# Eval("ZQuery") %>
		                        </ItemTemplate>
		                    </asp:TemplateField>
		                    <asp:BoundField DataField="CreationDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px" />
	                    </Columns>
	                    <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <br />        
</asp:Content>
