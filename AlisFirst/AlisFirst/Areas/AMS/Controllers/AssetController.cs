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
        private readonly IAssignedLocationRepository assignedlocRepo;
        private readonly ILocationRepository locationRepo;
        private readonly IAssignedStatusRepository assignstatRepo;
        private readonly IAssetConditionRepository condRepo;

        // If you are using Dependency Injection, you can delete the following constructor
        public AssetController()
            : this(new SupplierRepository(), new CategoryRepository(), new AssetModelRepository(),
            new AssetRepository(), new CheckListItemRepository(), new RepairRepository(),
            new AssignedLocationRepository(), new LocationRepository(), new AssignedStatusRepository(),
            new AssetConditionRepository())
        {
        }

        public AssetController(ISupplierRepository supplierRepository, ICategoryRepository categoryRepository,
            IAssetModelRepository assetmodelRepository, IAssetRepository assetRepository,
            ICheckListItemRepository checkListItemRepository, IRepairRepository repairRepository,
            IAssignedLocationRepository assignedLocationRepository, ILocationRepository locationRepository,
            IAssignedStatusRepository assignstatRepo, IAssetConditionRepository assetcondRepo)
        {
            this.supplierRepository = supplierRepository;
            this.categoryRepository = categoryRepository;
            this.assetmodelRepository = assetmodelRepository;
            this.assetRepo = assetRepository;
            this.checkListItemRepository = checkListItemRepository;
            this.repairRepo = repairRepository;
            this.condRepo = assetcondRepo;
            this.assignedlocRepo = assignedLocationRepository;
            this.locationRepo = locationRepository;
            this.assignstatRepo = assignstatRepo;
        }

        //
        // GET: /Asset/

        public ActionResult Index()
        {
            //this return is to work with my index until dashboard is ready
            var assets = assetRepo.All;
            return View(assets);

            //// this 'return' is to work with AMS dashboard when it is ready
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var assetmodel = new AssetMaintainModel(id);
            return View("Maintain", assetmodel);
        }

        // this works if to move all the partial views into Shared forlder!
        //[ActionName("EditGetByModel")]
        //public ActionResult EditGetByModel()
        //{
        //    var viewmodel = TempData["assetmodel"] as AssetMaintainModel;
        //    return View("Edit", viewmodel);
        //}

        // this doesn't work when there is non Ajax method with the same actionName
        // 
        // POST from partial _AssetLocationCreate       
        //[HttpPost]
        //[AjaxOnly]
        //[ActionName("CreateLocation")]
        //public ActionResult CreateLocationAjax(AssignedLocationCreateModel LocToCreate)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        assignedlocRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssignedLocationCreateModel,
        //                                                         AssignedLocation>(LocToCreate));
        //        assignedlocRepo.Save();

        //        var viewModel = new AssetAssignedLocationsModel(LocToCreate.AssetID);
        //        return PartialView("_AssignedLocationForm", viewModel);
        //    }
        //    else
        //    {
        //        var viewModel = new AssetAssignedLocationsModel(LocToCreate);
        //        return PartialView("_AssignedLocationCreate", viewModel);
        //    }
        //}

        [HttpPost]
        public ActionResult CreateLocation(AssignedLocationCreateModel LocToCreate)
        {
            if (ModelState.IsValid)
            {
                assignedlocRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssignedLocationCreateModel,
                                                                AssignedLocation>(LocToCreate));
                assignedlocRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    var viewModel = new AssetAssignedLocationsModel(LocToCreate.AssetID);
                    return PartialView("_AssignedLocationForm", viewModel);
                }
                else
                {
                    //this returns the whole page again, with updated data
                    return RedirectToAction("Edit", "Asset", new { id = LocToCreate.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    // this branch never runs - validation always happens on the client side!
                    var viewModel = new AssetAssignedLocationsModel(LocToCreate);
                    return PartialView("_AssignedLocationCreate", viewModel);
                }
                else
                {
                    AssetMaintainModel viewModel = new AssetMaintainModel(LocToCreate.AssetID);
                    viewModel.AssetLocations = new AssetAssignedLocationsModel(LocToCreate);
                    return View("Maintain", viewModel);
                }
            }
        }

        // POST from partial _AssetStatusCreate
        [HttpPost]
        public ActionResult CreateAssignedStatus(AssignedStatusCreateModel statToCreate)
        {
            if (ModelState.IsValid)
            {
                assignstatRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssignedStatusCreateModel, AssignedStatus>(statToCreate));
                assignstatRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    var viewModel = new AssetAssignedStatusModel(statToCreate.AssetID);
                    return PartialView("_AssignedStatusForm", viewModel);
                }
                else
                {
                    return RedirectToAction("Edit", new { id = statToCreate.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    var viewModel = new AssetAssignedStatusModel(statToCreate);
                    return PartialView("_AssignedStatusCreate", viewModel);
                }
                else
                {
                    var viewModel = new AssetMaintainModel(statToCreate.AssetID);
                    viewModel.AssetAssignedStatus = new AssetAssignedStatusModel(statToCreate);
                    return View("Maintain", viewModel);
                }
            }
        }

        // post from partial _AssetConditionCreate
        [HttpPost]
        public ActionResult CreateCondition(AssetConditionCreateModel condToCreate)
        {
            if (ModelState.IsValid)
            {
                condRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssetConditionCreateModel, AssetCondition>(condToCreate));
                condRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_AssetConditionForm", new AssetConditionsModel(condToCreate.AssetID));
                }
                else
                {
                    return RedirectToAction("Edit", new { id = condToCreate.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    var condModel = new AssetConditionsModel(condToCreate);
                    return PartialView("_AssetConditionForm", condModel);
                }
                else
                {
                    AssetMaintainModel viewModel = new AssetMaintainModel(condToCreate.AssetID);
                    viewModel.AssetConditions = new AssetConditionsModel(condToCreate);
                    return View("Maintain", viewModel);
                }
            }
        }

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
                    return PartialView("_AssetRepairForm", new AssetRepairsModel(repairToCreate.AssetID));
                }
                else
                {
                    return RedirectToAction("Edit", new { id = repairToCreate.AssetID });
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    var repairModel = new AssetRepairsModel(repairToCreate);
                    return PartialView("_AssetRepairForm", repairModel);
                }
                else
                {
                    AssetMaintainModel viewModel = new AssetMaintainModel(repairToCreate.AssetID);
                    viewModel.AssetRepairs = new AssetRepairsModel(repairToCreate);
                    return View("Maintain", viewModel);
                }
            }
        }

        // 
        // POST from partial _AssetRepairCreate
        public ActionResult UpdateCheckList(AssetCheckListEditModel selectedItemsView, string[] selectedItems)
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

            return PartialView("_AssetCheckListItems", new AssetCheckListEditModel(selectedItemsView.AssetID));
        }

        //
        // POST: /Asset/Edit/5

        [HttpPost]
        public ActionResult AssetEdit(AssetEditModel assetToEdit)
        {

            if (ModelState.IsValid)
            {
                assetRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssetEditModel, Asset>(assetToEdit));
                assetRepo.Save();
                if (Request.IsAjaxRequest())
                {          
                    return PartialView("_AssetEdit", assetToEdit);
                }
                    else
	            {
                    return RedirectToAction("Edit", new { id = assetToEdit.AssetID});
	            }
            }
            else
            {
                ViewBag.PossibleSuppliers = supplierRepository.All;
                ViewBag.PossibleCategories = categoryRepository.All;
                ViewBag.PossibleAssetModels = assetmodelRepository.All;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_AssetEdit", assetToEdit);
                }
                else
                {
                    AssetMaintainModel viewModel = new AssetMaintainModel(assetToEdit.AssetID);
                    viewModel.AssetToEdit = assetToEdit;
                    return View("Maintain", viewModel);
                }
            }
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

