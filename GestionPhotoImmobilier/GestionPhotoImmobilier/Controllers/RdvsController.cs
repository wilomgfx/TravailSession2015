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
    public class RdvsController : Controller
    {
        //private GestionPhotoImmobilierEntities db = new GestionPhotoImmobilierEntities();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Rdvs
        public ActionResult Index()
        {
            return View(unitOfWork.RdvRepository.ObtenirRdv().ToList());
        }

        // GET: Rdvs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rdv rdv = unitOfWork.RdvRepository.ObtenirRdvParID(id);
            if (rdv == null)
            {
                return HttpNotFound();
            }
            return View(rdv);
        }

        // GET: Rdvs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rdvs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RdvId,Confirmer,Client,Photographe,SeanceId")] Rdv rdv)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RdvRepository.InsertRdv(rdv);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(rdv);
        }

        // GET: Rdvs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rdv rdv = unitOfWork.RdvRepository.ObtenirRdvParID(id);
            if (rdv == null)
            {
                return HttpNotFound();
            }
            return View(rdv);
        }

        // POST: Rdvs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RdvId,Confirmer,Client,Photographe,SeanceId")] Rdv rdv)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RdvRepository.UpdateRdv(rdv);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(rdv);
        }

        // GET: Rdvs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rdv rdv = unitOfWork.RdvRepository.ObtenirRdvParID(id);
            if (rdv == null)
            {
                return HttpNotFound();
            }
            return View(rdv);
        }

        // POST: Rdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rdv rdv = unitOfWork.RdvRepository.ObtenirRdvParID(id);
            unitOfWork.RdvRepository.DeleteRdv(rdv);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
