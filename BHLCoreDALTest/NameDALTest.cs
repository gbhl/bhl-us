using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    
    
    /// <summary>
    ///This is a test class for NameDALTest and is intended
    ///to contain all NameDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NameDALTest
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
        ///A test for NameSelectByNameString
        ///</summary>
        [TestMethod()]
        public void NameSelectByNameStringTest()
        {
            NameDAL target = new NameDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "zea mays";
            CustomGenericList<Name> actual = target.NameSelectByNameString(sqlConnection, sqlTransaction, name);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for NameSelectByNameID
        ///</summary>
        [TestMethod()]
        public void NameSelectByNameIDTest()
        {
            NameDAL target = new NameDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int nameId = 4378278;
            Name actual = target.NameSelectByNameID(sqlConnection, sqlTransaction, nameId);
            Assert.AreEqual(actual.NameString, "Mollusca");
        }

        /// <summary>
        ///A test for NameSelectByNameStringExact
        ///</summary>
        [TestMethod()]
        public void NameSelectByNameStringExactTest()
        {
            NameDAL target = new NameDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string name = "Mollusca";
            Name actual = target.NameSelectByNameStringExact(sqlConnection, sqlTransaction, name);
            Assert.IsNotNull(actual);
        }
    }
}
