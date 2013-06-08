using System;
using System.Collections.Generic;
using System.Data;
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
            //return View(asset);

            //instead we will display partial views with forms separate for editing Asset 
            // and for its related objects
            return View(PopulateAssetMaintainView(id));
        }

        private AssetMaintain PopulateAssetMaintainView(int id)
        {
            var assetToMaintain = new AssetMaintain
            {
                AssetToEdit = PopulateAssetEditView(id),
                AssetLocations = PopulateAssetLocationsView(id),
                AssetRepairs = PopulateAssetRepairsView(id),
                AssetCheckListView = SetSelectedCheckListItemsView(id)
            };
            return assetToMaintain;
        }

        private AssetEditVM PopulateAssetEditView(int id)
        {
            var assetToEdit = assetRepo.Find(id);
            return AutoMapper.Mapper.Map<Asset, AssetEditVM>(assetToEdit);
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

        private IEnumerable<LocationHistoryItemsVM> PopulateLocationHistory(int id)
        {
            var viewModel = AutoMapper.Mapper.Map<IEnumerable<AssignedLocation>,
                IEnumerable<LocationHistoryItemsVM>>(assignedLocationRepo
                .AllIncluding(l => l.Location).OrderBy(l => l.AssignedLocationDate)
                .Where(m => m.AssetID == id));
            return viewModel;
        }

        private CreateAssignedLocationWithDDLVM PopulateLocationToCreate(int id)
        {
            var forNewLocation = new CreateAssignedLocationVM
            {
                AssetID = id,
                AssignedLocationDate = DateTime.Today
            };

            return new CreateAssignedLocationWithDDLVM(forNewLocation, locationRepo.All.ToList());
        }

        // 
        // POST from partial _AssetLocationCreate

        [HttpPost]
        public ActionResult CreateLocation(CreateAssignedLocationVM assignedLocation)
        {
            if (ModelState.IsValid)
            {
                assignedLocationRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateAssignedLocationVM,
                                                                AssignedLocation>(assignedLocation));
                assignedLocationRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    var viewModel = PopulateAssetLocationsView(assignedLocation.AssetID);
                    return PartialView("_AssetLocationHistory", viewModel);
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
                        LocationToCreate = new CreateAssignedLocationWithDDLVM(assignedLocation, locationRepo.All.ToList())
                    };
                    return PartialView("_AssetLocationHistory", viewModel);
                }
                else
                {
                    // 1 - render the whole viewmodel data
                    var editModel = PopulateAssetMaintainView(assignedLocation.AssetID);

                    // 2 - update repairToCreate with current repair data (then it is with updated modelState)
                    editModel.AssetLocations.LocationToCreate = new CreateAssignedLocationWithDDLVM(assignedLocation, locationRepo.All.ToList());

                    return View("Edit", editModel);
                }
            }
        }

        private AssetRepairsVM PopulateAssetRepairsView(int id)
        {
            return new AssetRepairsVM
            {
                RepairHistory = PopulateRepairsHistory(id),
                RepairToCreate = PopulateRepairToCreate(id)
            };
        }

        private IEnumerable<CreateAssetRepairVM> PopulateRepairsHistory(int id)
        {
            var repairs = repairRepo.All.Where(m => m.AssetID == id);
            var repairsHistory = AutoMapper.Mapper.Map<IEnumerable<Repair>,
                    IEnumerable<CreateAssetRepairVM>>(repairs);
            return repairsHistory; ;
        }

        private CreateAssetRepairVM PopulateRepairToCreate(int id)
        {
            return new CreateAssetRepairVM
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
        public ActionResult CreateRepair(CreateAssetRepairVM repairToCreate)
        {
            if (ModelState.IsValid)
            {
                repairRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateAssetRepairVM,
                                                                Repair>(repairToCreate));
                repairRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    // in Firefox, Google Chrome this doesn't clear textfields values (not as expected!)
                    return PartialView("_AssetRepairHistory", PopulateAssetRepairsView(repairToCreate.AssetID));
                }
                else
                {
                    // this renders the whole page again, with updated data
                    return RedirectToAction("Edit", new { id = repairToCreate.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    var repairModel = new AssetRepairsVM
                    {
                        RepairHistory = PopulateRepairsHistory(repairToCreate.AssetID),
                        RepairToCreate = repairToCreate
                    };
                    return PartialView("_AssetRepairHistory", repairModel);
                }
                else
                {
                    // 1 - render the whole viewmodel data
                    var editModel = PopulateAssetMaintainView(repairToCreate.AssetID);

                    // 2 - update repairToCreate with current repair data (then it is with updated modelState)
                    editModel.AssetRepairs.RepairToCreate = repairToCreate;

                    return View("Edit", editModel);
                }
            }
        }

        private AssetCheckListVM SetSelectedCheckListItemsView(int id)
        {
            var allCheckListItems = checkListItemRepository.All.ToList();
            var asset = assetRepo.Find(id);
            // check if it is not null!!!

            var assetCheckListItems = new HashSet<int>(asset.CheckListItems.Select(c => c.CheckListItemID));
            var viewModel = new AssetCheckListVM
            {
                AssetID = id,
                SelectedItems = new List<SelectedCheckListItemsData> { }
            };

            foreach (var item in allCheckListItems)
            {
                viewModel.SelectedItems.Add(new SelectedCheckListItemsData
                {
                    CheckListItemID = item.CheckListItemID,
                    ItemName = item.CheckListItemName,
                    Selected = assetCheckListItems.Contains(item.CheckListItemID)
                });
            };
            return viewModel;
        }

        // 
        // POST from partial _AssetRepairCreate

        public ActionResult UpdateCheckList(AssetCheckListVM selectedItemsView, string[] selectedItems)
        {
            //asset to update related data
            var asset = assetRepo.AllIncluding(m => m.CheckListItems)
                .Where(m => m.AssetID == selectedItemsView.AssetID).FirstOrDefault();

            if (selectedItems == null)
            {
                asset.CheckListItems = new List<CheckListItem>();
            }
            else
            {
                var allCheckListItems = checkListItemRepository.All.ToList();
                // current asset items ids, before update 
                var assetItemsIDsHS = new HashSet<int>(asset.CheckListItems.Select(c => c.CheckListItemID));

                // input values
                var selectedItemsHS = new HashSet<string>(selectedItems);

                foreach (var item in allCheckListItems)
                {
                    if (selectedItemsHS.Contains(item.CheckListItemID.ToString()))
                    {
                        if (!assetItemsIDsHS.Contains(item.CheckListItemID))
                        {
                            asset.CheckListItems.Add(item);
                        }
                    }
                    else
                    {
                        if (assetItemsIDsHS.Contains(item.CheckListItemID))
                        {
                            asset.CheckListItems.Remove(item);
                        }
                    }
                } // end foreach
            } // end condition if selectedItems is empty

            assetRepo.Update(asset);
            assetRepo.Save();

            return PartialView("_AssetCheckListItems", SetSelectedCheckListItemsView(selectedItemsView.AssetID));
        }

        //
        // POST: /Asset/Edit/5

        [HttpPost]
        public ActionResult Edit(Asset asset, string[] selectedCheckListItems)
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
                ViewBag.PossibleCategories = categoryRepository.All;
                ViewBag.PossibleAssetModels = assetmodelRepository.All;
                return View();
            }
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


