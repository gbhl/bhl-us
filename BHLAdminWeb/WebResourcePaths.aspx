<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WebResourcePaths.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.WebResourcePaths" 
    ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<a href="/">&lt; Return to Dashboard</a><br />
<br />
<span class="pageHeader">Web Resource Paths</span>
<br /><br />
<div class="box" style="padding:10px; width:800px">
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
            <label for="txtImageBaseUrl" style="display:inline-block; width:20%;">Image Base URL:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtImageBaseUrl" Width="550"></asp:TextBox>
        </p>
        <p>
            <label for="txtImageZipPath" style="display:inline-block; width:20%;">Image ZIP Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtImageZipPath" Width="550"></asp:TextBox>
        </p>
        <p>
            <label for="txtPdfPath" style="display:inline-block; width:20%;">PDF Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPdfPath" Width="550"></asp:TextBox>
        </p>
        <p>
            <label for="txtScandataPath" style="display:inline-block; width:20%;">Scandata Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtScandataPath" Width="550"></asp:TextBox>
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
        txtScandataPath.value = "https://archive.org/download/{barcode}/{fileName}";
    }

    function fillAWSDefault() {
        txtImageBaseUrl.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com";
        txtImageZipPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/images/{barcode}/{fileName}";
        txtPdfPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/pdf/{fileName}";
        txtScandataPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/web/{barcode}/{fileName}";
    }
</script>

</asp:Content>
