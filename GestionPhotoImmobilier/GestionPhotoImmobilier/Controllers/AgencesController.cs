using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionPhotoImmobilier.Models;
using GestionPhotoImmobilier.DAL;

namespace GestionPhotoImmobilier.Controllers
{
    public class AgencesController : Controller
    {
        //private H15_PROJET_E03Entities db = new H15_PROJET_E03Entities();
        private UnitOfWork unitOfWork = new UnitOfWork();


        // GET: Agences
        public ActionResult Index()
        {
            return View(unitOfWork.AgenceRepository.ObtenirAgence().ToList());
        }

        // GET: Agences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = unitOfWork.AgenceRepository.ObtenirAgenceParID(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // GET: Agences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agences/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgenceId,Nom,Adresse,NumTel")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                //db.Agences.Add(agence);
                //db.SaveChanges();
                unitOfWork.AgenceRepository.InsertAgence(agence);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(agence);
        }

        // GET: Agences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = unitOfWork.AgenceRepository.ObtenirAgenceParID(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // POST: Agences/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgenceId,Nom,Adresse,NumTel")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(agence).State = EntityState.Modified;
                //db.SaveChanges();

                unitOfWork.AgenceRepository.UpdateAgence(agence);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(agence);
        }

        // GET: Agences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = unitOfWork.AgenceRepository.ObtenirAgenceParID(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // POST: Agences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agence agence = unitOfWork.AgenceRepository.ObtenirAgenceParID(id);
            //db.Agences.Remove(agence);
            //db.SaveChanges();
            unitOfWork.AgenceRepository.DeleteAgence(agence);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
