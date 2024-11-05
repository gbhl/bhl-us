using MOBOT.BHL.Server;
using System;

namespace MOBOT.BHL.AdminWeb
{
    public partial class SegmentGenreAbout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BHLProvider service = new BHLProvider();
                dlType.DataSource = service.SegmentGenreSelectAll();
                dlType.DataBind();
            }
        }
    }
}