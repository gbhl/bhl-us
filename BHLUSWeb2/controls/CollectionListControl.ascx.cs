using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Controls
{
    public partial class CollectionListControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Collection> collections = new BHLProvider().CollectionSelectActive();
                litNumCollections.Text = collections.Count.ToString();
                rptCollections.DataSource = collections;
                rptCollections.DataBind();
            }
        }
    }
}