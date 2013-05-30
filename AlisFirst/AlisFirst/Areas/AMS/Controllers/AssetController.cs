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

namespace AlisFirst.Areas.AMS.Controllers
{
    public class AssetController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAssetModelRepository assetmodelRepository;
        private readonly IAssetRepository assetRepository;
        private readonly ICheckListItemRepository checkListItemRepository;
        private readonly IRepairRepository repairRepo;

        // If you are using Dependency Injection, you can delete the following constructor
        public AssetController()
            : this(new SupplierRepository(), new CategoryRepository(), new AssetModelRepository(),
            new AssetRepository(), new CheckListItemRepository(), new RepairRepository())
        {
        }

        public AssetController(ISupplierRepository supplierRepository, ICategoryRepository categoryRepository,
            IAssetModelRepository assetmodelRepository, IAssetRepository assetRepository,
            ICheckListItemRepository checkListItemRepository, IRepairRepository repairRepository)
        {
            this.supplierRepository = supplierRepository;
            this.categoryRepository = categoryRepository;
            this.assetmodelRepository = assetmodelRepository;
            this.assetRepository = assetRepository;
            this.checkListItemRepository = checkListItemRepository;
            this.repairRepo = repairRepository;
        }

        //
        // GET: /Asset/

        public ActionResult Index()
        {
            // this code is to use this controller's Index:
            var assets = assetRepository.AllIncluding(asset => asset.Supplier,
                asset => asset.AssetModel,
                asset => asset.AssetConditions,
                asset => asset.CheckListItems,
                asset => asset.Repairs,
                asset => asset.Loans,
                asset => asset.AssignedToes,
                asset => asset.AssignedLocations);
            return View(assets);

            // next line is for final app when AssetList will work
            //return RedirectToAction("Index", "AssetList");
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
        // GET: /AMS/Asset/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleSuppliers = supplierRepository.All;
            ViewBag.PossibleCategories = categoryRepository.All;
            ViewBag.PossibleAssetModels = assetmodelRepository.All;

            var assetToEdit = assetRepository.Find(id);
            var assetToMaintain = new AssetMaintain();
            assetToMaintain.AssetID = id;
            assetToMaintain.AssetToEdit = AutoMapper.Mapper.Map<Asset, AssetEdit>(assetToEdit);
            assetToMaintain.AssetRepairs = new AssetRepairsEdit(id);

            PopulateChosenCheckListItemsData(assetToEdit);

            return View(assetToMaintain);
        }

        // 
        // POST from partial _AssetRepairCreate

        [HttpPost]
        public ActionResult CreateRepair(AssetRepairsEdit.AssetRepair repairToCreate)
        {
            if (ModelState.IsValid)
            {
                repairRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssetRepairsEdit.AssetRepair, 
                                                                Repair>(repairToCreate));
                repairRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    // this doesn't change textfields values
                    return PartialView("_AssetRepairHistory", new AssetRepairsEdit(repairToCreate.AssetID));
                }
                else
                {
                    // this works correct
                    return RedirectToAction("Edit", new { id = repairToCreate.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    //this works correct
                    return PartialView("_AssetRepairCreate",repairToCreate);
                }
                else
                {
                    // this doesn't work. can work only if to provide whole data from the form.
                    // should catch exception and tell to turn on Javascript!
                    return View("Edit");
                }
            }

        }

        //
        // POST: /AMS/Asset/Edit/5

        [HttpPost]
        public ActionResult Edit(Asset asset, string[] selectedCheckListItems)

        // this  method signature was used in draft branch, when developing edit checklistitems list
        //public ActionResult Edit(Asset asset, string[] selectedCheckListItems)
        {

            if (ModelState.IsValid)
            {
               // UpdateAssetCheckListItems(selectedCheckListItems, asset);

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

        //private void UpdateAssetCheckListItems(string[] selectedCheckListItems, Asset assetToUpdate)
        //{
        //    if (selectedCheckListItems == null)
        //    {
        //        assetToUpdate.CheckListItems = new List<CheckListItem>();
        //        return;
        //    }

        //    var allCheckItems = checkListItemRepository.All.ToList();
        //    var selectedItemsHS = new HashSet<string>(selectedCheckListItems);
        //    var assetItems = new HashSet<int>
        //        (assetToUpdate.CheckListItems.Select(c => c.CheckListItemID));
        //    foreach (var checkItem in allCheckItems)
        //    {
        //        if (selectedItemsHS.Contains(checkItem.CheckListItemID.ToString()))
        //        {
        //            if (!assetItems.Contains(checkItem.CheckListItemID))
        //            {
        //                assetToUpdate.CheckListItems.Add(checkItem);
        //            }
        //        }
        //        else
        //        {
        //            if (assetItems.Contains(checkItem.CheckListItemID))
        //            {
        //                assetToUpdate.CheckListItems.Remove(checkItem);
        //            }
        //        }
        //    }
        //}
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

// this commented code is from the controller scaffolded with using EF context.
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
