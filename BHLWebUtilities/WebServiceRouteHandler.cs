using System.Web;
using System.Web.Routing;
using System.Web.Services.Protocols;

namespace MOBOT.BHL.Web.Utilities
{
    public class WebServiceRouteHandler : IRouteHandler
    {
        private string _VirtualPath;

        public WebServiceRouteHandler(string virtualPath)
        {
            _VirtualPath = virtualPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new WebServiceHandlerFactory().GetHandler(HttpContext.Current, "*", _VirtualPath, HttpContext.Current.Server.MapPath(_VirtualPath));
        }
    }
}
