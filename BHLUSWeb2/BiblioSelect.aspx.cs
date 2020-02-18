using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2
{
    public partial class BiblioSelect : BasePage
    {

        protected List<Title> TitleList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                int itemId;
                String itemIdString = (string)RouteData.Values["itemid"];
                TitleList = new List<Title>();

                if (int.TryParse(itemIdString, out itemId))
                {
                    TitleList = bhlProvider.TitleSelectByItem(itemId);                    
                }
            }
        }
    }
}
