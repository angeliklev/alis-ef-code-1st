using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;
using AlisFirst.ViewModels;

namespace AlisFirst.Areas.LMS.Controllers
{
    public class LocationController : Controller
    {
        //
        // GET: /LMS/Location/
        LocationRepository locRepo = new LocationRepository();

        public ActionResult Index()
        {
            IEnumerable<Location> listOfLocs = locRepo.All;
            var ViewModel =
                AutoMapper.Mapper.Map<IEnumerable<Location>, IEnumerable<ListLocationViewModel>>(listOfLocs);
            return View(ViewModel);
        }

        //
        // GET: /LMS/Location/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LMS/Location/Create

        public ActionResult Create()
        {
            Location createLoc = new Location();
            CreateLocationViewModel viewModel = AutoMapper.Mapper.Map<Location, CreateLocationViewModel>(createLoc);
            return View(viewModel);
        } 

        //
        // POST: /LMS/Location/Create

        [HttpPost]
        public ActionResult Create(CreateLocationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                locRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateLocationViewModel, Location>(viewModel));
                locRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }
        
        //
        // GET: /LMS/Location/Edit/5

        public ActionResult Edit(int id)
        {
            EditLocationViewModel viewModel = AutoMapper.Mapper.Map<Location, EditLocationViewModel>(locRepo.Find(id));
            return View(viewModel);
        }

        //
        // POST: /LMS/Location/Edit/5

        [HttpPost]
        public ActionResult Edit(EditLocationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                locRepo.InsertOrUpdate(AutoMapper.Mapper.Map<EditLocationViewModel, Location>(viewModel));
                locRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/Location/Delete/5
 
        public ActionResult Delete(int id)
        {
            Location deleteLoc = locRepo.Find(id);
            return View(AutoMapper.Mapper.Map<Location, DeleteLocationViewModel>(deleteLoc));
        }

        //
        // POST: /LMS/Location/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            locRepo.Delete(id);
            locRepo.Save();
            return RedirectToAction("Index", "LookupTable");
        }
    }
}
