using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BHLCoreDALTest
{
    [TestClass()]
    public class BookDALTest
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

        [TestMethod()]
        public void BookSelectByInstitutionTest()
        {
            BookDAL target = new BookDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MBLWHOI";
            int returnCount = 1;
            string sortBy = "Date";
            List<Book> actual = target.BookSelectByInstitution(sqlConnection, sqlTransaction, institutionCode, returnCount, sortBy);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void BookSelectByBarcodeOrItemIDTest()
        {
            BookDAL target = new BookDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string barcode = "journalofmicrosc07post";
            Book actual = target.BookSelectByBarCodeOrItemID(sqlConnection, sqlTransaction, null, barcode);
            Assert.IsTrue(actual.BarCode == barcode);
        }

        [TestMethod]
        public void BookSelectByCollectionTest()
        {
            BookDAL target = new BookDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int collectionID = 12;
            List<Book> actual = target.BookSelectByCollection(sqlConnection, sqlTransaction, collectionID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void BookSelectByTitleIDTest()
        {
            BookDAL target = new BookDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 4000;
            List<Book> actual = target.BookSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void BookSelectRecentTest()
        {
            BookDAL target = new BookDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int top = 100;
            string languageCode = string.Empty;
            string institutionCode = string.Empty;
            List<Book> actual = target.BookSelectRecent(sqlConnection, sqlTransaction, top, languageCode, institutionCode);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void BookSelectRecentlyChangedTest()
        {
            BookDAL target = new BookDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string startDate = "1/1/2008";
            List<Book> actual = target.BookSelectRecentlyChanged(sqlConnection, sqlTransaction, startDate);
            Assert.IsNotNull(actual);
        }
    }
}
