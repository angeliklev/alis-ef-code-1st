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
    public class AssetModelController : Controller
    {
        //
        // GET: /LMS/AssetModel/

        AssetModelRepository assModRepo = new AssetModelRepository();
        public ActionResult Index()
        {
            IEnumerable<AssetModel> listOfAssMods = assModRepo.All;
            var ViewModel =
                AutoMapper.Mapper.Map<IEnumerable<AssetModel>, IEnumerable<ListAssetModelViewModel>>(listOfAssMods);
            return View(ViewModel);
        }

        //
        // GET: /LMS/AssetModel/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LMS/AssetModel/Create

        public ActionResult Create()
        {
            AssetModel createAssMod = new AssetModel();
            CreateAssetModelViewModel viewModel = AutoMapper.Mapper.Map<AssetModel, CreateAssetModelViewModel>(createAssMod);
            return View(viewModel);
        }

        //
        // POST: /LMS/AssetModel/Create

        [HttpPost]
        public ActionResult Create(CreateAssetModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                assModRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateAssetModelViewModel, AssetModel>(viewModel));
                assModRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/AssetModel/Edit/5

        public ActionResult Edit(int id)
        {
            EditAssetModelViewModel viewModel = AutoMapper.Mapper.Map<AssetModel, EditAssetModelViewModel>(assModRepo.Find(id));
            return View(viewModel);
        }

        //
        // POST: /LMS/AssetModel/Edit/5

        [HttpPost]
        public ActionResult Edit(EditAssetModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                assModRepo.InsertOrUpdate(AutoMapper.Mapper.Map<EditAssetModelViewModel, AssetModel>(viewModel));
                assModRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/AssetModel/Delete/5

        public ActionResult Delete(int id)
        {
            AssetModel deleteAssMod = assModRepo.Find(id);
            return View(AutoMapper.Mapper.Map<AssetModel, DeleteAssetModelViewModel>(deleteAssMod));
        }

        //
        // POST: /LMS/AssetModel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assModRepo.Delete(id);
            assModRepo.Save();
            return RedirectToAction("Index", "LookupTable");
        }
    }
}
