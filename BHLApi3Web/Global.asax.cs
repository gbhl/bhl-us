using System;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BHLApi3Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Authenticate the API calls
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthenticationHandler());

            GlobalConfiguration.Configuration.Routes.MapHttpRoute("resource search", "{controller}/search", defaults: new { since = "01/01/2000", until = DateTime.Now.ToShortDateString() });
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("DefaultGet", "{controller}/{id}", new { id = RouteParameter.Optional } );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("resolver", "{controller}", defaults: new { title = "", authors = "", year = "" });
            GlobalConfiguration.Configuration.Routes.Add("default", new HttpRoute("{controller}"));

            // Use this to suppress NULL values in the JSON stream... fields with NULL values are not returned
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            // Custom generic error handler
            GlobalConfiguration.Configuration.Filters.Add(
                new Filters.UnhandledExceptionFilterAttribute()
                // To add handlers for specific exception types, uncomment and customize the following
                // http://stackoverflow.com/questions/12519561/asp-net-web-api-throw-httpresponseexception-or-return-request-createerrorrespon
                /*
                .Register<System.Collections.Generic.KeyNotFoundException>(System.Net.HttpStatusCode.NotFound)
                .Register<System.Security.SecurityException>(System.Net.HttpStatusCode.Forbidden)
                .Register<System.Data.SqlClient.SqlException>(
                    (exception, request) =>
                    {
                        var sqlException = exception as System.Data.SqlClient.SqlException;
                        if (sqlException.Number > 50000)
                        {
                            //var response = request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                            System.Net.Http.HttpResponseMessage response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                            response.ReasonPhrase = sqlException.Message.Replace(Environment.NewLine, String.Empty);
                            return response;
                        }
                        else
                        {
                            //return request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
                            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                )
                 */
            );
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}