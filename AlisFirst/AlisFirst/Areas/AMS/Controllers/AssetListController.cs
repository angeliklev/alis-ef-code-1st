using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;
using MvcPaging;

namespace AlisFirst.Areas.AMS.Controllers
{
    public class AssetListController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IAssetModelRepository assetmodelRepository;
        private readonly IAssetRepository assetRepository;
        private readonly ILocationRepository locationRepository;
        ViewModels.AssetsViewModel AssetListIndexViewModel = new ViewModels.AssetsViewModel();

        // If you are using Dependency Injection, you can delete the following constructor
        public AssetListController()
            : this(new SupplierRepository(), new AssetModelRepository(), new AssetRepository(), new LocationRepository())
        {
        }

        public AssetListController(ISupplierRepository supplierRepository,
            IAssetModelRepository assetmodelRepository,
            IAssetRepository assetRepository,
            LocationRepository locationRepository
            )
        {
            this.supplierRepository = supplierRepository;
            this.assetmodelRepository = assetmodelRepository;
            this.assetRepository = assetRepository;
            this.locationRepository = locationRepository;
        }

        //
        // GET: /Asset/

        public ActionResult Index()
        {
            

            int currentpageindex = 0;
            var Assets = assetRepository.All.ToList();

            AssetListIndexViewModel.listViewModel = new ViewModels._AssetListViewModel();

            AssetListIndexViewModel.listViewModel.Assets = Assets.ToPagedList(currentpageindex, 5);

            return View(AssetListIndexViewModel);
        }
        
        public ActionResult AjaxIndex(int? page)
        {
            
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            AlisFirst.Areas.AMS.ViewModels._AssetListViewModel ViewModel = new ViewModels._AssetListViewModel();
           

            var Assets = assetRepository.All.ToList();
            ViewModel.Assets = Assets.ToPagedList<AlisFirst.Models.Asset>(currentPageIndex, 5);

            return PartialView("_AssetList", ViewModel);

        }

        public ActionResult noJSIndex(int? page)
        {

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var Assets = assetRepository.All.ToList();

            AssetListIndexViewModel.listViewModel = new ViewModels._AssetListViewModel();

            AssetListIndexViewModel.listViewModel.Assets = Assets.ToPagedList(currentPageIndex, 5);


            return View("Index", AssetListIndexViewModel);

        }

        //public ActionResult Filter(AlisFirst.Areas.AMS.ViewModels._AssetListViewModel model)
        //{
            

        //    ass.listViewModel = new ViewModels._AssetListViewModel();

        //    var asse = from i in assetRepository.All
        //               where model.SelectedLocations.Contains((i.AssignedLocations.OrderByDescending(te => te.AssignedLocationDate)).FirstOrDefault().LocationID)
        //               select i;

        //    ass.listViewModel.Assets = asse.ToList().ToPagedList<AlisFirst.Models.Asset>(1, 5);

        //    ass.listViewModel.Locations = locationRepository.All;

        //    return View("Index", ass);

        //}

        [HttpPost]
        public ActionResult Index(ViewModels.AssetsViewModel v)
        {
            //This is just a silly peice of code to allow MVC 3 to redirect here as home page.
            if (!this.ControllerContext.RouteData.DataTokens.ContainsKey("area"))
            {
                this.ControllerContext.RouteData.DataTokens.Add("area", "AMS");
            }

            
            if (String.IsNullOrEmpty(v.searchKey))
                return RedirectToAction("Index");



            var assets = from Models.Asset a in assetRepository.All
                         where a.AssetModel.AssetModelName.Contains(v.searchKey.Trim()) || a.BarCode.Contains(v.searchKey.Trim())
                         || a.SerialNum.Contains(v.searchKey.Trim()) || a.Supplier.SupplierName.Contains(v.searchKey.Trim())
                         select a;

            



            if (assets.ToList().Count == 1)
            {
                AlisFirst.Models.Asset a = assets.ToList()[0];

                return RedirectToAction("Edit", new { id = a.AssetID.ToString() });
            }
            v.listViewModel = new ViewModels._AssetListViewModel();
            v.listViewModel.Assets = assets.ToList().ToPagedList(0, 5);

            return View(v);
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