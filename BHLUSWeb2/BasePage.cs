using System;
using System.Web.UI;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.Web2
{
    public class BasePage : Page
    {
        protected BHLProvider bhlProvider;
        protected MasterPage main;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            bhlProvider = new BHLProvider();
            main = Page.Master;
        }
    }
}