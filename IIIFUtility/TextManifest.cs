using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BHL.IIIF
{
    public class TextManifest
    {
        private string _rootUrl = "http://localhost";

        public TextManifest()
        {

        }

        public TextManifest(string url )
        {
            _rootUrl = url;
        }

        public string GetManifest(int itemId, int pageSequence)
        {
            BHLProvider provider = new BHLProvider();
            Item item = provider.ItemSelectAuto(itemId);
            List<Page> pages = provider.PageMetadataSelectByItemID(itemId);
            ScanData scanData = new Helper().GetScanData(itemId, item.BarCode);

            string iiifRootAddress = _rootUrl;

            string manifest =
                "{" +
                  "\"@context\": \"http://iiif.io/api/presentation/2/context.json\"," +
                  "\"@id\": \"" + iiifRootAddress + "/iiif/" + itemId.ToString() + "/text\"," +
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

                        string textUrl = _rootUrl + "/pagetext/" + pages[pageCount].PageID.ToString();
                        StreamReader sr = new StreamReader(new WebClient().OpenRead(textUrl), System.Text.Encoding.Default);
                        int resourceCount = 1;
                        while (sr.Peek() >= 0)
                        {
                            if (resourceCount > 1) resources += ",";
                            string text = sr.ReadLine();
                            resources += GetResource(itemId, leafNum, resourceCount, text);
                            resourceCount++;
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

        private string GetResource(int itemId, int canvasNum, int resourceCount, string text)
        {
            string iiifRootAddress =  _rootUrl + "/iiif/" + itemId.ToString() + "$" + canvasNum.ToString();

            string canvas =
                "{" +
                  "\"@id\": \"" + iiifRootAddress + "/annos/text/t" + resourceCount.ToString() + "\"," +
                  "\"@type\": \"oa:Annotation\"," +
                  "\"motivation\": \"sc:painting\"," +
                  "\"resource\": {" +
                    "\"@type\": \"cnt:ContentAsText\"," +
                    "\"format\": \"text/plain\"," +
                    "\"chars\": \"" + Helper.CleanManifestData(text) + "\"" +
                  "}," +
                  "\"on\": \"" + iiifRootAddress + "/canvas\"" +
                "}";

            return canvas;
        }
    }
}
