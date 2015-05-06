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
    public class ProprietesController : Controller
    {
        private H15_PROJET_E03Entities db = new H15_PROJET_E03Entities();

        // GET: Proprietes
        public ActionResult Index()
        {
            return View(db.Proprietes.ToList());
        }

        // GET: Proprietes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propriete propriete = db.Proprietes.Find(id);
            if (propriete == null)
            {
                return HttpNotFound();
            }
            return View(propriete);
        }

        // GET: Proprietes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proprietes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProprieteId,Client,Adresse,Ville")] Propriete propriete)
        {
            if (ModelState.IsValid)
            {
                db.Proprietes.Add(propriete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propriete);
        }

        // GET: Proprietes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propriete propriete = db.Proprietes.Find(id);
            if (propriete == null)
            {
                return HttpNotFound();
            }
            return View(propriete);
        }

        // POST: Proprietes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProprieteId,Client,Adresse,Ville")] Propriete propriete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propriete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propriete);
        }

        // GET: Proprietes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propriete propriete = db.Proprietes.Find(id);
            if (propriete == null)
            {
                return HttpNotFound();
            }
            return View(propriete);
        }

        // POST: Proprietes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Propriete propriete = db.Proprietes.Find(id);
            db.Proprietes.Remove(propriete);
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
