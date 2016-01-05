﻿using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class BioStorSegmentsForItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemId = Request.QueryString["id"] as string;

            if (!IsPostBack)
            {
                BHLImportProvider service = new BHLImportProvider();
                dlSegments.DataSource = service.BSSegmentSelectByItem(Convert.ToInt32(itemId));
                dlSegments.DataBind();
            }
        }
    }
}