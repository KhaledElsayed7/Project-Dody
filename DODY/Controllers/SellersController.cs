using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DODY.Models;

namespace DODY.Controllers
{
    public class SellersController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: Sellers
        [OutputCache(Duration =1800 ,VaryByParam ="none")]
        public ActionResult Index()
        {  if (Session["Username"] != null)
            {
                var sellers = db.Sellers.Include(s => s.Manager);
                return View(sellers.ToList());
            }
            else 
                return RedirectToAction("Login");
        }

        // GET: Sellers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // GET: Sellers/Create
        [OutputCache(Duration = 1800, VaryByParam = "none")]
        public ActionResult Create()
        {  if (Session["Username"] != null)
            {
                ViewBag.Mid = new SelectList(db.Managers, "Mid", "Fname");
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sid,Fname,Lname,Phone,Address,Email,Password,Country,Birth_Date,Mid")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Sellers.Add(seller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Mid = new SelectList(db.Managers, "Mid", "Fname", seller.Mid);
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mid = new SelectList(db.Managers, "Mid", "Fname", seller.Mid);
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sid,Fname,Lname,Phone,Address,Email,Password,Country,Birth_Date,Mid")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Mid = new SelectList(db.Managers, "Mid", "Fname", seller.Mid);
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = db.Sellers.Find(id);
            db.Sellers.Remove(seller);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult getManyRecords()
        {

            //var recs = db.Products.Where(item=>item.Pno >= 1).ToList();
            var records = db.Sellers.Where(x => x.Sid >= 1).ToList().OrderBy(x => x.Fname);
            //var recs = db.Products.Where(x => x.Name == "T-Shirt").ToList().OrderByDescending(x => x.Is_Sold);
            return View(records);
        }
        public ActionResult getOneRecord()
        {
            //var record = db.Sellers.Find(1);
            var record = db.Sellers.Single(x => x.Sid == 1);
            //var record = db.Sellers.Where(item => item.Sid >= 1).ToList().FirstOrDefault();
            //var record = db.Sellers.Where(item => item.Sid >= 1).ToList().LastOrDefault();

            return View(record);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include ="Email,Password")]Seller seller)
        {
            var rec = db.Sellers.Where(x => x.Email == seller.Email && x.Password == seller.Password).ToList().FirstOrDefault();
            if (rec != null)
            {
                Session["Username"] = rec.Fname;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Invalid User";
                return View(seller);
            }
            
        }
        [OutputCache(Duration = 1800, VaryByParam = "none")]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        [OutputCache(Duration = 1800, VaryByParam = "none")]
        public ActionResult Search(int SellerPhone)
        {
            var rec = db.Sellers.Where(item => item.Phone == SellerPhone).ToList();
            return View(rec);
        }
    }
}
