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
    public class CategoryController : Controller
    {
        //
        // GET: /LMS/Category/
        CategoryRepository catRepo = new CategoryRepository();

        public ActionResult Index()
        {
            IEnumerable<Category> listOfCats = catRepo.All;
            var ViewModel =
                AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<ListCategoryViewModel>>(listOfCats);
            return View( ViewModel );
        }

        //
        // GET: /LMS/Category/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LMS/Category/Create

        public ActionResult Create()
        {
            Category createCat = new Category();
            CreateCategoryViewModel viewModel = AutoMapper.Mapper.Map<Category, CreateCategoryViewModel>(createCat);
            return View(viewModel);
        } 

        //
        // POST: /LMS/Category/Create

        [HttpPost]
        public ActionResult Create(CreateCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                catRepo.InsertOrUpdate(AutoMapper.Mapper.Map<CreateCategoryViewModel, Category>(viewModel));
                catRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }
        
        //
        // GET: /LMS/Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            EditCategoryViewModel viewModel = AutoMapper.Mapper.Map<Category, EditCategoryViewModel>(catRepo.Find(id));
            return View(viewModel);
        }

        //
        // POST: /LMS/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                catRepo.InsertOrUpdate(AutoMapper.Mapper.Map<EditCategoryViewModel, Category>(viewModel));
                catRepo.Save();
                return RedirectToAction("Index", "LookupTable");
            }
            return View(viewModel);
        }

        //
        // GET: /LMS/Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            Category deleteCat = catRepo.Find(id);
            return View(AutoMapper.Mapper.Map<Category, DeleteCategoryViewModel>(deleteCat));
        }

        //
        // POST: /LMS/Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            catRepo.Delete(id);
            catRepo.Save();
            return RedirectToAction("Index", "LookupTable");
        }
    }
}
