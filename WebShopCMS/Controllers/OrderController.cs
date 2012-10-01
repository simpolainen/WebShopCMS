using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopCMS.Models;

namespace WebShopCMS.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        private WebShopDbContext db = new WebShopDbContext();
       

        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

    }
}
