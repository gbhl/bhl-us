using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            List<Item> actual;
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
            List<Item> actual;
            actual = target.ItemSelectWithoutPageNames(sqlConnection, sqlTransaction);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ItemCountByInstitutionTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MBLWHOI";
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
        public void ItemSelectNonMemberMonographTest()
        {
            ItemDAL target = new ItemDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string dateSince = "1/1/2008";
            int isMember = 1;
            string institutionCode = "MO";
            List<NonMemberMonograph> actual = target.ItemSelectNonMemberMonograph(sqlConnection, sqlTransaction, dateSince, isMember, institutionCode);
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
            List<ItemSuspectCharacter> actual = target.ItemSelectWithSuspectCharacters(sqlConnection, sqlTransaction, institutionCode, maxAge);
            Assert.IsNotNull(actual);
        }
    }
}
