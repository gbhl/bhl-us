<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WebResourcePaths.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.WebResourcePaths" 
    ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<a href="/">&lt; Return to Dashboard</a><br />
<br />
<span class="pageHeader">Web Resource Paths</span>
<br /><br />
<div class="box" style="padding:10px; width:900px">
    <asp:Label runat="server" ID="lblMessage" ForeColor="Blue" />
	<p>
        Specify the locations from which web resources (images, PDFs, downloadable ZIPs, and Scandata metadata) are served.
    </p>
    <p>
        For template fields, <span style="font-family:'Courier New'">{barcode}</span> and <span style="font-family:'Courier New'">{fileName}</span> can be used as placeholders for variable parts of a resource path.
    </p>
    <p>
        <p>For example,</p>
        <p style="margin-left:20px; font-family:'Courier New'">https://www.archive.org/download/{barcode}/{fileName}</p>
        <p>is a template for resource paths such as</p>
        <p style="margin-left:20px; font-family:'Courier New'">https://www.archive.org/download/beytragezurnatuv117klee/beytragezurnatuv117klee.pdf</p>
    </p>
    <div style="margin:30px">
        <p>
            <label for="txtImageBaseUrl" style="display:inline-block; width:25%;">Image Base URL:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtImageBaseUrl" Width="550"></asp:TextBox>
        </p>
        <p>
            <label for="txtImageZipPath" style="display:inline-block; width:25%;">Image ZIP Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtImageZipPath" Width="550"></asp:TextBox>
        </p>
        <p>
            <label for="txtPdfPath" style="display:inline-block; width:25%;">PDF Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPdfPath" Width="550"></asp:TextBox>
        </p>
        <p>
            <label for="txtPreLocalScandataPath" style="display:inline-block; width:25%;">Pre-Local Scandata Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPreLocalScandataPath" Width="550"></asp:TextBox>
            <br />
            <span style="display:inline-block;width:25%">&nbsp;</span>
            <span>Book reader will read this location first, before falling back to a local file.</span>
        </p>
        <p>
            <label for="txtPostLocalScandataPath" style="display:inline-block; width:25%;">Pre-Local Scandata Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPostLocalScandataPath" Width="550"></asp:TextBox>
            <br />
            <span style="display:inline-block;width:25%">&nbsp;</span>
            <span>Book reader will read the local file first, before falling back to this location.</span>
        </p>
        <p>
            <label for="txtDjvuPath" style="display:inline-block; width:25%;">Djvu Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtDjvuPath" Width="550"></asp:TextBox>
        </p>
    </div>
    <br />
	<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
    <input type="button" onclick="fillIADefault();" id="btnIADefault" value="Use IA Defaults" />
    <input type="button" onclick="fillAWSDefault();" id="btnAWSDefault" value="Use AWS Defaults" />
</div>
<script type="text/javascript">
    function fillIADefault() {
        txtImageBaseUrl.value = "https://archive.org";
        txtImageZipPath.value = "https://archive.org/download/{barcode}/{fileName}";
        txtPdfPath.value = "https://archive.org/download/{barcode}/{fileName}";
        txtPreLocalScandataPath.value = "";
        txtPostLocalScandataPath.value = "https://archive.org/download/{barcode}/{fileName}";
        txtDjvuPath.value = "https://archive.org/download/{barcode}/{fileName}";
    }

    function fillAWSDefault() {
        txtImageBaseUrl.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com";
        txtImageZipPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/images/{barcode}/{fileName}";
        txtPdfPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/pdf/{fileName}";
        txtPreLocalScandataPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/scandata/{barcode}_scandata.xml";
        txtPostLocalScandataPath.value = "https://archive.org/download/{barcode}/{fileName}";
        txtDjvuPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/web/{barcode}/{fileName}";
    }
</script>

</asp:Content>
