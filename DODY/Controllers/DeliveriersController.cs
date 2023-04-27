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

namespace DODY.Controllers
{
    public class DeliveriersController : Controller
    {
        // GET: Deliverier
        private mydbEntities db = new mydbEntities();
        public ActionResult Index()
        {
            return View(db.Deliveriers.ToList());
        }
        public ActionResult Details(int? id)
        {
            //ViewBag["image"] = db.Products.Select(item => item.Image).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Did,Name,Phone,Address,Country,Birth_Date,Image")] Deliverier deliverier, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)


            {
                string path = "";
                if (imgFile.FileName.Length > 0)

                {
                    path = "~/Images/Deliverier_img/" + Path.GetFileName(imgFile.FileName); // path ="~/Images/7" 
                    imgFile.SaveAs(Server.MapPath(path));  //  Save in Folder Images 

                }
                deliverier.Image = path;  // Image ="~/Images/7"    in Model

                db.Deliveriers.Add(deliverier); 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliverier);
        }
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deliverier deliverier = db.Deliveriers.Find(id);
            if (deliverier == null)
            {
                return HttpNotFound();
            }
            return View(deliverier);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Image, Name")] Deliverier deliverier, HttpPostedFileBase imgFile)
        {

            string path = "";
            if (imgFile.FileName.Length > 0)

            {
                path = "~/Images/Deliverier_img/" + Path.GetFileName(imgFile.FileName); // path ="~/Images/7" 
                imgFile.SaveAs(Server.MapPath(path));  //  Save in Folder Images 

            }
            deliverier.Image = path;  // Image ="~/Images/7"    in Model


            var before = db.Deliveriers.AsNoTracking().Where(item => item.Did == deliverier.Did).ToList().FirstOrDefault();
            deliverier.Name = before.Name;


            db.Entry(deliverier).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}