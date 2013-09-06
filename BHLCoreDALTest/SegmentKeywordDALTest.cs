﻿using MOBOT.BHL.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace BHLCoreDALTest
{
    
    
    /// <summary>
    ///This is a test class for SegmentKeywordDALTest and is intended
    ///to contain all SegmentKeywordDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SegmentKeywordDALTest
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
        ///A test for SegmentKeywordSelectBySegmentID
        ///</summary>
        [TestMethod()]
        public void SegmentKeywordSelectBySegmentIDTest()
        {
            SegmentKeywordDAL target = new SegmentKeywordDAL();
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            int segmentID = 970;
            CustomGenericList<SegmentKeyword> actual = target.SegmentKeywordSelectBySegmentID(sqlConnection, sqlTransaction, segmentID);
            Assert.IsTrue(actual.Count == 0);
        }
    }
}
