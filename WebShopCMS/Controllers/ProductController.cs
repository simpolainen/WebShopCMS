using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopCMS.Models;
using WebShopCMS.DataAccess;

namespace WebShopCMS.Controllers
{
    public class ProductController : Controller
    {
        private WebShopDbContext db = new WebShopDbContext();
        private DataAccess<Product> dataAccess = new DataAccess<Product>();

        // GET: /Product/

        public ActionResult Index()
        {
           
            //var o = new Order()
            //{
            //    OrderId = Guid.NewGuid(),
            //    Comment = "fdsf",
            //    Products = db.Products.ToList()
            //};
            //dataAccess.Add(new Product(){ ProductKey = Guid.NewGuid(), Name = "Test" });
            //dataAccess.GetById(Guid.Parse("32071315-217D-47A3-846A-EF50ABC4B9E7"));
            //db.Orders.Add(o);
            //db.SaveChanges();

            //var p = db.Orders.ToList();
            //var a = db.Orders.Include(s => s.Products).ToList();
            dataAccess.FilterObject
                .Create("Product_Price > 1000")
                .Or("Product_Name == Steelseries G795");
            dataAccess.Include = "Orders";
                
            var products = dataAccess.Go();

            dataAccess.FilterObject
                .Create("Product_Price > 200")
                .Or("Product_Name == Steelseries G795");

            var products2 = dataAccess.Go();

            return View(db.Products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductKey = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}