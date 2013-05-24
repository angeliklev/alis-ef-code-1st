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
    public class ManufacturerController : Controller
    {
        //
        // GET: /LMS/Manufacturer/
        ManufacturerRepository manRepo = new ManufacturerRepository();

        public ActionResult Index()
        {
            IEnumerable<Manufacturer> listOfMans = manRepo.All;
            var ViewModel =
                AutoMapper.Mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ListManufacturerViewModel>>(listOfMans);
            return View(ViewModel);
        }

        //
        // GET: /LMS/Manufacturer/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LMS/Manufacturer/Create

        public ActionResult Create()
        {
            Manufacturer createMan = new Manufacturer();
            CreateManufacturerViewModel viewModel = AutoMapper.Mapper.Map<Manufacturer, CreateManufacturerViewModel>(createMan);
            return View(viewModel);
        } 

        //
        // POST: /LMS/Manufacturer/Create

        [HttpPost]
        public ActionResult Create(CreateManufacturerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                manRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateManufacturerViewModel, Manufacturer>(viewModel));
                manRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }
        
        //
        // GET: /LMS/Manufacturer/Edit/5
 
        public ActionResult Edit(int id)
        {
            EditManufacturerViewModel viewModel = AutoMapper.Mapper.Map<Manufacturer, EditManufacturerViewModel>(manRepo.Find(id));
            return View(viewModel);
        }

        //
        // POST: /LMS/Manufacturer/Edit/5

        [HttpPost]
        public ActionResult Edit(EditManufacturerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                manRepo.InsertOrUpdate(AutoMapper.Mapper.Map<EditManufacturerViewModel, Manufacturer>(viewModel));
                manRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/Manufacturer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Manufacturer deleteCat = manRepo.Find(id);
            return View(AutoMapper.Mapper.Map<Manufacturer, DeleteManufacturerViewModel>(deleteCat));
        }

        //
        // POST: /LMS/Manufacturer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            manRepo.Delete(id);
            manRepo.Save();
            return RedirectToAction("Index", "LookupTable");
        }
    }
}
