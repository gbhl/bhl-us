using BHLApi3Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BHLApi3WebTest
{
    /// <summary>
    ///This is a test class for ResolveControllerTest and is intended
    ///to contain all ResolveControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ResolveControllerTest
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
            var routeData = new System.Web.Http.Routing.HttpRouteData(route, new System.Web.Http.Routing.HttpRouteValueDictionary { { "controller", "resolve" } });
            request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.ControllerContext = new System.Web.Http.Controllers.HttpControllerContext(config, routeData, request);
        }

        /// <summary>
        ///A test for Get Resolve Title/Author/Year
        ///</summary>
        [TestMethod()]
        public void Get_ReturnOKForValidResolution()
        {
            ResolveController controller = new ResolveController();
            string title = "A new Zodariid spider from Panama";
            string authors = "Chickering, A M";
            string year = "1957";
            HttpStatusCode expected = HttpStatusCode.OK;
            HttpResponseMessage actual;

            SetupControllerForTests(controller, "http://localhost:56736/resolve/", "resolver", "{controller}");

            actual = controller.Get(title, authors, year);
            Assert.AreEqual(expected, actual.StatusCode);
        }

        /// <summary>
        /// A test for Get Resolve exception (no document to resolve).
        /// </summary>
        [TestMethod]
        public void Get_ReturnExceptionIfNoDocumentToResolve()
        {
            ResolveController controller = new ResolveController();
            string title = "";
            string authors = "";
            string year = "";

            SetupControllerForTests(controller, "http://localhost:56736/resolve/", "resolver", "{controller}");

            bool wasCorrectExceptionThrown = false;
            string exceptionMessage = string.Empty;
            try
            {
                controller.Get(title, authors, year);
            }
            catch (Exception ex)
            {
                do
                {
                    if (ex.Message == "Please supply a document to resolve.")
                    {
                        wasCorrectExceptionThrown = true;
                        exceptionMessage = ex.Message;
                        break;
                    }
                    ex = ex.InnerException;
                } while (ex != null);
            }
            Assert.IsTrue(wasCorrectExceptionThrown, "Actual exception: " + exceptionMessage);
        }
    }
}
