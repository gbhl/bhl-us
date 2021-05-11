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

        public ActionResult SegmentsForTitle(int id)
        {
            BHLProvider provider = new BHLProvider();
            List<DataObjects.Segment> segments = provider.SegmentSelectByTitleID(id);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Segment ID\tTitle\tTranslated Title\tItem ID\tVolume\tIssue\tSeries\tDate\tLanguage\tAuthor IDs\tAuthor Names\tStart Page\tEnd Page\tStart Page BHL ID\tEnd Page BHL ID\tAdditional Page IDs\tArticle DOI");
            foreach(DataObjects.Segment segment in segments)
            {
                sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}",
                    segment.SegmentID.ToString(), segment.Title, segment.TranslatedTitle, segment.BookID, segment.Volume, segment.Issue, segment.Series, segment.Date, 
                    segment.LanguageName, segment.AuthorIDs, segment.Authors.Replace('|', ';'), segment.StartPageNumber, segment.EndPageNumber, segment.StartPageID, segment.EndPageID,
                    segment.AdditionalPages, segment.DOIName));
            }

            byte[] tsvData = Encoding.UTF8.GetBytes(sb.ToString());
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.Charset = "utf-8";
            return File(tsvData, "text/tab-separated-values", string.Format("SegmentsForTitle{0}-{1}.txt", id.ToString(), date));
        }
    }
}