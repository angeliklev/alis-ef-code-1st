using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.DAL;

namespace AlisFirst.Areas.AMS.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /AMS/Report/

        private readonly IAssetRepository assetRepo;

        public ReportController()
            : this(new AssetRepository())
        {
        }
        public ReportController(IAssetRepository assetRepo)
        {
            this.assetRepo = assetRepo;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult WarrantyExpireReport(int? months = 1)
        {

            ViewBag.months = months;

            DateTime limit = DateTime.Now.AddMonths(months.Value);

            var assets = assetRepo.All.Where(a => a.WarrantyExpires < limit);

            return View(assets.AsEnumerable());
        }



    }
}
