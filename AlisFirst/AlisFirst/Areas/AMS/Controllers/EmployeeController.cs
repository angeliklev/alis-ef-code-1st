using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.ViewModels;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.Controllers
{   
    public class EmployeeController : Controller
    {
        BorrowerRepository empRepo = new BorrowerRepository();

        //
        // GET: /Employee/

        public ViewResult Index()
        {
            IEnumerable<Borrower> AllEmployees = empRepo.AllEmployees;
            IEnumerable<ListEmployeeViewModel> viewModel =
                AutoMapper.Mapper.Map<IEnumerable<Borrower>, IEnumerable<ListEmployeeViewModel>>(AllEmployees);
            return View(viewModel);
        }

        //
        // GET: /Employee/Details/5

        public ViewResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(ListEmployeeViewModel viewModel)
        {
            return View();
        }
        
        //
        // GET: /Employee/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(ListEmployeeViewModel viewModel)
        {
            return View();
        }

        //
        // GET: /Employee/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            empRepo.Dispose();
        }
    }
}

