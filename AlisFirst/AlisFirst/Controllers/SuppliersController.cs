using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Controllers
{   
    public class SuppliersController : Controller
    {
		private readonly ISupplierRepository supplierRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SuppliersController() : this(new SupplierRepository())
        {
        }

        public SuppliersController(ISupplierRepository supplierRepository)
        {
			this.supplierRepository = supplierRepository;
        }

        //
        // GET: /Suppliers/

        public ViewResult Index()
        {
            return View(supplierRepository.AllIncluding(supplier => supplier.Assets));
        }

        //
        // GET: /Suppliers/Details/5

        public ViewResult Details(int id)
        {
            return View(supplierRepository.Find(id));
        }

        //
        // GET: /Suppliers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Suppliers/Create

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid) {
                supplierRepository.InsertOrUpdate(supplier);
                supplierRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Suppliers/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(supplierRepository.Find(id));
        }

        //
        // POST: /Suppliers/Edit/5

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid) {
                supplierRepository.InsertOrUpdate(supplier);
                supplierRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Suppliers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(supplierRepository.Find(id));
        }

        //
        // POST: /Suppliers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            supplierRepository.Delete(id);
            supplierRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                supplierRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

