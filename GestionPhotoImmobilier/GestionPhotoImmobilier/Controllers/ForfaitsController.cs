using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionPhotoImmobilier.Models;

namespace GestionPhotoImmobilier.Controllers
{
    public class ForfaitsController : Controller
    {
        private H15_PROJET_E03Entities db = new H15_PROJET_E03Entities();

        // GET: Forfaits
        public ActionResult Index()
        {
            return View(db.Forfaits.ToList());
        }

        // GET: Forfaits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forfait forfait = db.Forfaits.Find(id);
            if (forfait == null)
            {
                return HttpNotFound();
            }
            return View(forfait);
        }

        // GET: Forfaits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forfaits/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForfaitId,Nom,DescriptionForfait,Prix")] Forfait forfait)
        {
            if (ModelState.IsValid)
            {
                db.Forfaits.Add(forfait);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forfait);
        }

        // GET: Forfaits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forfait forfait = db.Forfaits.Find(id);
            if (forfait == null)
            {
                return HttpNotFound();
            }
            return View(forfait);
        }

        // POST: Forfaits/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForfaitId,Nom,DescriptionForfait,Prix")] Forfait forfait)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forfait).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forfait);
        }

        // GET: Forfaits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forfait forfait = db.Forfaits.Find(id);
            if (forfait == null)
            {
                return HttpNotFound();
            }
            return View(forfait);
        }

        // POST: Forfaits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forfait forfait = db.Forfaits.Find(id);
            db.Forfaits.Remove(forfait);
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
