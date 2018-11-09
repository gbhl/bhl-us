using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult TitlesWithExternalContent()
        {
            BHLProvider provider = new BHLProvider();
            CustomGenericList<TitleInstitution> titleInstitutions = provider.TitleSelectWithExternalContentProvider();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"Title ID\",\"Title\",\"External Content Provider\",\"External Repository URL\"");
            foreach (TitleInstitution ti in titleInstitutions)
            {
                sb.AppendLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", 
                    ti.TitleID.ToString(),
                    ti.FullTitle.Replace("\"", ""),
                    ti.InstitutionName.Replace("\"", ""),
                    ti.Url.Replace("\"", "")));
            }

            byte[] csvData = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            return File(csvData, "text/csv", string.Format("TitlesWithExternalContent{0}.csv", date));
        }
    }
}