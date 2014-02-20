using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public RedirectResult Index()
        {
            return new RedirectResult("~/dashboard.aspx");
        }

        public RedirectResult Login()
        {
            return new RedirectResult("~/login.aspx");
        }

        public RedirectResult Error()
        {
            return new RedirectResult("~/error.aspx");
        }

        public RedirectResult PageNotFound()
        {
            return new RedirectResult("~/pagenotfound.aspx");
        }
    }
}
