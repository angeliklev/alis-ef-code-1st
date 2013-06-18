using AlisFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.DAL;

namespace AlisFirst.Areas
{
    public class HomeController : Controller
    {
        public ActionResult Index(AlisFirstContext context)
        {

            //string sqlscript = (context as IObjectContextAdapter).ObjectContext.CreateDatabaseScript();
            ViewBag.Message = "Welcome to ALIS!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
