using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;
using AlisFirst.Areas.AMS.ViewModels;
using MvcPaging;

namespace AlisFirst.Areas.AMS.Controllers
{
    public class AssetController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAssetModelRepository assetmodelRepository;
        private readonly IAssetRepository assetRepository;
        private readonly ICheckListItemRepository checkListItemRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public AssetController()
            : this(new SupplierRepository(), new CategoryRepository(), new AssetModelRepository(),
            new AssetRepository(), new CheckListItemRepository())
        {
        }

        public AssetController(ISupplierRepository supplierRepository, ICategoryRepository categoryRepository,
            IAssetModelRepository assetmodelRepository, IAssetRepository assetRepository,
            ICheckListItemRepository checkListItemRepository)
        {
            this.supplierRepository = supplierRepository;
            this.categoryRepository = categoryRepository;
            this.assetmodelRepository = assetmodelRepository;
            this.assetRepository = assetRepository;
            this.checkListItemRepository = checkListItemRepository;
        }

        //
        // GET: /Asset/

        public ActionResult Index()
        {

            ViewModels.AssetsIndexViewModel AssetListIndexViewModel = new ViewModels.AssetsIndexViewModel();

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
            ViewModels.AssetsIndexViewModel AssetListIndexViewModel = new ViewModels.AssetsIndexViewModel();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var Assets = assetRepository.All.ToList();

            AssetListIndexViewModel.listViewModel = new ViewModels._AssetListViewModel();

            AssetListIndexViewModel.listViewModel.Assets = Assets.ToPagedList(currentPageIndex, 5);


            return View("Index", AssetListIndexViewModel);

        }


        [HttpPost]
        public ActionResult Index(ViewModels.AssetsIndexViewModel v)
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

        //
        // GET: /Asset/Details/5

        public ViewResult Details(int id)
        {
            return View(assetRepository.Find(id));
        }

        //
        // GET: /Asset/Create

        public ActionResult Create()
        {
            ViewBag.PossibleSuppliers = supplierRepository.All;
            ViewBag.PossibleCategories = categoryRepository.All;
            ViewBag.PossibleAssetModels = assetmodelRepository.All;
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
            else
            {
                ViewBag.PossibleSuppliers = supplierRepository.All;
                ViewBag.PossibleAssetModels = assetmodelRepository.All;
                return View();
            }
        }

        //
        // GET: /Asset/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleSuppliers = supplierRepository.All;
            ViewBag.PossibleCategories = categoryRepository.All;
            ViewBag.PossibleAssetModels = assetmodelRepository.All;

            var asset = assetRepository.Find(id);

            PopulateChosenCheckListItemsData(asset);
            return View(asset);
        }

        //
        // POST: /Asset/Edit/5

        [HttpPost]
        public ActionResult Edit(Asset asset, string[] selectedCheckListItems)
        {

            if (ModelState.IsValid)
            {
                UpdateAssetCheckListItems(selectedCheckListItems, asset);

                assetRepository.InsertOrUpdate(asset);
                assetRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleSuppliers = supplierRepository.All;
                ViewBag.PossibleCategories = categoryRepository.All;
                ViewBag.PossibleAssetModels = assetmodelRepository.All;

                PopulateChosenCheckListItemsData(asset);
                return View();
            }
        }

        private void UpdateAssetCheckListItems(string[] selectedCheckListItems, Asset assetToUpdate)
        {
            if (selectedCheckListItems == null)
            {
                assetToUpdate.CheckListItems = new List<CheckListItem>();
                return;
            }

            var allCheckItems = checkListItemRepository.All.ToList();
            var selectedItemsHS = new HashSet<string>(selectedCheckListItems);
            var assetItems = new HashSet<int>
                (assetToUpdate.CheckListItems.Select(c => c.CheckListItemID));
            foreach (var checkItem in allCheckItems)
            {
                if (selectedItemsHS.Contains(checkItem.CheckListItemID.ToString()))
                {
                    if (!assetItems.Contains(checkItem.CheckListItemID))
                    {
                        assetToUpdate.CheckListItems.Add(checkItem);
                    }
                }
                else
                {
                    if (assetItems.Contains(checkItem.CheckListItemID))
                    {
                        assetToUpdate.CheckListItems.Remove(checkItem);
                    }
                }
            }
        }
        private void PopulateChosenCheckListItemsData(Asset asset)
        {
            var allCheckListItems = checkListItemRepository.All.ToList();
            var assetCheckListItems = new HashSet<int>(asset.CheckListItems.Select(c => c.CheckListItemID));
            var viewModel = new List<SelectedCheckListItemsData>();
            foreach (var item in allCheckListItems)
            {
                viewModel.Add(new SelectedCheckListItemsData
                {
                    CheckListItemID = item.CheckListItemID,
                    ItemName = item.CheckListItemName,
                    Selected = assetCheckListItems.Contains(item.CheckListItemID)
                });
            };
            ViewBag.CheckListItems = viewModel;
        }
        //
        // GET: /Asset/Delete/5

        public ActionResult Delete(int id)
        {
            return View(assetRepository.Find(id));
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
            if (disposing)
            {
                supplierRepository.Dispose();
                assetmodelRepository.Dispose();
                assetRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

//        private AlisFirstContext db = new AlisFirstContext();

//        //
//        // GET: /AMS/Asset/

//        public ViewResult Index()
//        {
//            var assets = db.Assets.Include(a => a.Supplier).Include(a => a.Category).Include(a => a.AssetModel);
//            return View(assets.ToList());
//        }

//        //
//        // GET: /AMS/Asset/Details/5

//        public ViewResult Details(int id)
//        {
//            Asset asset = db.Assets.Find(id);
//            return View(asset);
//        }

//        //
//        // GET: /AMS/Asset/Create

//        public ActionResult Create()
//        {
//            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
//            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
//            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName");
//            return View();
//        } 

//        //
//        // POST: /AMS/Asset/Create

//        [HttpPost]
//        public ActionResult Create(Asset asset)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Assets.Add(asset);
//                db.SaveChanges();
//                return RedirectToAction("Index");  
//            }

//            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", asset.SupplierID);
//            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", asset.CategoryID);
//            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName", asset.AssetModelID);
//            return View(asset);
//        }

//        //
//        // GET: /AMS/Asset/Edit/5

//        public ActionResult Edit(int id)
//        {
//            Asset asset = db.Assets.Find(id);
//            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", asset.SupplierID);
//            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", asset.CategoryID);
//            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName", asset.AssetModelID);
//            return View(asset);
//        }

//        //
//        // POST: /AMS/Asset/Edit/5

//        [HttpPost]
//        public ActionResult Edit(Asset asset)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(asset).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", asset.SupplierID);
//            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", asset.CategoryID);
//            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName", asset.AssetModelID);
//            return View(asset);
//        }

//        //
//        // GET: /AMS/Asset/Delete/5

//        public ActionResult Delete(int id)
//        {
//            Asset asset = db.Assets.Find(id);
//            return View(asset);
//        }

//        //
//        // POST: /AMS/Asset/Delete/5

//        [HttpPost, ActionName("Delete")]
//        public ActionResult DeleteConfirmed(int id)
//        {            
//            Asset asset = db.Assets.Find(id);
//            db.Assets.Remove(asset);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}
