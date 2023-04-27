using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DODY.Models;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace DODY.Controllers
{
    public class OrdersController :Controller
    {
        // GET: Orders
        private mydbEntities db = new mydbEntities();
        
  
        // Orders/Index
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }
        // Orders/Details
        public ActionResult Details(int? id)
        {
       
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Size,Quantity,Color,OrderTime,Total,Pno,Cid")] Order order)
        {
            if (ModelState.IsValid)
            {
                //if (Session["Client_id"] != null)
                {
                    order.Total = 1.0;
                    order.OrderTime = DateTime.Now;
                    //order.OrderTime = DateTime.Now; 
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                   /* return RedirectToAction("HomePage", "Products")*/;
                }
            }

            return View(order);
        }
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Size, Quantity")] Order order , HttpPostedFileBase imgFile)
        {

            var before = db.Orders.AsNoTracking().Where(item => item.Ono == order.Ono).ToList().FirstOrDefault();
            order.Size = before.Size;
            order.Quantity = before.Quantity;

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
    public class ProductsControler: Controller
    {
        private mydbEntities db = new mydbEntities();
        public ActionResult product_Client(int id)
        {
            return View();
        }
    }
}