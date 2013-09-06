using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace MOBOT.BHL.Web2.Controls
{
    public partial class CollectionListControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CustomGenericList<Collection> collections = new BHLProvider().CollectionSelectActive();
                litNumCollections.Text = collections.Count.ToString();
                rptCollections.DataSource = collections;
                rptCollections.DataBind();
            }
        }
    }
}