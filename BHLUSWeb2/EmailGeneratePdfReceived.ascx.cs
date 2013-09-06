using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace MOBOT.BHL.Web2
{
    public partial class EmailGeneratePdfReceived : System.Web.UI.UserControl
    {
        public int PdfID { get; set; }

        internal string RenderControlToString()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            this.RenderControl(htw);

            return sb.ToString();
        }
    }
}