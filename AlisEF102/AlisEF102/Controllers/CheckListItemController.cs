using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisEF102.Models;
using AlisEF102.DAL;

namespace AlisEF102.Controllers
{ 
    public class CheckListItemController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /CheckListItem/

        public ViewResult Index()
        {
            var checklistitems = db.CheckListItems.Include(c => c.Asset);
            return View(checklistitems.ToList());
        }

        //
        // GET: /CheckListItem/Details/5

        public ViewResult Details(int id)
        {
            CheckListItem checklistitem = db.CheckListItems.Find(id);
            return View(checklistitem);
        }

        //
        // GET: /CheckListItem/Create

        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode");
            return View();
        } 

        //
        // POST: /CheckListItem/Create

        [HttpPost]
        public ActionResult Create(CheckListItem checklistitem)
        {
            if (ModelState.IsValid)
            {
                db.CheckListItems.Add(checklistitem);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", checklistitem.AssetID);
            return View(checklistitem);
        }
        
        //
        // GET: /CheckListItem/Edit/5
 
        public ActionResult Edit(int id)
        {
            CheckListItem checklistitem = db.CheckListItems.Find(id);
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", checklistitem.AssetID);
            return View(checklistitem);
        }

        //
        // POST: /CheckListItem/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckListItem checklistitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checklistitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", checklistitem.AssetID);
            return View(checklistitem);
        }

        //
        // GET: /CheckListItem/Delete/5
 
        public ActionResult Delete(int id)
        {
            CheckListItem checklistitem = db.CheckListItems.Find(id);
            return View(checklistitem);
        }

        //
        // POST: /CheckListItem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CheckListItem checklistitem = db.CheckListItems.Find(id);
            db.CheckListItems.Remove(checklistitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}