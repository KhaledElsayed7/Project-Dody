using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DODY.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
          

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        [HttpGet]
        [ActionName("Sign_in")]
        public ActionResult Sign_in_get()
        {


            return View();
        }
        [HttpPost]
        [ActionName("Sign_in")]
        public ActionResult Sign_in_post(FormCollection obj)
        {
            ViewBag.Message = "Your contact page.";

            ViewBag.fn       = obj["first name"];
            ViewBag.ln       = obj["last name"];
            ViewBag.phone    = obj["phone"];
            ViewBag.Email    = obj["Email"];
            ViewBag.password = obj["password"];
            ViewBag.country  = obj["country"];
            ViewBag.BirthDay = obj["BirthDay"];

            return View();
        }
    }
}