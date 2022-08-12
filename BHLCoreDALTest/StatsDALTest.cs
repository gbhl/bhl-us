using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Data.SqlClient;

namespace BHLCoreDALTest
{


    /// <summary>
    ///This is a test class for StatsDALTest and is intended
    ///to contain all StatsDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatsDALTest
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
        ///A test for StatsSelect
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BHLCoreDAL.dll")]
        public void StatsSelectTest()
        {
            StatsDAL target = new StatsDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            Stats actual = target.StatsSelect(sqlConnection, sqlTransaction);
            Assert.IsTrue(actual.NameCount > 0);
        }

        [TestMethod()]
        public void StatsSelectForInstitutionTest()
        {
            StatsDAL target = new StatsDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string institutionCode = "MBLWHOI";
            Stats actual = target.StatsSelectForInstitution(sqlConnection, sqlTransaction, institutionCode);
            Assert.IsTrue(actual.PageCount > 0);
        }
    }
}
