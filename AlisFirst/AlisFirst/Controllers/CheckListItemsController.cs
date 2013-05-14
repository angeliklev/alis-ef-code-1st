using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Controllers
{   
    public class CheckListItemsController : Controller
    {
		private readonly ICheckListItemRepository checklistitemRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CheckListItemsController() : this(new CheckListItemRepository())
        {
        }

        public CheckListItemsController(ICheckListItemRepository checklistitemRepository)
        {
			this.checklistitemRepository = checklistitemRepository;
        }

        //
        // GET: /CheckListItems/

        public ViewResult Index()
        {
            return View(checklistitemRepository.AllIncluding(checklistitem => checklistitem.Assets));
        }

        //
        // GET: /CheckListItems/Details/5

        public ViewResult Details(int id)
        {
            return View(checklistitemRepository.Find(id));
        }

        //
        // GET: /CheckListItems/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /CheckListItems/Create

        [HttpPost]
        public ActionResult Create(CheckListItem checklistitem)
        {
            if (ModelState.IsValid) {
                checklistitemRepository.InsertOrUpdate(checklistitem);
                checklistitemRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /CheckListItems/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(checklistitemRepository.Find(id));
        }

        //
        // POST: /CheckListItems/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckListItem checklistitem)
        {
            if (ModelState.IsValid) {
                checklistitemRepository.InsertOrUpdate(checklistitem);
                checklistitemRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /CheckListItems/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(checklistitemRepository.Find(id));
        }

        //
        // POST: /CheckListItems/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            checklistitemRepository.Delete(id);
            checklistitemRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                checklistitemRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

