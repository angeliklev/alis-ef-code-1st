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
            // this return is to work with my index until dashboard is ready
            var assets = assetRepo.All;
            return View(assets);

            // this 'return' is to work with AMS dashboard when it is ready
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
            return View(PopulateAssetMaintainView(id));
        }

        private AssetMaintainModel PopulateAssetMaintainView(int id)
        {
            var assetToEdit = assetRepo.Find(id);            
            
            var assetToMaintain = new AssetMaintainModel
            {
                AssetToEdit = AutoMapper.Mapper.Map<Asset, AssetMaintainModel.AssetEditVM>(assetToEdit),

                // would be great to move these below to constrictors or another controllers...
                AssetLocations = PopulateAssetLocationsView(id),
                AssetRepairs = PopulateAssetRepairsView(id),
                AssetCheckListView = SetSelectedCheckListItemsView(id)
            };
            return assetToMaintain;
        }

        private AssetMaintainModel.AssetAssignedLocationsModel PopulateAssetLocationsView(int id)
        {
            var viewModel =
             new AssetMaintainModel.AssetAssignedLocationsModel
             {
                 LocationHistory = PopulateLocationHistory(id),
                 LocationToCreate = new AssignedLocationCreateModel(id, locationRepo.All.ToList())
             };
            return viewModel;
        }

        private IEnumerable<AssetMaintainModel.LocationHistoryItemsModel> PopulateLocationHistory(int id)
        {
            return AutoMapper.Mapper.Map<IEnumerable<AssignedLocation>,
                IEnumerable<AssetMaintainModel.LocationHistoryItemsModel>>(assignedLocationRepo
                .AllIncluding(l => l.Location).OrderBy(l => l.AssignedLocationDate)
                .Where(m => m.AssetID == id));
        }

        // 
        // POST from partial _AssetLocationCreate

        [HttpPost]
        public ActionResult CreateLocation(AssignedLocationCreateModel assignedLocation)
        {
            if (ModelState.IsValid)
            {
                assignedLocationRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssignedLocationCreateModel,
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
                    var viewModel = new AssetMaintainModel.AssetAssignedLocationsModel
                    {
                        LocationHistory = PopulateLocationHistory(assignedLocation.AssetID),
                        LocationToCreate = new AssignedLocationCreateModel(assignedLocation.AssetID, locationRepo.All.ToList())
                    };
                    return PartialView("_AssetLocationHistory", viewModel);
                }
                else
                {
                    // 1 - render the whole viewmodel data
                    var editModel = PopulateAssetMaintainView(assignedLocation.AssetID);

                    // 2 - update repairToCreate with current repair data (then it is with updated modelState)
                    editModel.AssetLocations.LocationToCreate = new AssignedLocationCreateModel(assignedLocation.AssetID, locationRepo.All.ToList());

                    return View("Edit", editModel);
                }
            }
        }

        private AssetMaintainModel.AssetRepairsModel PopulateAssetRepairsView(int id)
        {
            return new AssetMaintainModel.AssetRepairsModel
            {
                RepairHistory = PopulateRepairsHistory(id),
                RepairToCreate = new AssetRepairCreateModel(id)
            };
        }

        private IEnumerable<AssetMaintainModel.AssetRepairsHistoryModel> PopulateRepairsHistory(int id)
        {
            // this didn't work. Automapper did not like the mapping Map<Asset,
                    //IEnumerable<AssetMaintainModel.AssetRepairsHistoryModel>>... needed resolving maybe.
            //var asset = assetRepo.AllIncluding(m => m.Repairs).Where(m => m.AssetID == id).FirstOrDefault();
            //return AutoMapper.Mapper.Map<Asset,IEnumerable<AssetMaintainModel.AssetRepairsHistoryModel>>(asset);   
         
            var repairs = repairRepo.All.Where(m => m.AssetID == id);
            return AutoMapper.Mapper.Map<IEnumerable<Repair>,
                    IEnumerable<AssetMaintainModel.AssetRepairsHistoryModel>>(repairs);
        }

        // 
        // POST from partial _AssetRepairCreate

        [HttpPost]
        public ActionResult CreateRepair(AssetRepairCreateModel repairToCreate)
        {
            if (ModelState.IsValid)
            {
                repairRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssetRepairCreateModel,
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
                    var repairModel = new AssetMaintainModel.AssetRepairsModel
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

        private AssetMaintainModel.AssetCheckListVM SetSelectedCheckListItemsView(int id)
        {
            var allCheckListItems = checkListItemRepository.All.ToList();
            var asset = assetRepo.Find(id);
            // check if it is not null!!!

            var assetCheckListItems = new HashSet<int>(asset.CheckListItems.Select(c => c.CheckListItemID));
            var viewModel = new AssetMaintainModel.AssetCheckListVM
            {
                AssetID = id,
                SelectedItems = new List<AssetMaintainModel.SelectedCheckListItemsData> { }
            };

            foreach (var item in allCheckListItems)
            {
                viewModel.SelectedItems.Add(new AssetMaintainModel.SelectedCheckListItemsData
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

        public ActionResult UpdateCheckList(AssetMaintainModel.AssetCheckListVM selectedItemsView, string[] selectedItems)
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

