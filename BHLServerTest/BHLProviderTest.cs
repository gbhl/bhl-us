using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
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
            actual = target.GetFileAccessProvider();
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(MOBOT.FileAccess.IFileAccessProvider));
        }

        [TestMethod]
        public void GetOcrTextTest()
        {
            BHLProvider target = new BHLProvider();
            string actual;
            actual = target.GetOcrText(1);
            Assert.AreEqual("Text unavailable for this page.", actual);
        }
    
        [TestMethod]
        public void GetTextUrlExistsTest()
        {
            BHLProvider target = new BHLProvider();
            string fileLocation = @"OcrTestFile.txt";
            string actual;
            actual = target.GetTextUrl(fileLocation);
            Assert.AreEqual(fileLocation, actual);
        }

        [TestMethod]
        public void GetTextUrlNotExistsTest()
        {
            BHLProvider target = new BHLProvider();
            string fileLocation = @"OcrTestFile2.txt";
            string actual;
            actual = target.GetTextUrl(fileLocation);
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public async void PageGetImageDimensionsTest()
        {
            BHLProvider target = new BHLProvider();

            ItemType itemType = ItemType.Book;
            int itemid = 22010;
            List<BHLProvider.ViewerPage> actual = new List<BHLProvider.ViewerPage>();
            BHLProvider.ViewerPage page = new BHLProvider.ViewerPage
            {
                BarCode = "journalofmicrosc04post",
                SequenceOrder = 1,
                Height = 0,
                Width = 0
            };
            actual.Add(page);
            actual = await target.PageGetImageDimensions(actual, itemType, itemid);
            Assert.IsTrue(actual[0].Height > 0);
        }

        [TestMethod]
        public void GetNamesFromOcrTest()
        {
            BHLProvider target = new BHLProvider();

            int pageID = 3001717;
            List<NameFinderResponse> actual;
            actual = target.GetNamesFromOcr(pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void GetNameDetailFromGNVerifierTest()
        {
            BHLProvider target = new BHLProvider();
            string name = "Zea mays";
            List<GNVerifierResponse> actual;
            actual = target.GetNameDetailFromGNVerifier(name);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void SegmentBibTeXGetCitationStringForSegmentTest()
        {
            BHLProvider target = new BHLProvider();
            int segmentID = 6450;
            string actual;
            actual = target.SegmentBibTeXGetCitationStringForSegmentID(segmentID, null, false);
            Assert.IsTrue(actual.Length > 0);
        }

        [TestMethod]
        public void TitleBibTeXGetCitationStringForTitleIDTest()
        {
            BHLProvider target = new BHLProvider();
            int titleID = 4000;
            string actual;
            actual = target.TitleBibTeXGetCitationStringForTitleID(titleID);
            Assert.IsTrue(actual.Length > 0);
        }
    }
}
