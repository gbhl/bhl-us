using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ImageServerEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLProvider provider = new BHLProvider();
                Configuration configuration = provider.ConfigurationSelectByName("ImageBaseURL");
                if (configuration != null)
                {
                    bool itemSelected = false;
                    foreach (ListItem li in rdoListServers.Items)
                    {
                        li.Selected = (li.Value == configuration.ConfigurationValue);
                        itemSelected = (itemSelected || li.Selected);
                    }
                    if (!itemSelected)
                    {
                        rdoListServers.SelectedValue = "Other";
                        txtAddress.Text = configuration.ConfigurationValue;
                        divAddress.Visible = true;
                    }
                }
            }

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                string configurationValue = rdoListServers.SelectedValue;
                if (configurationValue == "Other") configurationValue = txtAddress.Text;

                BHLProvider provider = new BHLProvider();

                // Save the configuration value here
                if (provider.ConfigurationSave("ImageBaseURL", configurationValue) != null)
                {
                    lblMessage.Text = "Image Server updated.";
                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lblMessage.Text = "Error saving Image Server.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                return;
            }
        }

        protected void rdoListServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            divAddress.Visible = (rdoListServers.SelectedValue == "Other");
        }

        private bool validate()
        {
            bool flag = true;

            if (rdoListServers.SelectedValue == "Other" && txtAddress.Text == string.Empty)
            {
                flag = false;
                lblMessage.Text = "Please specify a domain address for the image server.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            return flag;
        }

    }
}