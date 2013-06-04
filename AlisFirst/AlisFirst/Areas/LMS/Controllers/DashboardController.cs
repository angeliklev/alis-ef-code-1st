using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlisFirst.Areas.LMS.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /LMS/Dashboard/

        public ActionResult Index()
        {
            return View();
        }

    }
}
