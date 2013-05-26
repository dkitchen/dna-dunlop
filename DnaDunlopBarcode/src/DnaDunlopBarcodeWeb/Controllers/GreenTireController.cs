using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DnaDunlopBarcodeWeb.Models;

namespace DnaDunlopBarcodeWeb.Controllers
{
    public class GreenTireController : Controller
    {
        private Entities _db = new Entities();

        //
        // GET: /GreenTire/

        public ActionResult Index()
        {
            return View(_db.GreenTires.ToList());
        }

        //
        // GET: /GreenTire/Details/5

        public ActionResult Details(decimal id = 0)
        {
            GreenTire greentire = _db.GreenTires.Find(id);
            if (greentire == null)
            {
                return HttpNotFound();
            }
            return View(greentire);
        }

        //
        // GET: /GreenTire/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GreenTire/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GreenTire greentire)
        {
            if (ModelState.IsValid)
            {
                _db.GreenTires.Add(greentire);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(greentire);
        }

        //
        // GET: /GreenTire/Edit/5

        public ActionResult Edit(decimal id = 0)
        {
            GreenTire greentire = _db.GreenTires.Find(id);
            if (greentire == null)
            {
                return HttpNotFound();
            }
            return View(greentire);
        }

        //
        // POST: /GreenTire/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GreenTire greentire)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(greentire).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(greentire);
        }

        //
        // GET: /GreenTire/Delete/5

        public ActionResult Delete(decimal id = 0)
        {
            GreenTire greentire = _db.GreenTires.Find(id);
            if (greentire == null)
            {
                return HttpNotFound();
            }
            return View(greentire);
        }

        //
        // POST: /GreenTire/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            GreenTire greentire = _db.GreenTires.Find(id);
            _db.GreenTires.Remove(greentire);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}