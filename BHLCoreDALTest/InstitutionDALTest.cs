using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    [TestClass()]
    public class InstitutionDALTest
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
        public void InstitutionSelectByItemIDTest()
        {
            InstitutionDAL target = new InstitutionDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 22500;
            CustomGenericList<Institution> actual = target.InstitutionSelectByItemID(sqlConnection, sqlTransaction, itemID);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void InstitutionSelectDOIStatsTest()
        {
            InstitutionDAL target = new InstitutionDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int sortBy = 1;
            int bhlOnly = 0;
            CustomGenericList<Institution> actual = target.InstitutionSelectDOIStats(sqlConnection, sqlTransaction, sortBy, bhlOnly);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void InstitutionSelectWithPublishedItemsTest()
        {
            InstitutionDAL target = new InstitutionDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            bool onlyMemberLibraries = false;
            CustomGenericList<Institution> actual = target.InstitutionSelectWithPublishedItems(sqlConnection, sqlTransaction, onlyMemberLibraries);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void InstitutionSelectWithPublishedSegmentsTest()
        {
            InstitutionDAL target = new InstitutionDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            bool onlyMemberLibraries = false;
            CustomGenericList<Institution> actual = target.InstitutionSelectWithPublishedSegments(sqlConnection, sqlTransaction, onlyMemberLibraries);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void InstitutionSelectByItemIDAndRole()
        {
            InstitutionDAL target = new InstitutionDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int itemID = 22004;
            string role = "Contributor";
            CustomGenericList<Institution> actual = target.InstitutionSelectByItemIDAndRole(sqlConnection, sqlTransaction, itemID, role);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void InstitutionSelectBySegmentIDAndRole()
        {
            InstitutionDAL target = new InstitutionDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 6450;
            string role = "Contributor";
            CustomGenericList<Institution> actual = target.InstitutionSelectBySegmentIDAndRole(sqlConnection, sqlTransaction, segmentID, role);
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void InstitutionSelectByTitleIDAndRole()
        {
            InstitutionDAL target = new InstitutionDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int titleID = 2187;
            string role = "Contributor";
            CustomGenericList<Institution> actual = target.InstitutionSelectByTitleIDAndRole(sqlConnection, sqlTransaction, titleID, role);
            Assert.IsTrue(actual.Count > 0);
        }
    }
}
