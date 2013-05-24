using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.DAL;
using AlisFirst.Models;
using AlisFirst.ViewModels;

namespace AlisFirst.Areas.LMS.Controllers
{
    public class AssetStatusController : Controller
    {
        //
        // GET: /LMS/AssetStatus/

        AssetStatusRepository assStaRepo = new AssetStatusRepository();
        public ActionResult Index()
        {
            IEnumerable<AssetStatus> listOfAssStas = assStaRepo.All;
            var ViewModel =
                AutoMapper.Mapper.Map<IEnumerable<AssetStatus>, IEnumerable<ListAssetStatusViewModel>>(listOfAssStas);
            return View(ViewModel);
        }

        //
        // GET: /LMS/AssetStatus/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LMS/AssetStatus/Create

        public ActionResult Create()
        {
            AssetStatus createAssSta = new AssetStatus();
            CreateAssetStatusViewModel viewModel = AutoMapper.Mapper.Map<AssetStatus, CreateAssetStatusViewModel>(createAssSta);
            return View(viewModel);
        }

        //
        // POST: /LMS/AssetStatus/Create

        [HttpPost]
        public ActionResult Create(CreateAssetStatusViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                assStaRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateAssetStatusViewModel, AssetStatus>(viewModel));
                assStaRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/AssetStatus/Edit/5

        public ActionResult Edit(int id)
        {
            EditAssetStatusViewModel viewModel = AutoMapper.Mapper.Map<AssetStatus, EditAssetStatusViewModel>(assStaRepo.Find(id));
            return View(viewModel);
        }

        //
        // POST: /LMS/AssetStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(EditAssetStatusViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                assStaRepo.InsertOrUpdate(AutoMapper.Mapper.Map<EditAssetStatusViewModel, AssetStatus>(viewModel));
                assStaRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/AssetStatus/Delete/5

        public ActionResult Delete(int id)
        {
            AssetStatus deleteAssSta = assStaRepo.Find(id);
            return View(AutoMapper.Mapper.Map<AssetStatus, DeleteAssetStatusViewModel>(deleteAssSta));
        }

        //
        // POST: /LMS/AssetStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assStaRepo.Delete(id);
            assStaRepo.Save();
            return RedirectToAction("Index", "LookupTable");
        }
    }
}