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
    public class ProductsController : Controller
    {
        private mydbEntities db = new mydbEntities();
       private List<Product> list;  // We use tis list in method Add_to_List()

        // GET: Products
        [OutputCache(Duration = 1800, VaryByParam ="none")]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        [OutputCache(Duration = 600, VaryByParam="id")]
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
        // Products/Choose
        [OutputCache(Duration = 600, VaryByParam = "id")]
        public ActionResult Choose(int? id)
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
   
        // Products/Add_To_Cart
        [OutputCache(Duration = 600, VaryByParam = "id")]
        public ActionResult Add_to_cart(int? id)
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
        // Products/Add_To_List
        [OutputCache(Duration = 600, VaryByParam = "id")]
        public ActionResult Add_to_list(int? id)
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

            list.Add(product);
            return View(product);
        }

        //[ActionName("Details")]
        // public ActionResult Image()
        //{
        //    Session["image"] = db.Products.Select(item => item.Image).ToList();
        //    Console.WriteLine(Session["image"]);
        //    return View();
        //}

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pno,Name,Color,Size,Pro_For,Description,Department,UnitPrice,Brand,Image")] Product product, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)


            {
                string path = "";
                if (imgFile.FileName.Length > 0)

                {
                    path = "~/Images/" + Path.GetFileName(imgFile.FileName); // path ="~/Images/7" 
                    imgFile.SaveAs(Server.MapPath(path));  //  Save in Folder Images 

                }
                product.Image = path;  // Image ="~/Images/7"    in Model

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }


        //public ActionResult Create([Bind(Include = "Pno,Name,Color,Size,Pro_For,Description,Department,UnitPrice,Brand,Image")] Product product, IEnumerable<HttpPostedFileBase> imgFiles)
        //{
        //    if (ModelState.IsValid)

        //    {

        //        foreach (HttpPostedFileBase Image in imgFiles)
        //        {
        //           Image.SaveAs(Server.MapPath("~/Images/"+Path.GetFileName(Image.FileName)));
        //        }

        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}
        // Products/Download
        //[HttpPost]
        public ActionResult Download(string imageName ="2.jpg" ) // Action Datatybe => FileResult (True)
        {
           
            string path = Server.MapPath("~/Images/");
            string fileName = Path.GetFileName(imageName);

            string Fullpath = Path.Combine(path,fileName);
            return File(Fullpath,"images/jpg");
            //db.Products.Where(item => item.Image == fileName).ToList()
        }
        // GET: Products/Edit/5
        [OutputCache(Duration = 600, VaryByParam = "id")]
        public ActionResult Edit(int? id)
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Image, Name")] Product product , HttpPostedFileBase imgFile)
        {
            
                string path = "";
                if (imgFile.FileName.Length > 0)

                {
                    path = "~/Images/" + Path.GetFileName(imgFile .FileName); // path ="~/Images/7" 
                    imgFile.SaveAs(Server.MapPath(path));  //  Save in Folder Images 

                }
                product.Image = path;  // Image ="~/Images/7"    in Model


                var before = db.Products.AsNoTracking().Where(item => item.Pno == product.Pno).ToList().FirstOrDefault();
                product.Name = before.Name;


                db.Entry(product).State = EntityState.Modified;
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // Products/Count
        public int Count()
        {
           return  db.Products.Count();     
        }
        // Products/Min
        public int Min()
        {
            //db.Products.Min(item => item.Pno).Value;
            return  db.Products.Min(item => item.Pno);
            
        }
        // Products/Max
        public int Max()
        {
            return  db.Products.Max(item => item.Pno);

          
        }
        public ActionResult getManyRecords()
        {

            //var records = db.Products.Where(item=>item.Pno >= 1).ToList();
            var records = db.Products.Where(x => x.Pno >= 1).ToList().OrderBy(x => x.Name);
            //var records = db.Products.Where(x => x.Name == "T-Shirt").ToList().OrderByDescending(x => x.Is_Sold);
            return View(records);
        }
        public ActionResult getOneRecord()
        {
            var record = db.Products.Find(1);
            //var record = db.Products.Single(x=>x.Pno == 1);
            //var record = db.Products.Where(item => item.Pno >= 1).ToList().FirstOrDefault();
            //var record = db.Products.Where(item => item.Pno >= 1).ToList().LastOrDefault();

            return View(record);
        }
        // Products/Login
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Login()
        {
            return View();
        }
        // Products/HomePage
        [OutputCache(Duration =1800, VaryByParam ="none")]
        public ActionResult HomePage()
        {
            var rec = db.Products.ToList();

            return View(rec);
        }
        public ActionResult Test()
        {
            return View();
        }
        // GET:Products/Search
        public ActionResult Search()
        {
            return View();
        }
        // POST:Products/Search
        [HttpPost]
        [OutputCache(Duration = 1500, VaryByParam ="search")]
        public ActionResult Search(string search)
        {
            if( search != null)
            {
                var product = db.Products.Where(item => item.Name.Contains(search)).ToList();
                return View(product);
            }
            string message = "Not Found, please enter correct name";
            return View(message);

        }

        //[HttpPost]  // not work Search()
        //public ActionResult Search([Bind(Include ="Name")]Product product)
        //{
        //    var rec = db.Products.Where(x => x.Name == product.Name).ToList();
        //    var rec1= db.Products.Where(x => x.Name == product.Name).ToList().FirstOrDefault();
        //    if (rec != null)
        //    {
        //        Session["Name"] = rec1.Name;
        //        Session["notFound"] = "Search is not Match";
        //        return View(rec);
        //    }
        //    else
        //    {
        //        //Session["Error"] = "Not Found";
        //        return RedirectToAction("HomePage");
        //    }


        //}

        // Products/Men
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Men()
        {
            var rec = db.Products.Where(x => x.Department == "Men").ToList();
           
            if (rec != null)
            {
                return View(rec);
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        // Products/Women
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Women()
        {
            var rec = db.Products.Where(x => x.Department == "Women").ToList();

            if (rec != null)
            {
                return View(rec);
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        // Products/kids
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Kids()
        {
            var rec = db.Products.Where(x => x.Pro_For == "Kids").ToList();

            if (rec != null)
            {
                return View(rec);
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        // Products/Baby
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Baby()
        {
            var rec = db.Products.Where(x => x.Pro_For == "Baby").ToList();

            if (rec != null)
            {
                return View(rec);
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        // Products/Luggage
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Luggage()
        {
            var rec = db.Products.Where(x => x.Department == "Luggage").ToList();

            if (rec != null)
            {
                return View(rec);
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        // products/Sports
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Sports()
        {
            var rec = db.Products.Where(x => x.Department == "Sport").ToList();

            if (rec != null)
            {
                return View(rec);
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        // Produts/Kids_Baby
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Kids_Baby()
        {
            var rec = db.Products.Where(x => x.Department == "Kids&Baby").ToList();

            if (rec != null)
            {
                return View(rec);
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        // Products/Price
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Price()
        {
            return RedirectToAction("HomePage");
        }
        // Products/Buy
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Buy()
        {
            var rec = db.Products.ToList();
            return View(rec);
        }
       
        //[HttpPost]
        //public ActionResult Buy(int imageId)
        //{
        //    var rec = db.Products.Where(item =>item.Pno == imageId).ToList().FirstOrDefault();
        //    var ImageName = db.Products.Find(rec.Image);
        //    return View(ImageName);
        //}
    }

    
}
