using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace BHLServerTest
{
    [TestClass]
    public class BHLProviderTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void GetFileAccessProviderTest()
        {
            BHLProvider target = new BHLProvider();
            MOBOT.FileAccess.IFileAccessProvider actual;
            actual = target.GetFileAccessProvider(false);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(MOBOT.FileAccess.IFileAccessProvider));
        }

        [TestMethod]
        public void GetOcrTextTest()
        {
            BHLProvider target = new BHLProvider();
            string actual = string.Empty;
            actual = target.GetOcrText(1);
            Assert.AreEqual("OCR text unavailable for this page.", actual);
        }
    
        [TestMethod]
        public void GetTextUrlExistsTest()
        {
            BHLProvider target = new BHLProvider();
            string fileLocation = @"OcrTestFile.txt";
            string actual = string.Empty;
            actual = target.GetTextUrl(false, fileLocation);
            Assert.AreEqual(fileLocation, actual);
        }

        [TestMethod]
        public void GetTextUrlNotExistsTest()
        {
            BHLProvider target = new BHLProvider();
            string fileLocation = @"OcrTestFile2.txt";
            string actual = string.Empty;
            actual = target.GetTextUrl(false, fileLocation);
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void PageGetImageDimensionsTest()
        {
            BHLProvider target = new BHLProvider();

            int itemid = 22010;
            List<BHLProvider.ViewerPage> actual = new List<BHLProvider.ViewerPage>();
            BHLProvider.ViewerPage page = new BHLProvider.ViewerPage();
            page.BarCode = "journalofmicrosc04post";
            page.SequenceOrder = 1;
            page.Height = 0;
            page.Width = 0;
            actual.Add(page);
            actual = target.PageGetImageDimensions(actual, itemid);
            Assert.IsTrue(actual[0].Height > 0);
        }

        [TestMethod]
        public void GetNamesFromOcrTest()
        {
            BHLProvider target = new BHLProvider();

            string resolverName = "GNRD";
            int pageID = 3001717;
            bool useRemoteFileAccessProvider = false;
            bool usePreferredResults = true;
            int maxReadAttempts = 5;
            List<NameFinderResponse> actual = null;
            actual = target.GetNamesFromOcr(resolverName, pageID, useRemoteFileAccessProvider, usePreferredResults, maxReadAttempts);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void GetNameDetailFromGNResolverTest()
        {
            BHLProvider target = new BHLProvider();
            string name = "Zea mays";
            List<GNResolverResponse> actual = null;
            actual = target.GetNameDetailFromGNResolver(name);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void SegmentBibTeXGetCitationStringForSegmentTest()
        {
            BHLProvider target = new BHLProvider();
            int segmentID = 6450;
            string actual = string.Empty;
            actual = target.SegmentBibTeXGetCitationStringForSegmentID(segmentID, false);
            Assert.IsTrue(actual.Length > 0);
        }

        [TestMethod]
        public void SegmentEndNoteGetCitationStringForSegmentTest()
        {
            BHLProvider target = new BHLProvider();
            int segmentID = 6450;
            string actual = string.Empty;
            actual = target.SegmentEndNoteGetCitationStringForSegmentID(segmentID, string.Empty, false);
            Assert.IsTrue(actual.Length > 0);
        }

        [TestMethod]
        public void TitleBibTeXGetCitationStringForTitleIDTest()
        {
            BHLProvider target = new BHLProvider();
            int titleID = 4000;
            string actual = string.Empty;
            actual = target.TitleBibTeXGetCitationStringForTitleID(titleID);
            Assert.IsTrue(actual.Length > 0);
        }

        [TestMethod]
        public void TitleEndNoteGetCitationStringForTitleID()
        {
            BHLProvider target = new BHLProvider();
            int titleID = 4000;
            string actual = string.Empty;
            actual = target.TitleEndNoteGetCitationStringForTitleID(titleID, string.Empty);
            Assert.IsTrue(actual.Length > 0);
        }
    }
}
