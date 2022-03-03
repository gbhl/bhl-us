using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FredCK.FCKeditorV2;
using MOBOT.FileAccess;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class AlertEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAlertMessage.ToolbarSet = "BHL";

                string fileName = ConfigurationManager.AppSettings["AlertMsgPath"];
                if (System.IO.File.Exists(fileName))
                {
                    IFileAccessProvider fileAccessProvider = new BHLProvider().GetFileAccessProvider();
                    string alertMessage = fileAccessProvider.GetFileText(fileName);
                    txtAlertMessage.Value = alertMessage;
                }
            }

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                String alertMessage = txtAlertMessage.Value;

                // If the message is enclosed by <p> tags, and there are no other <p>
                // tags within the message, then replace the <p> tags.
                if ((alertMessage.LastIndexOf("<p>") == 0) && 
                    (alertMessage.IndexOf("</p>") == alertMessage.Length - 4))
                {
                    alertMessage = alertMessage.Replace("<p>", "");
                    alertMessage = alertMessage.Replace("</p>", "");
                }

                string fileName = ConfigurationManager.AppSettings["AlertMsgPath"];
                IFileAccessProvider fileAccessProvider = new BHLProvider().GetFileAccessProvider();
                fileAccessProvider.SaveFile(new UTF8Encoding().GetBytes(alertMessage), fileName);
            }
            else
            {
                return;
            }

            Response.Redirect("/");
        }

        protected void clearButton_Click(object sender, EventArgs e)
        {
            clearForm(this.Controls);
        }

        private void clearForm(ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = (TextBox)c;
                    textBox.Text = "";
                }
                else if (c.HasControls())
                {
                    clearForm(c.Controls);
                }
                else if (c is FCKeditor)
                {
                    FCKeditor fck = (FCKeditor)c;
                    fck.Value = "";
                }
            }
        }

        private bool validate()
        {
            bool flag = false;
            return !flag;
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            txtAlertMessage.Value = linkButton.Text;
        }
    }
}
