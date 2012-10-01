﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopCMS.Models;

namespace WebShopCMS.Controllers
{
    public class ProductController : Controller
    {
        private WebShopDbContext db = new WebShopDbContext();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            List<Product> ps = new List<Product>();

            //var o = new Order()
            //{
            //    OrderId = Guid.NewGuid(),
            //    Comment = "fdsf",
            //    Products = db.Products.ToList()

            //};
            
            //db.Orders.Add(o);
            //db.SaveChanges();

            var p = db.Orders.ToList();
            var a = db.Orders.Include(s => s.Products).ToList();
           

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