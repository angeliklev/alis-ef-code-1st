using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.DAL;
using AlisFirst.Models;
using AlisFirst.Utils;

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

        [HttpPost]
        [AcceptButton("Maintain")]
        [ActionName("Index")]
        public ActionResult MaintainButtonPress(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
            //Needs validation to make sure that the barcode exists
            Asset asset = assetRepository.FindByBarcode(v.Barcode);
            return RedirectToAction("Edit", "Asset", new { id = asset.AssetID, area = "AMS" });
        }

        [HttpPost]
        [AcceptButton("Location")]
        [ActionName("Index")]
        public ActionResult Location(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
            Asset asset = assetRepository.FindByBarcode(v.Barcode);
            return RedirectToAction("Edit", "Asset", new { id = asset.AssetID, area = "AMS" });
        }

        [HttpPost]
        [AcceptButton("Employee")]
        [ActionName("Index")]
        public ActionResult Employee(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AcceptButton("Status")]
        [ActionName("Index")]
        public ActionResult Status(AlisFirst.Areas.AMS.ViewModels.DashboardViewModel v)
        {
            return RedirectToAction("Index");
        }

    }
}
