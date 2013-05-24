using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Areas.AMS.Controllers
{
    public class AssetListController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IAssetModelRepository assetmodelRepository;
        private readonly IAssetRepository assetRepository;


        // If you are using Dependency Injection, you can delete the following constructor
        public AssetListController()
            : this(new SupplierRepository(), new AssetModelRepository(), new AssetRepository())
        {
        }

        public AssetListController(ISupplierRepository supplierRepository,
            IAssetModelRepository assetmodelRepository,
            IAssetRepository assetRepository
            )
        {
            this.supplierRepository = supplierRepository;
            this.assetmodelRepository = assetmodelRepository;
            this.assetRepository = assetRepository;

        }

        //
        // GET: /Asset/

        public ViewResult Index()
        {

            //This is just a silly peice of code to allow MVC 3 to redirect here as home page.
            if (!this.ControllerContext.RouteData.DataTokens.ContainsKey("area"))
            {
                this.ControllerContext.RouteData.DataTokens.Add("area", "AMS");
            }

            ViewModels.AssetsViewModel ass = new ViewModels.AssetsViewModel();

            //ass.Status = assetRepository.AllIncluding(status => status

            ass.Assets = assetRepository.All.ToList();

            return View(ass);
        }


        //This can be extended to allow filtering and searching by checking the buttons that come through on the form collections

        [HttpPost]
        public ActionResult Index(FormCollection coll)
        {
            //This is just a silly peice of code to allow MVC 3 to redirect here as home page.
            if (!this.ControllerContext.RouteData.DataTokens.ContainsKey("area"))
            {
                this.ControllerContext.RouteData.DataTokens.Add("area", "AMS");
            }
            ViewModels.AssetsViewModel ass = new ViewModels.AssetsViewModel();

            if (String.IsNullOrEmpty(coll["searchKey"]))
                return RedirectToAction("Index");

            string SearchKey = coll["searchKey"].Trim();

            var assets = from Models.Asset a in assetRepository.All
                         where a.AssetModel.AssetModelName.Contains(SearchKey) || a.BarCode.Contains(SearchKey)
                         || a.SerialNum.Contains(SearchKey) || a.Supplier.SupplierName.Contains(SearchKey)
                         select a;

            ass.Assets = assets.ToList();



            if (assets.ToList().Count == 1)
            {
                AlisFirst.Models.Asset a = assets.ToList()[0];

                return RedirectToAction("Edit", new { id = a.AssetID.ToString() });
            }

            return View(ass);
        }

        //
        // GET: /Asset/Details/5

        public ViewResult Details(int id)
        {
            Asset asset = assetRepository.Find(id);
            return View(asset);
        }

        //
        // GET: /Asset/Create

        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(supplierRepository.All, "SupplierID", "SupplierName");
            ViewBag.AssetModelID = new SelectList(assetmodelRepository.All, "AssetModelID", "AssetModelName");
            return View();
        }

        //
        // POST: /Asset/Create

        [HttpPost]
        public ActionResult Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                assetRepository.InsertOrUpdate(asset);
                assetRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(supplierRepository.All, "SupplierID", "SupplierName");
            ViewBag.AssetModelID = new SelectList(assetmodelRepository.All, "AssetModelID", "AssetModelName");
            return View(asset);
        }

        //
        // GET: /Asset/Edit/5

        public ActionResult Edit(int id)
        {
            Asset asset = assetRepository.Find(id);
            ViewBag.SupplierID = new SelectList(supplierRepository.All, "SupplierID", "SupplierName");
            ViewBag.AssetModelID = new SelectList(assetmodelRepository.All, "AssetModelID", "AssetModelName");
            return View(asset);
        }

        //
        // POST: /Asset/Edit/5

        [HttpPost]
        public ActionResult Edit(Asset asset)
        {
            if (ModelState.IsValid)
            {
                assetRepository.InsertOrUpdate(asset);
                assetRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(supplierRepository.All, "SupplierID", "SupplierName");
            ViewBag.AssetModelID = new SelectList(assetmodelRepository.All, "AssetModelID", "AssetModelName");
            return View(asset);
        }

        //
        // GET: /Asset/Delete/5

        public ActionResult Delete(int id)
        {
            Asset asset = assetRepository.Find(id);
            assetRepository.Save();
            return View(asset);
        }

        //
        // POST: /Asset/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            assetRepository.Delete(id);
            assetRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            supplierRepository.Dispose();
            assetmodelRepository.Dispose();
            assetRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}