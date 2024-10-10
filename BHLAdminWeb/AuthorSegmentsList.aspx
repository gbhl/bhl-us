<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorSegmentsList.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.AuthorSegmentsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Segments For Author</title>
	<link rel="stylesheet" type="text/css" runat="server" id="link1" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%"><tr><td><b>Author: <%=authorName%></b></td><td align="right"><b>Number of Segments: <%=numberOfSegments%></b></td></tr></table>
	    <asp:GridView ID="segmentList" runat="server" 
	        AutoGenerateColumns="False" CellPadding="2" GridLines="None" 
	        AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
		    CssClass="boxTable" Font-Size="Small" HeaderStyle-CssClass="boxHeader" Width="100%">
		    <Columns>
            <asp:TemplateField HeaderText="ID" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <a id="hypSegments" target="_blank" href="<%# ConfigurationManager.AppSettings["BaseUrl"]%>/part/<%# Eval( "SegmentID" ) %>"><%# Eval("SegmentID") %></a>
                </ItemTemplate>
            </asp:TemplateField>
		    <asp:BoundField HeaderText="Title" DataField="Title" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
		    <asp:BoundField HeaderText="Date" DataField="Date" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
		    <asp:BoundField HeaderText="Type" DataField="GenreName" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" />
		    </Columns>
        </asp:GridView>    
    </div>
    </form>
</body>
</html>
