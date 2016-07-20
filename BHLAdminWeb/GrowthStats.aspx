<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="GrowthStats.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.GrowthStats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
    <a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Expanded Growth Stats</span><hr />

    Contributor:
    <asp:DropDownList ID="ddlInstitutions" runat="server">
    <asp:ListItem Value="" Text="(All)"></asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Dates:
    <asp:DropDownList ID="ddlTimespan" runat="server">
    <asp:ListItem Value="0" Text="This Month"></asp:ListItem>
    <asp:ListItem Value="1" Text="This Year"></asp:ListItem>
    <asp:ListItem Value="-3" Text="Last 3 Months"></asp:ListItem>
    <asp:ListItem Value="-6" Text="Last 6 Months" Selected="True"></asp:ListItem>
    <asp:ListItem Value="-12" Text="Last 12 Months"></asp:ListItem>
    <asp:ListItem Value="-24" Text="Last 24 Months"></asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnShow" runat="server" Text="Show" onclick="btnShow_Click" />
    <asp:Button ID="btnDownload" runat="server" Text="Download All For Contributor" OnClick="btnDownload_Click" />
    <br /><br />

    <span class="tableHeader">Item Additions By Month</span><br />
    <div style="overflow:auto;height:175px;width:910px;border-style:solid;border-color:Black;border-width:1px;text-align:center">
        <br /><img ID="imgMonthlyItems" runat="server" src="https://chart.googleapis.com/chart?cht=bvg&chs=800x300&chbh=10,1&chxt=x,y&chdl=Titles|Items|Pages|Names&chd=t:153,113,120|60,70,80|52,60,40|30,50,45&chds=0,153&chxr=0,0,153|1,0,153&chxl=0:|Jan%2009|Feb%2009|Mar%2009&chf=&chco=2c50f2,ffcc00,99cc00,ff0000" />
    </div>
    <br /><br />

    <span class="tableHeader">Page Additions By Month</span><br />
    <div style="overflow:auto;height:175px;width:910px;border-style:solid;border-color:Black;border-width:1px;text-align:center">
        <br /><img ID="imgMonthlyPages" runat="server" src="https://chart.googleapis.com/chart?cht=bvg&chs=800x300&chbh=10,1&chxt=x,y&chdl=Titles|Items|Pages|Names&chd=t:153,113,120|60,70,80|52,60,40|30,50,45&chds=0,153&chxr=0,0,153|1,0,153&chxl=0:|Jan%2009|Feb%2009|Mar%2009&chf=&chco=2c50f2,ffcc00,99cc00,ff0000" />
    </div>
    <br /><br />

    <span class="tableHeader">Name Additions By Month</span><br />
    <div style="overflow:auto;height:175px;width:910px;border-style:solid;border-color:Black;border-width:1px;text-align:center">
        <br /><img ID="imgMonthlyNames" runat="server" src="https://chart.googleapis.com/chart?cht=bvg&chs=800x300&chbh=10,1&chxt=x,y&chdl=Titles|Items|Pages|Names&chd=t:153,113,120|60,70,80|52,60,40|30,50,45&chds=0,153&chxr=0,0,153|1,0,153&chxl=0:|Jan%2009|Feb%2009|Mar%2009&chf=&chco=2c50f2,ffcc00,99cc00,ff0000" />
    </div>
    <br /><br />

    <span class="tableHeader">Segment Additions By Month</span><br />
    <div style="overflow:auto;height:175px;width:910px;border-style:solid;border-color:Black;border-width:1px;text-align:center">
        <br /><img ID="imgMonthlySegments" runat="server" src="https://chart.googleapis.com/chart?cht=bvg&chs=800x300&chbh=10,1&chxt=x,y&chdl=Titles|Items|Pages|Names&chd=t:153,113,120|60,70,80|52,60,40|30,50,45&chds=0,153&chxr=0,0,153|1,0,153&chxl=0:|Jan%2009|Feb%2009|Mar%2009&chf=&chco=2c50f2,ffcc00,99cc00,ff0000" />
    </div>
    <br /><br />
    <!--
    <span class="tableHeader">Cumulative Totals By Month</span><br />
    <div style="overflow:auto;height:350px;width:910px;border-style:solid;border-color:Black;border-width:1px;text-align:center">
        <br /><img id="imgCumulative" runat="server" src="https://chart.googleapis.com/chart?cht=lc&chs=800x300&chxt=x,y&chdl=Titles|Items|Pages|Names&chd=t:153,266,586|60,130,410|52,112,352|30,80,325&chds=0,586&chxr=0,0,586|1,0,586&chxl=0:|Jan 09|Feb 09|Mar 09&chf=&chco=2c50f2,ffcc00,99cc00,ff0000" />
    </div>
    <br />
    -->
</asp:Content>
