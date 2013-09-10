using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MOBOT.IAFileGenerator.GUI
{
    public partial class Main : Form
    {
        private string _lastFolderSelected = string.Empty;

        public Main()
        {
            InitializeComponent();

            openFileDialog1.Multiselect = false;

            // Read the list of collections and the IA keys from the configuration file
            string collections = ConfigurationManager.AppSettings["Collections"].ToString();
            lbCollections.Items.AddRange(collections.Split('|'));
            txtAccessKey.Text = ConfigurationManager.AppSettings["AccessKey"].ToString();
            txtSecretKey.Text = ConfigurationManager.AppSettings["SecretKey"].ToString();
        }

        #region UI event handlers

        /// <summary>
        /// Validate the inputs and then start a new thread to generate the files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                // Get the selected collections
                List<string> collections = new List<string>();
                foreach (string collection in lbCollections.SelectedItems)
                {
                    collections.Add(collection);
                }

                // Set up the generator
                IAFileGenerator.Generator generator = new Generator(
                    txtIdentifier.Text,
                    collections,
                    txtImageFile.Text,
                    txtMetadataFile.Text,
                    txtMarcFile.Text,
                    txtOutputFolder.Text,
                    txtAccessKey.Text,
                    txtSecretKey.Text);

                // Generate the metadata (use a separate thread so the app doesn't "freeze")
                this.DisableUI();
                Thread thread = new Thread(Generate);
                thread.IsBackground = true;
                thread.Start(generator);
            }
            else
            {
                MessageBox.Show("Error.  Please supply all of the requested information.");
            }
        }

        /// <summary>
        /// Open file dialog to allow the user to select the image ZIP file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectImageFile_Click(object sender, EventArgs e)
        {
            this.SelectInputFile("Zip files (*.zip)|*.zip|All files (*.*)|*.*", txtImageFile);
        }

        /// <summary>
        /// Open file dialog to allow the user to select the Excel file containing the metadata
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectMetadataFile_Click(object sender, EventArgs e)
        {
            this.SelectInputFile("Excel files (*.xls)|*.xls|All files (*.*)|*.*", txtMetadataFile);
        }

        /// <summary>
        /// Open file dialog to allow the user to select a MARCXML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectMarcFile_Click(object sender, EventArgs e)
        {
            this.SelectInputFile("MARC xml files (*_marc.xml)|*_marc.xml|All files (*.*)|*.*", txtMarcFile);
        }

        /// <summary>
        /// Open the file dialog and capture the user response and file selection.  Set the text of the
        /// specified textbox to the user selection and save the selected folder for later use.
        /// </summary>
        /// <param name="filter">Filter to apply to the file dialog</param>
        /// <param name="textBox">Control for which we should set the text</param>
        private void SelectInputFile(string filter, TextBox textBox)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = filter;
            if (openFileDialog1.ShowDialog().ToString() == "OK")
            {
                textBox.Text = openFileDialog1.FileName;
                _lastFolderSelected = System.IO.Path.GetDirectoryName(textBox.Text);
            }
        }

        /// <summary>
        /// Open the folder dialog to allow the user to select the output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            if (_lastFolderSelected != string.Empty) folderBrowserDialog1.SelectedPath = _lastFolderSelected;
            if (folderBrowserDialog1.ShowDialog().ToString() == "OK") txtOutputFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        /// <summary>
        /// Open the dialog window to display the application's log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewLog_Click(object sender, EventArgs e)
        {
            ViewLog viewLog = new ViewLog();
            viewLog.ShowDialog();
        }

        #endregion UI event handlers

        /// <summary>
        /// Make sure that all required inputs have been supplied
        /// </summary>
        /// <returns>True if all required data has been supplied, otherwise false</returns>
        private bool ValidateInput()
        {
            bool valid = true;

            if (txtIdentifier.Text.Trim() == string.Empty) valid = false;
            if (valid) if (txtImageFile.Text.Trim() == string.Empty) valid = false;
            if (valid) if (txtMetadataFile.Text.Trim() == string.Empty) valid = false;
            if (valid) if (txtMarcFile.Text.Trim() == string.Empty) valid = false;
            if (valid) if (txtOutputFolder.Text.Trim() == string.Empty) valid = false;
            if (valid) if (lbCollections.SelectedItems.Count == 0) valid = false;
            if (valid) if (txtAccessKey.Text.Trim() == string.Empty) valid = false;
            if (valid) if (txtSecretKey.Text.Trim() == string.Empty) valid = false;

            return valid;
        }

        /// <summary>
        /// Call the file generator to produce the files.  Report on the success or failure of the process.
        /// </summary>
        /// <param name="data">An instance of the file generator</param>
        private void Generate(object data)
        {
            Generator generator = (Generator) data;

            // Wire up the events so that we can track the progress of the file generation
            generator.DCMetadataStart += new DCMetadataStartEventHandler(this.DCMetadataStart);
            generator.DCMetadataEnd += new DCMetadataEndEventHandler(this.DCMetadataEnd);
            generator.UploadStart += new UploadStartEventHandler(this.UploadStart);
            generator.UploadEnd += new UploadEndEventHandler(this.UploadEnd);
            generator.ScandataStart += new ScandataStartEventHandler(this.ScandataStart);
            generator.ImageProcessed += new ImageProcessedEventHandler(this.ImageProcessed);
            generator.ScandataEnd += new ScandataEndEventHandler(this.ScandataEnd);

            // Generate the files
            if (generator.Generate())
            {
                Invoke(new MethodInvoker(ProcessSuccess));
            }
            else
            {
                Invoke(new MethodInvoker(ProcessFailure));
            }

            // Re-enable the UI
            Invoke(new MethodInvoker(EnableUI));
        }

        #region UI manipulation methods

        private void DisableUI()
        {
            txtIdentifier.Enabled = false;
            txtImageFile.Enabled = false;
            txtMetadataFile.Enabled = false;
            txtMarcFile.Enabled = false;
            txtOutputFolder.Enabled = false;
            lbCollections.Enabled = false;
            txtAccessKey.Enabled = false;
            txtSecretKey.Enabled = false;
            btnGenerate.Enabled = false;
            btnViewLog.Enabled = false;
            btnSelectImageFile.Enabled = false;
            btnSelectMetadataFile.Enabled = false;
            btnSelectMarcFile.Enabled = false;
            btnSelectOutputFolder.Enabled = false;
        }

        void EnableUI()
        {
            txtIdentifier.Enabled = true;
            txtImageFile.Enabled = true;
            txtMetadataFile.Enabled = true;
            txtMarcFile.Enabled = true;
            txtOutputFolder.Enabled = true;
            lbCollections.Enabled = true;
            txtAccessKey.Enabled = true;
            txtSecretKey.Enabled = true;
            btnGenerate.Enabled = true;
            btnViewLog.Enabled = true;
            btnSelectImageFile.Enabled = true;
            btnSelectMetadataFile.Enabled = true;
            btnSelectMarcFile.Enabled = true;
            btnSelectOutputFolder.Enabled = true;
        }

        private void ProcessSuccess()
        {
            MessageBox.Show("Success.  Metadata files created for '" + txtIdentifier.Text + "' in '" + txtOutputFolder.Text + "'");
            txtIdentifier.Text = string.Empty;
            txtImageFile.Text = string.Empty;
            txtMetadataFile.Text = string.Empty;
            txtMarcFile.Text = string.Empty;
            txtOutputFolder.Text = string.Empty;
            lblDC.Visible = false;
            progressDC.Visible = false;
            lblScandata.Visible = false;
            progressScandata.Visible = false;
            lblUpload.Visible = false;
            progressMetadata.Visible = false;
            for (int x = 0; x < lbCollections.Items.Count; x++)
            {
                lbCollections.SetSelected(x, false);
            }
        }

        private void ProcessFailure()
        {
            MessageBox.Show("Error.  View the log for details.");
        }

        private void InitDCMetadata()
        {
            progressDC.Minimum = 0;
            progressDC.Maximum = 1;
            progressDC.Value = 0;
            lblDC.Visible = true;
            progressDC.Visible = true;
        }

        private void EndDCMetadata()
        {
            progressDC.Value = 1;
        }

        private void InitUpload()
        {
            progressMetadata.Minimum = 0;
            progressMetadata.Maximum = 1;
            progressMetadata.Value = 0;
            lblUpload.Visible = true;
            progressMetadata.Visible = true;
        }

        private void EndUpload()
        {
            progressMetadata.Value = 1;
        }

        private void StartScandata()
        {
            progressScandata.Minimum = 0;
            progressScandata.Maximum = 1;
            progressScandata.Value = 0;
            lblScandata.Visible = true;
            progressScandata.Visible = true;
        }

        public delegate void ProcessImageDelegate(int maxImages);
        private void ProcessImage(int maxImages)
        {
            progressScandata.Maximum = maxImages;
            progressScandata.Value++;
        }

        private void EndScandata()
        {
            progressScandata.Value = progressScandata.Maximum;
        }

        #endregion UI manipulation methods

        #region File generation event handlers

        public void DCMetadataStart(object sender, DCMetadataStartEventArgs e)
        {
            Invoke(new MethodInvoker(InitDCMetadata));
        }

        public void DCMetadataEnd(object sencer, DCMetadataEndEventArgs e)
        {
            Invoke(new MethodInvoker(EndDCMetadata));
        }

        public void UploadStart(object sender, UploadStartEventArgs e)
        {
            Invoke(new MethodInvoker(InitUpload));
        }

        public void UploadEnd(object sender, UploadEndEventArgs e)
        {
            Invoke(new MethodInvoker(EndUpload));
        }

        public void ScandataStart(object sender, ScandataStartEventArgs e)
        {
            Invoke(new MethodInvoker(StartScandata));
        }

        public void ImageProcessed(object sender, ImageProcessedEventArgs e)
        {
            BeginInvoke(new ProcessImageDelegate(ProcessImage), new object[]{e.MaxImages});
        }

        public void ScandataEnd(object sender, ScandataEndEventArgs e)
        {
            Invoke(new MethodInvoker(EndScandata));
        }

        #endregion File generation event handlers
    }
}
