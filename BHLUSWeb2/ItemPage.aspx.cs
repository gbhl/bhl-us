﻿using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2
{
    public partial class ItemPage : BasePage
    {
        protected DataObjects.Book BhlBook { get; set; }
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

            BhlBook = bhlProvider.BookSelectByBarcodeOrItemID(ItemID, null);
            if (BhlBook == null)
            {

                Response.Redirect("~/pagenotfound");
            }
            else
            {
                BhlTitle = bhlProvider.TitleSelect((int)BhlBook.PrimaryTitleID);
                List<Segment> segments = bhlProvider.SegmentSelectByBookID(BhlBook.BookID);

               if (!(segments == null))
               {
                   segmentRepeater.DataSource = segments;
                   segmentRepeater.DataBind();
               }
            }
        }
    }
}