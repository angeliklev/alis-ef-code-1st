using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.DAL;

namespace AlisFirst.Areas.AMS.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IAssetRepository assetRepository;

        public DashboardController()
            : this(new AssetRepository())
        {
        }

        public DashboardController(
            IAssetRepository assetRepository            
            )
        {
            
            this.assetRepository = assetRepository;
           
        }

        //
        // GET: /AMS/Dashboard/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //TODO: finish actions for following button presses
        [HttpPost]
        public ActionResult Index(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {

            if (HttpContext.Request.Form["Maintain"] != null)
                return Maintain(v);

            else if (HttpContext.Request.Form["Employee"] != null)
                return Employee(v);

            else if (HttpContext.Request.Form["Location"] != null)
                return Location(v);

            else if (HttpContext.Request.Form["Status"] != null)
                return Status(v);
            else
                return RedirectToAction("Index");
            
        }
        
        public ActionResult Maintain(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
             String s = HttpContext.Request.Form["Search"];
            
            if (String.IsNullOrEmpty(v.Barcode))
                return RedirectToAction("Index");

            

            var i = from a in assetRepository.All
                    where a.BarCode == v.Barcode
                    select a;

            if (i.Count() == 0)
                return RedirectToAction("Index");

            return RedirectToAction("Edit/"+i.First().AssetID.ToString(), "Assets");
                    
        }

        public ActionResult Location(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
            return null;
        }

        public ActionResult Employee(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
            return null;
        }

        public ActionResult Status (AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
            return null;
        }

    }
}
