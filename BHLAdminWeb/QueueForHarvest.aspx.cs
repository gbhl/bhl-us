using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHLImport.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class QueueForHarvest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        protected void btnQueue_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                BHLImportProvider service = new BHLImportProvider();

                string[] wsResult = service.IAItemQueueForDownload(txtIdentifier.Text, ConfigurationManager.AppSettings["LocalFileFolder"]);
                lblError.Text = wsResult[1];
                if (wsResult[0] == "true")
                    lblError.ForeColor = System.Drawing.Color.Black;
                else
                    lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
            }
        }

        private bool validate()
        {
            bool flag = false;
            if (txtIdentifier.Text.Trim().Length == 0)
            {
                flag = true;
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please specify an Internet Archive identifier.";
            }
            lblError.Visible = flag;
            return !flag;
        }

    }
}