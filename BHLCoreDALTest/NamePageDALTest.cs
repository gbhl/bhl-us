using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    
    
    /// <summary>
    ///This is a test class for NamePageDALTest and is intended
    ///to contain all NamePageDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NamePageDALTest
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
        ///A test for NamePageSelectByPageID
        ///</summary>
        [TestMethod()]
        public void NamePageSelectByPageIDTest()
        {
            NamePageDAL target = new NamePageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 7000;
            CustomGenericList<NamePage> actual = target.NamePageSelectByPageID(sqlConnection, sqlTransaction, pageID);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for NamePageSelectByPageIDAndNameString
        ///</summary>
        [TestMethod()]
        public void NamePageSelectByPageIDAndNameStringTest()
        {
            NamePageDAL target = new NamePageDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int pageID = 7000;
            string nameString = "Heteropteris";
            NamePage actual = target.NamePageSelectByPageIDAndNameString(sqlConnection, sqlTransaction, pageID, nameString);
            Assert.IsNotNull(actual);
        }
    }
}
