using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class BiblioSelect : BasePage
    {

        protected CustomGenericList<Title> TitleList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                int itemId;
                String itemIdString = (string)RouteData.Values["itemid"];
                TitleList = new CustomGenericList<Title>();

                if (int.TryParse(itemIdString, out itemId))
                {
                    TitleList = bhlProvider.TitleSelectByItem(itemId);                    
                }
            }
        }
    }
}
