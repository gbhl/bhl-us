using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace MOBOT.BHL.Web.Utilities
{
    public class UrlRewriterFormWriter : HtmlTextWriter
    {
        public UrlRewriterFormWriter(HtmlTextWriter writer) : base(writer)
        {
            base.InnerWriter = writer.InnerWriter;
        }

        public UrlRewriterFormWriter(System.IO.TextWriter writer) : base(writer)
        {
            base.InnerWriter = writer;
        }

        public override void WriteAttribute(string name, string value, bool fEncode)
        {
            if (name == "action")
            {
                HttpContext Context;
                Context = HttpContext.Current;

                if (Context.Items["ActionAlreadyWritten"] == null)
                {
                    value = Context.Request.Url.PathAndQuery;
                    Context.Items["ActionAlreadyWritten"] = true;
                }
            }
            base.WriteAttribute(name, value, fEncode);
        }
    }
}
