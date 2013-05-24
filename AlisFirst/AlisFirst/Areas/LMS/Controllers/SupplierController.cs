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
    public class SupplierController : Controller
    {
        //
        // GET: /LMS/Supplier/
        SupplierRepository supRepo = new SupplierRepository();
        public ActionResult Index()
        {
            IEnumerable<Supplier> listOfSups = supRepo.All;
            var ViewModel =
                AutoMapper.Mapper.Map<IEnumerable<Supplier>, IEnumerable<ListSupplierViewModel>>(listOfSups);
            return View(ViewModel);
        }

        //
        // GET: /LMS/Supplier/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LMS/Supplier/Create

        public ActionResult Create()
        {
            Supplier createSup = new Supplier();
            CreateSupplierViewModel viewModel = AutoMapper.Mapper.Map<Supplier, CreateSupplierViewModel>(createSup);
            return View(viewModel);
        } 

        //
        // POST: /LMS/Supplier/Create

        [HttpPost]
        public ActionResult Create(CreateSupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                supRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateSupplierViewModel, Supplier>(viewModel));
                supRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }
        
        //
        // GET: /LMS/Supplier/Edit/5
 
        public ActionResult Edit(int id)
        {
            EditSupplierViewModel viewModel = AutoMapper.Mapper.Map<Supplier, EditSupplierViewModel>(supRepo.Find(id));
            return View(viewModel);
        }

        //
        // POST: /LMS/Supplier/Edit/5

        [HttpPost]
        public ActionResult Edit(EditSupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                supRepo.InsertOrUpdate(AutoMapper.Mapper.Map<EditSupplierViewModel, Supplier>(viewModel));
                supRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/Supplier/Delete/5
 
        public ActionResult Delete(int id)
        {
            Supplier deleteSup = supRepo.Find(id);
            return View(AutoMapper.Mapper.Map<Supplier, DeleteSupplierViewModel>(deleteSup));
        }

        //
        // POST: /LMS/Supplier/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            supRepo.Delete(id);
            supRepo.Save();
            return RedirectToAction("Index", "LookupTable");
        }
    }
}
