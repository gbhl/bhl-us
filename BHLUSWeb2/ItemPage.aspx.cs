using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class ItemPage : BasePage
    {
        protected Item BhlItem { get; set; }
        protected int ItemID { get; set; }
        protected Title BhlTitle { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            int itemID;
            if (!int.TryParse((string)RouteData.Values["itemid"], out itemID))
            {
                Response.Redirect("~/pagenotfound");
            }
            else
            {
                ItemID = itemID;
            }

            BhlItem = bhlProvider.ItemSelectAuto(ItemID);
            if (BhlItem == null)
            {

                Response.Redirect("~/pagenotfound");
            }
            else
            {

                BhlTitle = bhlProvider.TitleSelect(BhlItem.PrimaryTitleID);
               CustomGenericList<Segment> segments = bhlProvider.SegmentSelectByItemID(ItemID);

               if (!(segments == null))
               {
                   segmentRepeater.DataSource = segments;
                   segmentRepeater.DataBind();
               }
            }




        }
    }
}