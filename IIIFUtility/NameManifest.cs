using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;

namespace BHL.IIIF
{
    public class NameManifest
    {
        private string _rootUrl = "http://localhost";

        public NameManifest()
        {

        }

        public NameManifest(string url)
        {
            _rootUrl = url;
        }

        public string GetManifest(int itemId, int pageSequence)
        {
            BHLProvider provider = new BHLProvider();
            Item item = provider.ItemSelectAuto(itemId);
            List<Page> pages = provider.PageMetadataSelectByItemID(itemId);
            ScanData scanData = new Helper().GetScanData(itemId, item.BarCode);

            string manifest =
                "{" +
                  "\"@context\": \"http://iiif.io/api/presentation/2/context.json\"," +
                  "\"@id\": \"" + _rootUrl + "/iiif/" + itemId.ToString() + "/text\"," +
                  "\"@type\": \"sc:AnnotationList\"," +
                  GetResources(itemId, pages, scanData, pageSequence) +
                "}";

            return manifest;
        }

        private string GetResources(int itemId, List<Page> pages, ScanData scanData, int pageSequence)
        {
            string resources = "\"resources\": [";

            int leafNum = scanData.StartLeafNumber;
            int pageCount = 0;
            foreach (PageScanData pageScanData in scanData.Pages)
            {
                if (pageScanData.Display)
                {
                    if (leafNum == pageSequence)
                    {

                        // TODO:  Refactor to make use of hte GetPageNameList() method in the PageSummaryService class
                        List<NameResolved> names = new BHLProvider().NameResolvedSelectByPageID(pages[pageCount].PageID);

                        int resourceCount = 1;
                        foreach(NameResolved name in names)
                        {
                            if (!string.IsNullOrEmpty(name.ResolvedNameString))
                            {
                                if (resourceCount > 1) resources += ",";

                                name.UrlName = name.ResolvedNameString.Replace(' ', '_').Replace('.', '$').Replace('?', '^').Replace('&', '~');
                                string nameString = string.Format("<a href='/name/{0}'>{1}</a>", Helper.CleanManifestData(name.UrlName), Helper.CleanManifestData(name.ResolvedNameString));
                                if (!string.IsNullOrWhiteSpace(name.EOLID)) nameString += " <a href='http://www.eol.org/pages/" + name.EOLID + "'><img src='/images/eol_11px.png'></a>";
                                resources += GetResource(itemId, leafNum, resourceCount, nameString);

                                resourceCount++;
                            }
                        }
                        break;
                    }
                    pageCount++;
                }
                leafNum++;
            }

            resources += "]";

            return resources;
        }

        private string GetResource(int itemId, int canvasNum, int resourceCount, string name)
        {
            string iiifRootAddress = _rootUrl + "/iiif/" + itemId.ToString() + "$" + canvasNum.ToString();

            string canvas =
                "{" +
                  "\"@id\": \"" + iiifRootAddress + "/annos/text/t" + resourceCount.ToString() + "\"," +
                  "\"@type\": \"oa:Annotation\"," +
                  "\"motivation\": \"sc:painting\"," +
                  "\"resource\": {" +
                    "\"@type\": \"cnt:ContentAsText\"," +
                    "\"format\": \"text/plain\"," +
                    "\"chars\": \"" + name + "\"" +
                  "}," +
                  "\"on\": \"" + iiifRootAddress + "/canvas\"" +
                "}";

            return canvas;
        }
    }
}
