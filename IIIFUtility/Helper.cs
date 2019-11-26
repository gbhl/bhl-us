﻿using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace BHL.IIIF
{
    class Helper
    {
        public ScanData GetScanData(int itemId, string barCode)
        {
            Item item = new BHLProvider().ItemSelectFilenames(itemId);
            if (string.IsNullOrWhiteSpace(item.ScandataFilename)) item.ScandataFilename = barCode + "_scandata.xml";

            WebClient wc = new WebClient();
            XmlDocument xml = new XmlDocument();
            xml.Load(wc.OpenRead(string.Format("https://www.archive.org/download/{0}/{1}", barCode, item.ScandataFilename)));

            String nsPrefix = String.Empty;
            XmlNamespaceManager nsmgr = null;
            XmlNodeList pages = xml.SelectNodes("book/pageData/page");

            // If we didn't find any pages in the file, try again... after first
            // adding a namespace to the XML document
            if (pages.Count == 0)
            {
                nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ns", "http://archive.org/scribe/xml");
                nsPrefix = "ns:";
                pages = xml.SelectNodes(nsPrefix + "book/" + nsPrefix + "pageData/" + nsPrefix + "page", nsmgr);
            }

            ScanData scanData = new ScanData();
            int sequence = 1;
            int displaySequence = 1;
            foreach (XmlNode page in pages)
            {
                if (sequence == 1) if (page.Attributes["leafNum"] != null) scanData.StartLeafNumber = Convert.ToInt32(page.Attributes["leafNum"].Value);

                PageScanData pageScanData = new PageScanData();
                pageScanData.Sequence = sequence;
                XmlNode addToAccessFormatsNode = page.SelectSingleNode(nsPrefix + "addToAccessFormats", nsmgr);
                if (addToAccessFormatsNode != null) pageScanData.Display = Convert.ToBoolean(addToAccessFormatsNode.InnerText);
                XmlNode heightNode = page.SelectSingleNode(nsPrefix + "origHeight", nsmgr);
                if (heightNode != null) pageScanData.Height = Convert.ToInt32(heightNode.InnerText);
                XmlNode widthNode = page.SelectSingleNode(nsPrefix + "origWidth", nsmgr);
                if (widthNode != null) pageScanData.Width = Convert.ToInt32(widthNode.InnerText);

                if (pageScanData.Display)
                {
                    scanData.ShowPageCount++;
                    pageScanData.DisplaySequence = displaySequence;
                    displaySequence++;
                }
                else
                {
                    scanData.HidePageCount++;
                }
                scanData.Pages.Add(pageScanData);
                sequence++;
            }

            return scanData;
        }
    }

    class ScanData
    {
        public ScanData()
        {
            ShowPageCount = 0;
            HidePageCount = 0;
            StartLeafNumber = 0;
            Pages = new List<PageScanData>();
        }

        public int ShowPageCount { get; set; }
        public int HidePageCount { get; set; }
        public int StartLeafNumber { get; set; }
        public List<PageScanData> Pages { get; set; }

        public PageScanData GetScanDataForDisplaySequence(int displaySequence)
        {
            PageScanData scanData = null;

            foreach (PageScanData pageScanData in Pages)
            {
                if (pageScanData.DisplaySequence == displaySequence) { scanData = pageScanData; break; }
            }

            return scanData;
        }
    }

    class PageScanData
    {
        public PageScanData()
        {
            DisplaySequence = null;
            Display = false;
            Height = 800;
            Width = 600;
        }

        public int Sequence { get; set; }
        public int? DisplaySequence { get; set; }
        public bool Display { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
