using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using DODY.Models; //Step 1: we add this library


namespace DODY.Controllers
{
    public class JoinController : Controller
    {
        // GET: Join
        public ActionResult Index()
        {
            using (mydbEntities db =new mydbEntities())   //Step 2: we add using 
            {
                List<Product> products = db.Products.ToList();
                List<Client> clients   = db.Clients.ToList();
                List<Order> orders     = db.Orders.ToList();

                var OrderRecord = from o in orders                 // step 2 in join
                               join p in products on o.Pno equals p.Pno into table1
                               from p in table1.ToList()
                               join c in clients on o.Cid equals c.Cid into table2
                               from c in table2.ToList()
                               select  new ViewModel              // step 1 in join
                               {
                                 product=p,
                                 order=o,
                                 client=c
                               };
                return View(OrderRecord);                       // step 3 in join
            }
        }

        private mydbEntities db = new mydbEntities();
        public ActionResult Log_Index()
            {
                if (Session["Cid"] != null)
                {
                var clientRecord = db.Clients.Find(Session["Cid"]);
                Session["Client-id"] = clientRecord.Cid;
                 return RedirectToAction("Create");
                }
                else
                   
                return View(Session["Client-id"]);
           }
            public ActionResult LoginClient()
           {
            return View();
           }
            [HttpPost]
            public ActionResult LoginClient([Bind(Include = "Email,Password")] Client client)
            {
                var rec = db.Clients.Where(x => x.Email == client.Email && x.Password == client.Password).ToList().FirstOrDefault();
                if (rec != null)
                {
                    //Session["Username"] = rec.Fname;
                    Session["Cid"] = rec.Cid;
                    return RedirectToAction("Log_Index");
                }
                else
                {
                    ViewBag.Error = "Invalid User";
                    return View(client);
                }

            }
        public ActionResult Create([Bind(Include = "Ono,Size,Quantity,Color,OrderTime,Total,Pno,Cid")] Order order, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
               
            {
                if (Session["Client-id"] != null)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(order);
        }
        //public ActionResult Search(string search)
        //{
        //    using (mydbEntities db = new mydbEntities())   //Step 2: we add using 
        //    {
        //        List<Product> products = db.Products.ToList();
        //        List<Client> clients = db.Clients.ToList();
        //        List<Order> orders = db.Orders.ToList();

        //        var OrderRecord = from o in orders                 // step 2 in join
        //                          join p in products on o.Pno equals p.Pno into table1
        //                          from p in table1.ToList()
        //                          join c in clients on o.Cid equals c.Cid into table2
        //                          from c in table2.ToList()
        //                          select new ViewModel              // step 1 in join
        //                          {
        //                              product = p,
        //                              order = o,
        //                              client = c
        //                          };
              
        //        return View(OrderRecord);                       // step 3 in join
        //    }
        //    if (search != null)
        //    {
        //        var product = OrderRecord.Where(item => item.Name.Contains(search)).ToList();
        //        return View(product);
        //    }
        //    string message = "Not Found, please enter correct name";
        //    return View(message);

        //}

    }
}
//The following are the design flaws that cause damage in software, mostly. 
//  1. Putting more stress on classes by assigning more responsibilities to them. (A lot of functionality not related to a class.) 
//  2.Forcing the classes to depend on each other. If classes are dependent on each other (in other words tightly coupled)
//    ,then a change in one will affect the other.
//  3. Spreading duplicate code in the system/application. 

//Solution 
//  1. Choosing the correct architecture (in other words MVC, 3-tier, Layered, MVP, MVVP and so on). 
//  2. Following Design Principles. 
//  3. Choosing correct Design Patterns to build the software based on its specifications.