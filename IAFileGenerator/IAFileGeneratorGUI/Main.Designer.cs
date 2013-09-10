namespace MOBOT.IAFileGenerator.GUI
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIdentifier = new System.Windows.Forms.TextBox();
            this.txtImageFile = new System.Windows.Forms.TextBox();
            this.txtMetadataFile = new System.Windows.Forms.TextBox();
            this.txtMarcFile = new System.Windows.Forms.TextBox();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.txtAccessKey = new System.Windows.Forms.TextBox();
            this.txtSecretKey = new System.Windows.Forms.TextBox();
            this.lbCollections = new System.Windows.Forms.ListBox();
            this.btnSelectImageFile = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectMetadataFile = new System.Windows.Forms.Button();
            this.btnSelectMarcFile = new System.Windows.Forms.Button();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.progressDC = new System.Windows.Forms.ProgressBar();
            this.progressScandata = new System.Windows.Forms.ProgressBar();
            this.progressMetadata = new System.Windows.Forms.ProgressBar();
            this.lblDC = new System.Windows.Forms.Label();
            this.lblScandata = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(20, 289);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(202, 23);
            this.btnGenerate.TabIndex = 13;
            this.btnGenerate.Text = "Generate Upload Package";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IA Identifier";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "IA Secret Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "IA Access Key";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Collections";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Output Folder";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "MARC File (_marc.xml)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Metadata File (XLS)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Image File (ZIP)";
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.Location = new System.Drawing.Point(133, 16);
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.Size = new System.Drawing.Size(239, 20);
            this.txtIdentifier.TabIndex = 1;
            // 
            // txtImageFile
            // 
            this.txtImageFile.Location = new System.Drawing.Point(133, 53);
            this.txtImageFile.Name = "txtImageFile";
            this.txtImageFile.Size = new System.Drawing.Size(344, 20);
            this.txtImageFile.TabIndex = 2;
            // 
            // txtMetadataFile
            // 
            this.txtMetadataFile.Location = new System.Drawing.Point(133, 81);
            this.txtMetadataFile.Name = "txtMetadataFile";
            this.txtMetadataFile.Size = new System.Drawing.Size(344, 20);
            this.txtMetadataFile.TabIndex = 4;
            // 
            // txtMarcFile
            // 
            this.txtMarcFile.Location = new System.Drawing.Point(133, 109);
            this.txtMarcFile.Name = "txtMarcFile";
            this.txtMarcFile.Size = new System.Drawing.Size(344, 20);
            this.txtMarcFile.TabIndex = 6;
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(133, 137);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(344, 20);
            this.txtOutputFolder.TabIndex = 8;
            // 
            // txtAccessKey
            // 
            this.txtAccessKey.Location = new System.Drawing.Point(391, 181);
            this.txtAccessKey.Name = "txtAccessKey";
            this.txtAccessKey.Size = new System.Drawing.Size(137, 20);
            this.txtAccessKey.TabIndex = 11;
            // 
            // txtSecretKey
            // 
            this.txtSecretKey.Location = new System.Drawing.Point(391, 209);
            this.txtSecretKey.Name = "txtSecretKey";
            this.txtSecretKey.Size = new System.Drawing.Size(137, 20);
            this.txtSecretKey.TabIndex = 12;
            // 
            // lbCollections
            // 
            this.lbCollections.FormattingEnabled = true;
            this.lbCollections.Location = new System.Drawing.Point(133, 177);
            this.lbCollections.Name = "lbCollections";
            this.lbCollections.ScrollAlwaysVisible = true;
            this.lbCollections.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbCollections.Size = new System.Drawing.Size(150, 56);
            this.lbCollections.Sorted = true;
            this.lbCollections.TabIndex = 10;
            // 
            // btnSelectImageFile
            // 
            this.btnSelectImageFile.Location = new System.Drawing.Point(483, 52);
            this.btnSelectImageFile.Name = "btnSelectImageFile";
            this.btnSelectImageFile.Size = new System.Drawing.Size(45, 20);
            this.btnSelectImageFile.TabIndex = 3;
            this.btnSelectImageFile.Text = "Select";
            this.btnSelectImageFile.UseVisualStyleBackColor = true;
            this.btnSelectImageFile.Click += new System.EventHandler(this.btnSelectImageFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSelectMetadataFile
            // 
            this.btnSelectMetadataFile.Location = new System.Drawing.Point(483, 80);
            this.btnSelectMetadataFile.Name = "btnSelectMetadataFile";
            this.btnSelectMetadataFile.Size = new System.Drawing.Size(45, 20);
            this.btnSelectMetadataFile.TabIndex = 5;
            this.btnSelectMetadataFile.Text = "Select";
            this.btnSelectMetadataFile.UseVisualStyleBackColor = true;
            this.btnSelectMetadataFile.Click += new System.EventHandler(this.btnSelectMetadataFile_Click);
            // 
            // btnSelectMarcFile
            // 
            this.btnSelectMarcFile.Location = new System.Drawing.Point(483, 108);
            this.btnSelectMarcFile.Name = "btnSelectMarcFile";
            this.btnSelectMarcFile.Size = new System.Drawing.Size(45, 20);
            this.btnSelectMarcFile.TabIndex = 7;
            this.btnSelectMarcFile.Text = "Select";
            this.btnSelectMarcFile.UseVisualStyleBackColor = true;
            this.btnSelectMarcFile.Click += new System.EventHandler(this.btnSelectMarcFile_Click);
            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(483, 136);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(45, 20);
            this.btnSelectOutputFolder.TabIndex = 9;
            this.btnSelectOutputFolder.Text = "Select";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
            // 
            // btnViewLog
            // 
            this.btnViewLog.Location = new System.Drawing.Point(326, 289);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(202, 23);
            this.btnViewLog.TabIndex = 14;
            this.btnViewLog.Text = "View Log";
            this.btnViewLog.UseVisualStyleBackColor = true;
            this.btnViewLog.Click += new System.EventHandler(this.btnViewLog_Click);
            // 
            // progressDC
            // 
            this.progressDC.Location = new System.Drawing.Point(102, 242);
            this.progressDC.Name = "progressDC";
            this.progressDC.Size = new System.Drawing.Size(426, 10);
            this.progressDC.TabIndex = 24;
            this.progressDC.Visible = false;
            // 
            // progressScandata
            // 
            this.progressScandata.Location = new System.Drawing.Point(102, 256);
            this.progressScandata.Name = "progressScandata";
            this.progressScandata.Size = new System.Drawing.Size(426, 10);
            this.progressScandata.TabIndex = 25;
            this.progressScandata.Visible = false;
            // 
            // progressMetadata
            // 
            this.progressMetadata.Location = new System.Drawing.Point(102, 270);
            this.progressMetadata.Name = "progressMetadata";
            this.progressMetadata.Size = new System.Drawing.Size(426, 10);
            this.progressMetadata.TabIndex = 26;
            this.progressMetadata.Visible = false;
            // 
            // lblDC
            // 
            this.lblDC.AutoSize = true;
            this.lblDC.Location = new System.Drawing.Point(21, 239);
            this.lblDC.Name = "lblDC";
            this.lblDC.Size = new System.Drawing.Size(43, 13);
            this.lblDC.TabIndex = 27;
            this.lblDC.Text = "_dc.xml";
            this.lblDC.Visible = false;
            // 
            // lblScandata
            // 
            this.lblScandata.AutoSize = true;
            this.lblScandata.Location = new System.Drawing.Point(21, 253);
            this.lblScandata.Name = "lblScandata";
            this.lblScandata.Size = new System.Drawing.Size(75, 13);
            this.lblScandata.TabIndex = 28;
            this.lblScandata.Text = "_scandata.xml";
            this.lblScandata.Visible = false;
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.Location = new System.Drawing.Point(21, 267);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(59, 13);
            this.lblUpload.TabIndex = 29;
            this.lblUpload.Text = "_upload.txt";
            this.lblUpload.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 329);
            this.Controls.Add(this.lblUpload);
            this.Controls.Add(this.lblScandata);
            this.Controls.Add(this.lblDC);
            this.Controls.Add(this.progressMetadata);
            this.Controls.Add(this.progressScandata);
            this.Controls.Add(this.progressDC);
            this.Controls.Add(this.btnViewLog);
            this.Controls.Add(this.btnSelectOutputFolder);
            this.Controls.Add(this.btnSelectMarcFile);
            this.Controls.Add(this.btnSelectMetadataFile);
            this.Controls.Add(this.btnSelectImageFile);
            this.Controls.Add(this.lbCollections);
            this.Controls.Add(this.txtSecretKey);
            this.Controls.Add(this.txtAccessKey);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.txtMarcFile);
            this.Controls.Add(this.txtMetadataFile);
            this.Controls.Add(this.txtImageFile);
            this.Controls.Add(this.txtIdentifier);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Internet Archive Metadata File Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIdentifier;
        private System.Windows.Forms.TextBox txtImageFile;
        private System.Windows.Forms.TextBox txtMetadataFile;
        private System.Windows.Forms.TextBox txtMarcFile;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.TextBox txtAccessKey;
        private System.Windows.Forms.TextBox txtSecretKey;
        private System.Windows.Forms.ListBox lbCollections;
        private System.Windows.Forms.Button btnSelectImageFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSelectMetadataFile;
        private System.Windows.Forms.Button btnSelectMarcFile;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Button btnViewLog;
        private System.Windows.Forms.ProgressBar progressDC;
        private System.Windows.Forms.ProgressBar progressScandata;
        private System.Windows.Forms.ProgressBar progressMetadata;
        private System.Windows.Forms.Label lblDC;
        private System.Windows.Forms.Label lblScandata;
        private System.Windows.Forms.Label lblUpload;
    }
}

