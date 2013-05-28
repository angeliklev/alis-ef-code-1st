using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Areas.AMS.ViewModels;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.Controllers
{   
    public class EmployeeController : Controller
    {
        BorrowerRepository empRepo = new BorrowerRepository();

        //
        // GET: /Employee/

        public ViewResult Index( String SearchFor)
        {
            IEnumerable<Borrower> AllEmployees = empRepo.AllEmployees;
            ListEmployeeViewModel viewModel = new ListEmployeeViewModel();

            if (String.IsNullOrEmpty(SearchFor))
            {
                viewModel.ListOfEmployees =
                    AutoMapper.Mapper.Map<IEnumerable<Borrower>, IEnumerable<EditEmployeeViewModel>>(empRepo.AllEmployees);
            }
            else
            {
                viewModel.ListOfEmployees =
                    AutoMapper.Mapper.Map<IEnumerable<Borrower>, IEnumerable<EditEmployeeViewModel>>(empRepo.All.Where(m => m.BarCode == SearchFor));
            }

            viewModel.SearchKey = "";
            return View(viewModel);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            Borrower bor = new Borrower();
            CreateEmployeeViewModel viewModel =
                AutoMapper.Mapper.Map<Borrower, CreateEmployeeViewModel>(bor);
            return View(viewModel);
        } 

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(CreateEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Borrower borrower =
                    AutoMapper.Mapper.Map<CreateEmployeeViewModel, Borrower>(viewModel);
                borrower.IsEmployee = true;
                borrower.BorrowerID = 0;
                empRepo.InsertOrUpdate(borrower);
                empRepo.Save();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
        
        //
        // GET: /Employee/Edit/5
 
        public ActionResult Edit(int id)
        {
            Borrower borrower = empRepo.Find(id);
            EditEmployeeViewModel viewModel =
                AutoMapper.Mapper.Map<Borrower, EditEmployeeViewModel>(borrower);
            return View(viewModel);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(EditEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Borrower borrower =
                    AutoMapper.Mapper.Map<EditEmployeeViewModel, Borrower>(viewModel);
                borrower.IsEmployee = true;
                empRepo.InsertOrUpdate(borrower);
                empRepo.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        //
        // GET: /Employee/Delete/5
 
        public ActionResult Delete(int id)
        {
            Borrower borrower = empRepo.Find(id);
            DeleteEmployeeViewModel viewModel =
                AutoMapper.Mapper.Map<Borrower, DeleteEmployeeViewModel>(borrower);
            return View(viewModel);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            empRepo.Delete(id);
            empRepo.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            empRepo.Dispose();
        }

        public ActionResult AssignToEmployee(int id)
        {
            AssignedToRepository assToRepo = new AssignedToRepository();
            AssignToEmployeeViewModel viewModel = new AssignToEmployeeViewModel();
            viewModel.History = assToRepo.GetAssignedToByAssetID(id);
            return View(viewModel);
        }
    }
}

