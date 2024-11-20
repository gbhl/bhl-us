using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;

namespace MOBOT.BHL.AdminWeb
{
    public partial class WebResourcePaths: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtImageBaseUrl.Text = GetConfigurationValue("ImageBaseURL");
                txtImageZipPath.Text =  GetConfigurationValue("ImageZIPPathTemplate");
                txtPdfPath.Text = GetConfigurationValue("PDFPathTemplate");
                txtScandataPath.Text = GetConfigurationValue("ScandataPathTemplate");
            }
        }

        private string GetConfigurationValue(string configKey)
        {
            string configValue = string.Empty;

            BHLProvider provider = new BHLProvider();
            Configuration configuration = provider.ConfigurationSelectByName(configKey);
            if (configuration != null) configValue = configuration.ConfigurationValue;

            return configValue;
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            BHLProvider provider = new BHLProvider();

            if (provider.ConfigurationSave("ImageBaseURL", txtImageBaseUrl.Text) != null &&
                provider.ConfigurationSave("ImageZIPPathTemplate", txtImageZipPath.Text) != null &&
                provider.ConfigurationSave("PDFPathTemplate", txtPdfPath.Text) != null &&
                provider.ConfigurationSave("ScandataPathTemplate", txtScandataPath.Text) != null )
            {
                lblMessage.Text = "Web Resource Paths updated.";
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblMessage.Text = "Error saving Web Resource Paths.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}