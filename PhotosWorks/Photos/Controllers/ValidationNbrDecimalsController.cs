using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Photos.Models;

namespace Photos.Controllers
{
    public class ValidationNbrDecimalsController : Controller
    {
        private ValidationNombreDecimalContext db = new ValidationNombreDecimalContext();

        // GET: ValidationNbrDecimals
        public ActionResult Index()
        {
            return View(db.ValidationNbrDecimals.ToList());
        }

        // GET: ValidationNbrDecimals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidationNbrDecimal validationNbrDecimal = db.ValidationNbrDecimals.Find(id);
            if (validationNbrDecimal == null)
            {
                return HttpNotFound();
            }
            return View(validationNbrDecimal);
        }

        // GET: ValidationNbrDecimals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ValidationNbrDecimals/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ValidationNbrDecimalID,Nb1,Nb2")] ValidationNbrDecimal validationNbrDecimal)
        {
            if (ModelState.IsValid)
            {
                db.ValidationNbrDecimals.Add(validationNbrDecimal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(validationNbrDecimal);
        }

        // GET: ValidationNbrDecimals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidationNbrDecimal validationNbrDecimal = db.ValidationNbrDecimals.Find(id);
            if (validationNbrDecimal == null)
            {
                return HttpNotFound();
            }
            return View(validationNbrDecimal);
        }

        // POST: ValidationNbrDecimals/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ValidationNbrDecimalID,Nb1,Nb2")] ValidationNbrDecimal validationNbrDecimal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(validationNbrDecimal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(validationNbrDecimal);
        }

        // GET: ValidationNbrDecimals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidationNbrDecimal validationNbrDecimal = db.ValidationNbrDecimals.Find(id);
            if (validationNbrDecimal == null)
            {
                return HttpNotFound();
            }
            return View(validationNbrDecimal);
        }

        // POST: ValidationNbrDecimals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValidationNbrDecimal validationNbrDecimal = db.ValidationNbrDecimals.Find(id);
            db.ValidationNbrDecimals.Remove(validationNbrDecimal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
