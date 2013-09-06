using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

namespace MOBOT.BHL.Web.Utilities
{
    public class HttpHandlerRouteHandler<T> : IRouteHandler where T : IHttpHandler, new()
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new T();
        }
    }

    public class HttpHandlerRouteHandler : IRouteHandler
    {
        private string _VirtualPath;

        public HttpHandlerRouteHandler(string virtualPath)
        {
            this._VirtualPath = virtualPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(this._VirtualPath, typeof(IHttpHandler));
        }

    }
}
