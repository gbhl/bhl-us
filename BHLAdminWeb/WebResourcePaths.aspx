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
        For template fields, <span style="font-family:'Courier New'">{barcode}</span>, <span style="font-family:'Courier New'">{fileName}</span>, 
        <span style="font-family:'Courier New'">{itemid}</span>, <span style="font-family:'Courier New'">{pageid}</span>, and 
        <span style="font-family:'Courier New'">{pageseq}</span> can be used as placeholders for variable parts of a resource path.
    </p>
    <p>
        <p>For example,</p>
        <p style="margin-left:20px; font-family:'Courier New'">
            https://www.archive.org/download/{barcode}/{fileName}<br />
            https://bhl-open-data.s3.us-east-2.amazonaws.com/ocr/item-{itemid}/item-{itemid}-{pageid}-{pageseq}.txt
        </p>
        <p>are templates for resource paths such as</p>
        <p style="margin-left:20px; font-family:'Courier New'">
            https://www.archive.org/download/beytragezurnatuv117klee/beytragezurnatuv117klee.pdf<br />
            https://bhl-open-data.s3.us-east-2.amazonaws.com/ocr/item-000007/item-000007-00600314-0001.txt
        </p>
    </p>
    <div style="margin:25px">
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtImageBaseUrl" style="display:inline-block; width:25%;">Image Base URL:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtImageBaseUrl" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillImageBaseDefault('ia');"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
            <button type="button" title="AWS default value" onclick="fillImageBaseDefault('aws');"><img alt="AWS button" src="/images/aws-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtImageZipPath" style="display:inline-block; width:25%;">Image ZIP Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtImageZipPath" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillImageZipDefault('ia');"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
            <button type="button" title="AWS default value" onclick="fillImageZipDefault('aws');"><img alt="AWS button" src="/images/aws-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtPdfPath" style="display:inline-block; width:25%;">PDF Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPdfPath" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillPdfPathDefault('ia');"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
            <button type="button" title="AWS default value" onclick="fillPdfPathDefault('aws');"><img alt="AWS button" src="/images/aws-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtPreLocalScandataPath" style="display:inline-block; width:25%;">Pre-Local Scandata Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPreLocalScandataPath" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillPreLocalScandataDefault('ia');"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
            <button type="button" title="AWS default value" onclick="fillPreLocalScandataDefault('aws');"><img alt="AWS button" src="/images/aws-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div>
            <span style="display:inline-block;width:25%">&nbsp;</span>
            <span>Book reader will read this location first, before falling back to a local file.</span>
        </div>
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtPostLocalScandataPath" style="display:inline-block; width:25%;">Post-Local Scandata Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPostLocalScandataPath" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillPostLocalScandataDefault();"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div>
            <span style="display:inline-block;width:25%">&nbsp;</span>
            <span>Book reader will read the local file first, before falling back to this location.</span>
        </div>
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtDjvuPath" style="display:inline-block; width:25%;">Djvu Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtDjvuPath" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillDjvuPathDefault('ia');"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
            <button type="button" title="AWS default value" onclick="fillDjvuPathDefault('aws');"><img alt="AWS button" src="/images/aws-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtItemTextPath" style="display:inline-block; width:25%;">Item Text Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtItemTextPath" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillItemTextPathDefault('ia');"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
            <button type="button" title="AWS default value" onclick="fillItemTextPathDefault('aws');"><img alt="AWS button" src="/images/aws-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div style="display:flex; gap:2px; margin-top:10px">
            <label for="txtPageTextPath" style="display:inline-block; width:25%;">Page Text Path Template:</label>
            <asp:TextBox runat="server" MaxLength="1000" ClientIDMode="Static" ID="txtPageTextPath" Width="550"></asp:TextBox>
            <button type="button" title="IA default value" onclick="fillPageTextPathDefault('ia');"><img alt="IA button" src="/images/ia-icon-24.png" style="width:16px;height:16px" /></button>
            <button type="button" title="AWS default value" onclick="fillPageTextPathDefault('aws');"><img alt="AWS button" src="/images/aws-icon-24.png" style="width:16px;height:16px" /></button>
        </div>
        <div>
            <span style="display:inline-block;width:25%">&nbsp;</span>
            <span>Leave blank to read page text from local servers.</span>
        </div>
    </div>
    <br />
	<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
    <input type="button" onclick="fillIADefault();" id="btnIADefault" value="Use All IA Defaults" />
    <input type="button" onclick="fillAWSDefault();" id="btnAWSDefault" value="Use All AWS Defaults" />
</div>
<script type="text/javascript">
    function fillIADefault() {
        fillImageBaseDefault('ia');
        fillImageZipDefault('ia');
        fillPdfPathDefault('ia');
        fillPreLocalScandataDefault('ia');
        fillPostLocalScandataDefault();
        fillDjvuPathDefault('ia');
        fillItemTextPathDefault('ia');
        fillPageTextPathDefault('ia');
    }

    function fillAWSDefault() {
        fillImageBaseDefault('aws');
        fillImageZipDefault('aws');
        fillPdfPathDefault('aws');
        fillPreLocalScandataDefault('aws');
        fillPostLocalScandataDefault();
        fillDjvuPathDefault('aws');
        fillItemTextPathDefault('aws');
        fillPageTextPathDefault('aws');
    }

    function fillImageBaseDefault(type) {
        if (type == "ia") txtImageBaseUrl.value = "https://archive.org";
        if (type == "aws") txtImageBaseUrl.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com";
    }

    function fillImageZipDefault(type) {
        if (type == "ia") txtImageZipPath.value = "https://archive.org/download/{barcode}/{fileName}";
        if (type == "aws") txtImageZipPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/images/{barcode}/{fileName}";
    }

    function fillPdfPathDefault(type) {
        if (type == "ia") txtPdfPath.value = "https://archive.org/download/{barcode}/{fileName}";
        if (type == "aws") txtPdfPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/pdf/{fileName}";
    }

    function fillPreLocalScandataDefault(type) {
        if (type == "ia") txtPreLocalScandataPath.value = "";
        if (type == "aws") txtPreLocalScandataPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/scandata/{barcode}_scandata.xml";
    }

    function fillPostLocalScandataDefault() {
        txtPostLocalScandataPath.value = "https://archive.org/download/{barcode}/{fileName}";
    }

    function fillDjvuPathDefault(type) {
        if (type == "ia") txtDjvuPath.value = "https://archive.org/download/{barcode}/{fileName}";
        if (type == "aws") txtDjvuPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/web/{barcode}/{fileName}";
    }

    function fillItemTextPathDefault(type) {
        if (type == "ia") txtItemTextPath.value = "https://archive.org/download/{barcode}/{fileName}";
        if (type == "aws") txtItemTextPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/ocr/item-{itemid}/item-{itemid}.txt";
    }

    function fillPageTextPathDefault(type) {
        if (type == "ia") txtPageTextPath.value = "";
        if (type == "aws") txtPageTextPath.value = "https://bhl-open-data.s3.us-east-2.amazonaws.com/ocr/item-{itemid}/item-{itemid}-{pageid}-{pageseq}.txt";
    }
</script>

</asp:Content>
