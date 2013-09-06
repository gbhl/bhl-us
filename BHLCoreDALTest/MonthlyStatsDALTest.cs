using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    
    
    /// <summary>
    ///This is a test class for MonthlyStatsDALTest and is intended
    ///to contain all MonthlyStatsDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MonthlyStatsDALTest
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
        ///A test for MonthlyStatsSelectByDateAndInstitution
        ///</summary>
        [TestMethod()]
        public void MonthlyStatsSelectByDateAndInstitutionTest()
        {
            MonthlyStatsDAL target = new MonthlyStatsDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int startYear = 2010;
            int startMonth = 1;
            int endYear = 2010;
            int endMonth = 2;
            string institutionName = string.Empty;
            CustomGenericList<MonthlyStats> actual = target.MonthlyStatsSelectByDateAndInstitution(sqlConnection, sqlTransaction, startYear, startMonth, endYear, endMonth, institutionName);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for MonthlyStatsSelectByStatType
        ///</summary>
        [TestMethod()]
        public void MonthlyStatsSelectByStatTypeTest()
        {
            MonthlyStatsDAL target = new MonthlyStatsDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            string statType = "PageNames Created";
            string institutionName = string.Empty;
            bool showMonthly = false;
            CustomGenericList<MonthlyStats> actual = target.MonthlyStatsSelectByStatType(sqlConnection, sqlTransaction, statType, institutionName, showMonthly);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for MonthlyStatsSelectCurrentMonthSummary
        ///</summary>
        [TestMethod()]
        public void MonthlyStatsSelectCurrentMonthSummaryTest()
        {
            MonthlyStatsDAL target = new MonthlyStatsDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<MonthlyStats> actual = target.MonthlyStatsSelectCurrentMonthSummary(sqlConnection, sqlTransaction);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for MonthlyStatsSelectCurrentYearSummary
        ///</summary>
        [TestMethod()]
        public void MonthlyStatsSelectCurrentYearSummaryTest()
        {
            MonthlyStatsDAL target = new MonthlyStatsDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            CustomGenericList<MonthlyStats> actual = target.MonthlyStatsSelectCurrentYearSummary(sqlConnection, sqlTransaction);
            Assert.IsNotNull(actual);
        }
    }
}
