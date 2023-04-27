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
    public class ClientsController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: Clients/Index
        [OutputCache(Duration =1800, VaryByParam ="none")]
        public ActionResult Index()
        {
            //var clients = db.Clients.Include(c => c.Seller);
            if (Session["UserName"] != null)
            {
                return View(db.Clients.ToList());

                //var ClientRecord = db.Clients.Find(Session["Cid"]);
                //Session["Client_id"] = ClientRecord.Cid;
                //return RedirectToAction("Create", "Orders");
            }
            else
                return View("Login");
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.Sid = new SelectList(db.Sellers, "Sid", "Fname");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cid,Fname,Lname,Phone,Address,Email,Password,Country,Birth_Date")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sid = new SelectList(db.Sellers, "Sid", "Fname", client.Sid);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Phone,Password,ConfEmail,ConfPassword,Birth_Date, Sid")] Client client)
        {
            if (ModelState.IsValid)
            {
                var before = db.Clients.AsNoTracking().Where(item => item.Cid == client.Cid).ToList().FirstOrDefault();
                client.Phone = before.Phone;
                client.Password = before.Password;
                //client.ConfEmail = before.Email;
                //client.ConfPassword = before.Password;
                client.Birth_Date = before.Birth_Date;
                client.Sid = before.Sid;

                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sid = new SelectList(db.Sellers, "Sid", "Fname", client.Sid);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        // Post: Clients/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
        // POST: Clients/Delete/5
            db.Clients.Remove(client);
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

        public ActionResult getOneRecord()
        {
            var record = db.Clients.Find(2);
            //var record = db.Clients.Single(x => x.Cid == 1);
            // var record = db.Clients.Where(x=.x.Cid == 2).ToList().FirstOrDefault();
            // var record = db.Clients.Where(x=.x.Cid == 2).ToList().LastOrDefault();
            return View(record);

        }
        public ActionResult getManyRecords()
        {
            //var record = db.Clients.ToList();
            //var record = db.Clients.Where(x=>x.Cid >= 1).ToList();
            var record = db.Clients.Where(x => x.Cid >= 1).ToList().OrderBy(X => X.Fname);
            return View(record);
        }
        // GET:Clients/Login
        [HttpGet]
        public ActionResult Login()
        {     
            return View();
        }
        // POST: Clients/Login
        [HttpPost]
        public ActionResult Login([Bind(Include ="Email,Password")]Client client)
        {
            var rec = db.Clients.Where(x => x.Email == client.Email && x.Password == client.Password).ToList().FirstOrDefault();
            if(rec != null)
            {
                Session["UserName"] = rec.Fname;
                return RedirectToAction("Index");
             
            }
            else
            {
                ViewBag.Error = "Invalid User";
                return View(client);
            }
           

            
        }
        // GET: Clients/Search
        public ActionResult Search()
        {
            return View();
        }
        // POST: Clients/Search
        [HttpPost]
        public ActionResult Search(int ClientPhone)
        {
            var rec = db.Clients.Where(item => item.Phone == ClientPhone).ToList();
            return View(rec);
        }
    }

}
