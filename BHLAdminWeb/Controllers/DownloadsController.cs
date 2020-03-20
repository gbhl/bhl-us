using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult ExternalContent()
        {
            BHLProvider provider = new BHLProvider();
            List<Tuple<string, string, string>> links = provider.LinkSelectToExternalContent();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"Entity\",\"ID\",\"Title/Name\",\"External URL\"");
            foreach (Tuple<string, string, string> link in links)
            {
                sb.AppendLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                    link.Item1.Replace("\"", "\"\""),
                    link.Item2.Split('|')[0].Replace("\"", "\"\""),
                    link.Item2.Split('|')[1].Replace("\"", "\"\""),
                    link.Item3.Replace("\"", "\"\"")));
            }

            byte[] csvData = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.Charset = "utf-8";
            return File(csvData, "text/csv", string.Format("LinksToExternalContent{0}.csv", date));
        }
    }
}