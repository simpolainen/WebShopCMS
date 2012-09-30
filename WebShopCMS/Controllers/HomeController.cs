using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebShopCMS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/fefe

        public ActionResult Index()
        {
            Membership.CreateUser("Simon", "Simon1");

            return View();
        }

    }
}
