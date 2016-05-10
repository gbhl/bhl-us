using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    /// <summary>
    ///This is a test class for TitleDALTest and is intended
    ///to contain all TitleDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TitleDALTest
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
        ///A test for TitleBibTeXSelectForTitleID
        ///</summary>
        [TestMethod()]
        public void TitleBibTeXSelectForTitleIDTest()
        {
            TitleDAL target = new TitleDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleId = 3926;
            CustomGenericList<TitleBibTeX> actual;
            actual = target.TitleBibTeXSelectForTitleID(sqlConnection, sqlTransaction, titleId);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for TitleEndNoteSelectForTitleID
        ///</summary>
        [TestMethod()]
        public void TitleEndNoteSelectForTitleIDTest()
        {
            TitleDAL target = new TitleDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleId = 3926;
            CustomGenericList<TitleEndNote> actual;
            actual = target.TitleEndNoteSelectForTitleID(sqlConnection, sqlTransaction, titleId);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod()]
        public void TitleSelectByCollectionTest()
        {
            TitleDAL target = new TitleDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int collectionID = 12;
            CustomGenericList<Title> actual = target.TitleSelectByCollection(sqlConnection, sqlTransaction, collectionID);
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void TitleSelectSearchNameTest()
        {
            TitleDAL target = new TitleDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "mollusca";
            string languageCode = "ENG";
            int returnCount = 10;
            CustomGenericList<Title> actual = target.TitleSelectSearchName(sqlConnection, sqlTransaction, name, languageCode, returnCount);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void TitleSelectWithSuspectCharactersTest()
        {
            TitleDAL target = new TitleDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = string.Empty;
            int maxAge = 5000;
            CustomGenericList<TitleSuspectCharacter> actual = target.TitleSelectWithSuspectCharacters(sqlConnection, sqlTransaction, institutionCode, maxAge);
            Assert.IsNotNull(actual);
        }
    }
}
