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
    public class ManagersController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: Managers
        public ActionResult Index()
        {   if (Session["Username"] != null)
                return View(db.Managers.ToList());
            else
                return RedirectToAction("Login");
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            if (Session["Username"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mid,Fname,Lname,Phone,Address,Email,Password,Country,BirthDate")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(manager);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Email,Password,Country,BirthDate,ConfEmail,ConfPassword")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                var before = db.Managers.AsNoTracking().Where(x => x.Mid == manager.Mid).ToList().FirstOrDefault();
                manager.Email = before.Email;
                manager.Password = before.Password;
                manager.Country = before.Country;
                manager.BirthDate = before.BirthDate;
                //manager.ConfEmail = before.Email;
                //manager.ConfPassword = before.Email;
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manager);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
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

            //var recs = db.Managers.Where(item=>item.Mid >= 1).ToList();
            var records = db.Managers.Where(x => x.Mid >= 1).ToList().OrderBy(x => x.Fname);
            //var records = db.Managers.Where(x => x.Fname == "Ahmed").ToList();
            return View(records);
        }
        public ActionResult getOneRecord()
        {
            var record = db.Managers.Find(1);
            //var record = db.Managers.Single(x=>x.Mid == 1);
            //var record = db.Managers.Where(item => item.Mid >= 1).ToList().FirstOrDefault();
            //var record = db.Managers.Where(item => item.Mid >= 1).ToList().LastOrDefault();

            return View(record);
        }

        public ActionResult Login()
        {
            return View();
         }
        [HttpPost]
        public ActionResult Login([Bind(Include =("Email,Password"))]Manager manager)
        {
            var rec = db.Managers.Where(x=>x.Email == manager.Email  &&  x.Password == manager.Password ).ToList().FirstOrDefault();
            if (rec != null)
            {
                Session["Username"] = rec.Fname;
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Error = "Invalid User";
                return View(manager);
            }
        }
    }
}
