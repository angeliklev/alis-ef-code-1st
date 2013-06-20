using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.DAL;
using AlisFirst.Models;

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

            if (String.IsNullOrEmpty(v.Barcode))
                return RedirectToAction("Index");

            Asset asset = assetRepository.All.FirstOrDefault(bar => bar.BarCode == v.Barcode);

            if (asset == null)
                return RedirectToAction("Index");           

            if (HttpContext.Request.Form["Maintain"] != null)
                return Maintain(asset);

            else if (HttpContext.Request.Form["Employee"] != null)
                return Employee(asset);

            else if (HttpContext.Request.Form["Location"] != null)
                return Location(asset);

            else if (HttpContext.Request.Form["Status"] != null)
                return Status(asset);
            else
                return RedirectToAction("Index");
            
        }
        
        public ActionResult Maintain(Asset asset)
        {
            return RedirectToAction("Edit", "Asset", new { id = asset.AssetID, area = "AMS" });
        }

        public ActionResult Location(Asset asset)
        {
            return RedirectToAction("Edit", "Asset", new { id = asset.AssetID, area = "AMS" });
        }

        public ActionResult Employee(Asset asset)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Status (Asset asset)
        {
            return RedirectToAction("Index");
        }

    }
}
