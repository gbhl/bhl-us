using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    /// <summary>
    ///This is a test class for ItemDALTest and is intended
    ///to contain all ItemDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ItemDALTest
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


        /// <summary>
        ///A test for ItemSelectWithExpiredPageNames
        ///</summary>
        [TestMethod()]
        public void ItemSelectWithExpiredPageNamesTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int maxAge = 0;
            CustomGenericList<Item> actual;
            actual = target.ItemSelectWithExpiredPageNames(sqlConnection, sqlTransaction, maxAge);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for ItemSelectWithoutPageNames
        ///</summary>
        [TestMethod()]
        public void ItemSelectWithoutPageNamesTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<Item> actual;
            actual = target.ItemSelectWithoutPageNames(sqlConnection, sqlTransaction);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for ItemSelectByInstitution
        ///</summary>
        [TestMethod()]
        public void ItemSelectByInstitutionTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MO";
            int returnCount = 1;
            string sortBy = "Date";
            CustomGenericList<Item> actual = target.ItemSelectByInstitution(sqlConnection, sqlTransaction, institutionCode, returnCount, sortBy);
            Assert.AreEqual(actual.Count, 1);
        }

        [TestMethod]
        public void ItemCountByInstitutionTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MO";
            int actual = target.ItemCountByInstitution(sqlConnection, sqlTransaction, institutionCode);
            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public void ItemSelectByBarcodeTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string barcode = "journalofmicrosc07post";
            Item actual = target.ItemSelectByBarCode(sqlConnection, sqlTransaction, barcode);
            Assert.IsTrue(actual.BarCode == barcode);
        }

        [TestMethod]
        public void ItemSelectByBarcodeOrItemIDTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string barcode = "journalofmicrosc07post";
            Item actual = target.ItemSelectByBarCodeOrItemID(sqlConnection, sqlTransaction, null, barcode);
            Assert.IsTrue(actual.BarCode == barcode);
        }

        [TestMethod]
        public void ItemSelectByCollectionTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int collectionID = 12;
            CustomGenericList<Item> actual = target.ItemSelectByCollection(sqlConnection, sqlTransaction, collectionID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void ItemSelectByTitleIDTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 4000;
            CustomGenericList<Item> actual = target.ItemSelectByTitleID(sqlConnection, sqlTransaction, titleID);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void ItemSelectNonMemberMonographTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string dateSince = "1/1/2008";
            int isMember = 1;
            string institutionCode = "MO";
            CustomGenericList<NonMemberMonograph> actual = target.ItemSelectNonMemberMonograph(sqlConnection, sqlTransaction, dateSince, isMember, institutionCode);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ItemSelectRecentTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int top = 100;
            string languageCode = string.Empty;
            string institutionCode = string.Empty;
            CustomGenericList<Item> actual = target.ItemSelectRecent(sqlConnection, sqlTransaction, top, languageCode, institutionCode);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void ItemSelectRecentlyChangedTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string startDate = "1/1/2008";
            CustomGenericList<Item> actual = target.ItemSelectRecentlyChanged(sqlConnection, sqlTransaction, startDate);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ItemSelectWithSuspectCharactersTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = string.Empty;
            int maxAge = 5000;
            CustomGenericList<ItemSuspectCharacter> actual = target.ItemSelectWithSuspectCharacters(sqlConnection, sqlTransaction, institutionCode, maxAge);
            Assert.IsNotNull(actual);
        }
    }
}
