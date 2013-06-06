using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AlisFirst.Areas.AMS.ViewModels;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.Controllers
{
    public class AssetController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAssetModelRepository assetmodelRepository;
        private readonly IAssetRepository assetRepo;
        private readonly ICheckListItemRepository checkListItemRepository;
        private readonly IRepairRepository repairRepo;
        private readonly IAssignedLocationRepository assignedLocationRepo;
        private readonly ILocationRepository locationRepo;

        // If you are using Dependency Injection, you can delete the following constructor
        public AssetController()
            : this(new SupplierRepository(), new CategoryRepository(), new AssetModelRepository(),
            new AssetRepository(), new CheckListItemRepository(), new RepairRepository(),
            new AssignedLocationRepository(), new LocationRepository())
        {
        }

        public AssetController(ISupplierRepository supplierRepository, ICategoryRepository categoryRepository,
            IAssetModelRepository assetmodelRepository, IAssetRepository assetRepository,
            ICheckListItemRepository checkListItemRepository, IRepairRepository repairRepository,
            IAssignedLocationRepository assignedLocationRepository, ILocationRepository locationReposiroty)
        {
            this.supplierRepository = supplierRepository;
            this.categoryRepository = categoryRepository;
            this.assetmodelRepository = assetmodelRepository;
            this.assetRepo = assetRepository;
            this.checkListItemRepository = checkListItemRepository;
            this.repairRepo = repairRepository;
            this.assignedLocationRepo = assignedLocationRepository;
            this.locationRepo = locationReposiroty;
        }

        //
        // GET: /Asset/

        public ActionResult Index()
        {
            var assets = assetRepo.AllIncluding(asset => asset.Supplier,
                asset => asset.AssetModel,
                asset => asset.AssetConditions,
                asset => asset.CheckListItems,
                asset => asset.Repairs,
                asset => asset.Loans,
                asset => asset.AssignedToes,
                asset => asset.AssignedLocations);
            return View(assets);
            //return RedirectToAction("Index", "AssetList");
        }

        //
        // GET: /Asset/Details/5

        public ViewResult Details(int id)
        {
            return View(assetRepo.Find(id));
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
                assetRepo.InsertOrUpdate(asset);
                assetRepo.Save();
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
        // GET: AMS/Asset/Edit/5

        public ActionResult Edit(int id)
        {
            //// to display single form for editing Asset and related Checklist items.
            //ViewBag.PossibleSuppliers = supplierRepository.All;
            //ViewBag.PossibleCategories = categoryRepository.All;
            //ViewBag.PossibleAssetModels = assetmodelRepository.All;
            //var asset = assetRepository.Find(id);
            //PopulateChosenCheckListItemsData(asset);
            //return View(asset);

            //instead we will display partial views with forms separate for editing Asset 
            // and for its related objects
            return View(PopulateAssetMaintainView(id));
        }

        private AssetMaintain PopulateAssetMaintainView(int id)
        {
            var assetToMaintain = new AssetMaintain
            {
                AssetID = id,
                AssetToEdit = PopulateAssetEditView(id),
                AssetRepairs = PopulateAssetRepairsView(id),
                AssetLocations = PopulateAssetLocationsView(id)
            };
            return assetToMaintain;
        }

        private AssetEdit PopulateAssetEditView(int id)
        {
            var assetToEdit = assetRepo.Find(id);
            return AutoMapper.Mapper.Map<Asset, AssetEdit>(assetToEdit);
        }

        private AssetAssignedLocationsVM PopulateAssetLocationsView(int id)
        {
            var viewModel =
             new AssetAssignedLocationsVM
            {
                LocationHistory = PopulateLocationHistory(id),
                LocationToCreate = PopulateLocationToCreate(id)
            };
            return viewModel;
        }

        private IEnumerable<AssignedLocationData> PopulateLocationHistory(int id)
        {
            // here happens lazy loading for the last added object.... 
            // the Location.LocationName is not displayed, but this happens if to refresh the page
            var viewModel = AutoMapper.Mapper.Map<IEnumerable<AssignedLocation>, 
                IEnumerable<AssignedLocationData>>(assignedLocationRepo.All
                .Where(m => m.AssetID == id));
            return viewModel;
        }

        private AssignedLocationVM PopulateLocationToCreate(int id)
        {
            var forNewLocation = new AssignedLocationData 
            {
                AssetID = id, AssignedLocationDate = DateTime.Today
            };

            return new AssignedLocationVM( forNewLocation, locationRepo.All.ToList());
        }
        
        // 
        // POST from partial _AssetLocationCreate

        [HttpPost]
        public ActionResult CreateLocation(AssignedLocationData assignedLocation)
        {
            if (ModelState.IsValid)
            {
                assignedLocationRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssignedLocationData,
                                                                AssignedLocation>(assignedLocation));
                assignedLocationRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    var viewModel = PopulateAssetLocationsView(assignedLocation.AssetID);
                    return PartialView("_AssetLocationHistory",viewModel);
                }
                else
                {
                    // this returns the whole page again, with updated data
                    return RedirectToAction("Edit", new { id = assignedLocation.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    var viewModel = new AssetAssignedLocationsVM
                    {
                        LocationHistory = PopulateLocationHistory(assignedLocation.AssetID),
                        LocationToCreate = new AssignedLocationVM( assignedLocation, locationRepo.All.ToList())
                    };
                    return PartialView("_AssetLocationHistory", viewModel);
                }
                else
                {
                    // 1 - render the whole viewmodel data
                    var editModel = PopulateAssetMaintainView(assignedLocation.AssetID);

                    // 2 - update repairToCreate with current repair data (then it is with updated modelState)
                    editModel.AssetLocations.LocationToCreate = new AssignedLocationVM(assignedLocation, locationRepo.All.ToList()); 

                    return View("Edit", editModel);
                }
            }
        }

        private AssetRepairs PopulateAssetRepairsView(int id)
        {
            return new AssetRepairs
            {
                RepairHistory = PopulateRepairsHistory(id),
                RepairToCreate = PopulateRepairToCreate(id)
            };
        }

        private IEnumerable<AssetRepair> PopulateRepairsHistory(int id)
        {
            var repairs = repairRepo.All.Where(m => m.AssetID == id);
            var repairsHistory = AutoMapper.Mapper.Map<IEnumerable<Repair>,
                    IEnumerable<AssetRepair>>(repairs);
            return repairsHistory; ;
        }

        private AssetRepair PopulateRepairToCreate(int id)
        {
            return new AssetRepair
            {
                AssetID = id,
                IssuedDate = DateTime.Today,
                TechnicianName = "",
                Result = " ",
            };
        }

        // 
        // POST from partial _AssetRepairCreate

        [HttpPost]
        public ActionResult CreateRepair(AssetRepair repairToCreate)
        {
            if (ModelState.IsValid)
            {
                repairRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssetRepair,
                                                                Repair>(repairToCreate));
                repairRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    //
                    // in Firefox, Google Chrome this doesn't clear textfields values (not as expected!)
                    return PartialView("_AssetRepairHistory", PopulateAssetRepairsView(repairToCreate.AssetID));
                }
                else
                {
                    // works in Firefox, IE, Google Chrome
                    // this renders the whole page again, with updated data
                    return RedirectToAction("Edit", new { id = repairToCreate.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    // works in Firefox, Google Chrome
                    //this works correct but not in Google Chrome
                    //return PartialView("_AssetRepairCreate", repairToCreate);
                    var repairModel = new AssetRepairs
                    {
                        RepairHistory = PopulateRepairsHistory(repairToCreate.AssetID),
                        RepairToCreate = repairToCreate
                    };
                    return PartialView("_AssetRepairHistory", repairModel);
                }
                else
                {
                    // works in Firefox, IE, Google Chrome
                    // 1 - render the whole viewmodel data
                    var editModel = PopulateAssetMaintainView(repairToCreate.AssetID);

                    // 2 - update repairToCreate with current repair data (then it is with updated modelState)
                    editModel.AssetRepairs.RepairToCreate = repairToCreate;

                    return View("Edit", editModel);
                }
            }
        }


        //
        // POST: /Asset/Edit/5

        [HttpPost]
        public ActionResult Edit(Asset asset, string[] selectedCheckListItems)
        {

            if (ModelState.IsValid)
            {
                UpdateAssetCheckListItems(selectedCheckListItems, asset);

                assetRepo.InsertOrUpdate(asset);
                assetRepo.Save();
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
            return View(assetRepo.Find(id));
        }

        //
        // POST: /Asset/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assetRepo.Delete(id);
            assetRepo.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                supplierRepository.Dispose();
                assetmodelRepository.Dispose();
                assetRepo.Dispose();
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
