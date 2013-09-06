using BHLApi3Web.Controllers;
using BHLApi3Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Net.Http;
using System.Web.Http;

namespace BHLApi3WebTest
{
    /// <summary>
    ///This is a test class for TitlesControllerTest and is intended
    ///to contain all TitlesControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TitlesControllerTest
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

        private static void SetupControllerForTests(ApiController controller, string requestUri, string routeName, string routeTemplate)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var route = config.Routes.MapHttpRoute(routeName, routeTemplate);
            var routeData = new System.Web.Http.Routing.HttpRouteData(route, new System.Web.Http.Routing.HttpRouteValueDictionary { { "controller", "titles" } });
            request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.ControllerContext = new System.Web.Http.Controllers.HttpControllerContext(config, routeData, request);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void Get_ReturnOKForValidID()
        {
            var controller = new TitlesController();
            int id = 1000;
            System.Net.HttpStatusCode expected = System.Net.HttpStatusCode.OK;
            HttpResponseMessage actual;

            SetupControllerForTests(controller, "http://localhost:56736/titles/1000", "Default", "{controller}/{id}");

            actual = controller.Get(id);
            Assert.AreEqual(expected, actual.StatusCode);
        }

        /// <summary>
        /// Test Get for Not Found
        /// </summary>
        [TestMethod]
        public void Get_ReturnNotFoundForBadID()
        {
            var controller = new TitlesController();
            int id = 0;
            System.Net.HttpStatusCode expected = System.Net.HttpStatusCode.NotFound;
            HttpResponseMessage actual;

            SetupControllerForTests(controller, "http://localhost:56736/titles/1000", "Default", "{controller}/{id}");

            actual = controller.Get(id);
            Assert.AreEqual(expected, actual.StatusCode);
        }

        /// <summary>
        ///A test for Get Search Since/Until
        ///</summary>
        [TestMethod()]
        public void Get_SearchReturnsOKForValidDateRange()
        {
            TitlesController controller = new TitlesController();
            string since = "2013-07-01";
            string until = "2013-09-01";
            System.Net.HttpStatusCode expected = System.Net.HttpStatusCode.OK;
            HttpResponseMessage actual;

            SetupControllerForTests(controller, "http://localhost:56736/titles/search", "resource search", "{controller}/search");

            actual = controller.Get(since, until);
            Assert.AreEqual(expected, actual.StatusCode);
        }

        /// <summary>
        /// Test Get Search Since/Until for Not Found
        /// </summary>
        [TestMethod]
        public void Get_SearchReturnsNotFoundWhenNoResults()
        {
            TitlesController controller = new TitlesController();
            string since = "2013-07-01";
            string until = "2013-07-01";
            System.Net.HttpStatusCode expected = System.Net.HttpStatusCode.NotFound;
            HttpResponseMessage actual;

            SetupControllerForTests(controller, "http://localhost:56736/titles/search", "resource search", "{controller}/search");

            actual = controller.Get(since, until);
            Assert.AreEqual(expected, actual.StatusCode);
        }
    }
}
