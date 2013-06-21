using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.DAL;

namespace AlisFirst.Areas.LMS.Controllers
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
        public ActionResult Asset(AlisFirst.Areas.LMS.ViewModels.DashboardViewModel v)
        {
            TempData["BorrowerBarcode"] = "alva-0001";

            return RedirectToAction("Create", "Loans");
            
        }

        [HttpPost]
        public ActionResult Borrower(AlisFirst.Areas.LMS.ViewModels.DashboardViewModel v)
        {


            return RedirectToAction("Index");

        }
        
       

    }
}
