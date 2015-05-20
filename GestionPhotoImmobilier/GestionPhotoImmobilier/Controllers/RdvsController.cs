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
using System.Data.Entity.Validation;

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

        public ActionResult CreateSansInfos()
        {
            List<Seance> lstSeancesValides = new List<Seance>();

            foreach (var item in unitOfWork.SeanceRepository.ObtenirSeance())
            {
                IEnumerable<Rdv> rdvs = unitOfWork.RdvRepository.ObtenirRdvDeLaSeance(item.SeanceId);

                if (rdvs.Count() == 0)
                    lstSeancesValides.Add(item);
            }

            SelectList seances = new SelectList(lstSeancesValides, "SeanceId", "DateSeance");
            ViewBag.SeanceId = seances;
            return View();
        }
        [HttpPost, ActionName("CreateSansInfos")]
        public ActionResult CreateSansInfos(FormCollection collection)
        {
            int seanceId = int.Parse(collection["SeanceId"]);

            Seance sea = unitOfWork.SeanceRepository.ObtenirSeanceComplete(seanceId);

            TempData["seance"] = sea;

            return RedirectToAction("Create");
        }

        // GET: Rdvs/Create
        public ActionResult Create()
        {
            List<Seance> lstSeancesValides = new List<Seance>();

            foreach (var item in unitOfWork.SeanceRepository.ObtenirSeance())
            {
                IEnumerable<Rdv> rdvs = unitOfWork.RdvRepository.ObtenirRdvDeLaSeance(item.SeanceId);

                if (rdvs.Count() == 0)
                    lstSeancesValides.Add(item);
            }
            Seance sea = (Seance)TempData["seance"];

            SelectList seances = new SelectList(lstSeancesValides, "SeanceId", "DateSeance", sea.SeanceId);
            ViewBag.SeanceId = seances;
            ViewBag.seance = sea;
            ViewBag.idSeance = sea.SeanceId;
            TempData["seanceTemp"] = sea;
            return View();
        }

        // POST: Rdvs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RdvId,Confirmer,Client,Photographe,SeanceId")] Rdv rdv, FormCollection collection)
        {
            int seanceId = int.Parse(collection["VraiSeanceId"]);

            rdv.SeanceId = seanceId;

            Seance seanceModifiée = unitOfWork.SeanceRepository.ObtenirSeanceParID(rdv.SeanceId);

            rdv.Client = seanceModifiée.Client;
            rdv.Photographe = seanceModifiée.Photographe;
            rdv.Seance = seanceModifiée;

            if (ModelState.IsValid)
            {
                List<Rdv> lstRdvs = new List<Rdv>();
                lstRdvs.Add(rdv);

                seanceModifiée.Rdvs = lstRdvs;
                rdv.Seance = seanceModifiée;

                unitOfWork.SeanceRepository.Update(seanceModifiée);
                unitOfWork.RdvRepository.Insert(rdv);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    
                }
            }

            List<Seance> lstSeancesValides = new List<Seance>();

            foreach (var item in unitOfWork.SeanceRepository.ObtenirSeance())
            {
                IEnumerable<Rdv> rdvs = unitOfWork.RdvRepository.ObtenirRdvDeLaSeance(item.SeanceId);

                if (rdvs.Count() == 0)
                    lstSeancesValides.Add(item);
            }

            SelectList seances = new SelectList(lstSeancesValides, "SeanceId", "DateSeance");
            ViewBag.SeanceId = seances;

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
                Seance seanceLiee = unitOfWork.SeanceRepository.ObtenirSeanceParID(rdv.SeanceId);
                Rdv rdvOriginal = unitOfWork.RdvRepository.ObtenirRdvParID(rdv.RdvId);

                rdvOriginal.Client = rdv.Client;
                rdvOriginal.Confirmer = rdv.Confirmer;
                rdvOriginal.Photographe = rdv.Photographe;
                rdvOriginal.Seance = seanceLiee;
                rdvOriginal.SeanceId = rdv.SeanceId;

                List<Rdv> lstRdvs = new List<Rdv>();
                lstRdvs.Add(rdvOriginal);
                seanceLiee.Rdvs = lstRdvs;

                unitOfWork.SeanceRepository.Update(seanceLiee);
                unitOfWork.RdvRepository.UpdateRdv(rdvOriginal);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            List<Seance> lstSeancesValides = new List<Seance>();

            foreach (var item in unitOfWork.SeanceRepository.ObtenirSeance())
            {
                IEnumerable<Rdv> rdvs = unitOfWork.RdvRepository.ObtenirRdvDeLaSeance(item.SeanceId);

                if (rdvs.Count() == 0)
                    lstSeancesValides.Add(item);
            }

            SelectList seances = new SelectList(lstSeancesValides, "SeanceId", "DateSeance");
            ViewBag.SeanceId = seances;

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
            Seance seanceLiee = unitOfWork.SeanceRepository.ObtenirSeanceParID(rdv.SeanceId);

            seanceLiee.Rdvs = null;

            unitOfWork.SeanceRepository.Update(seanceLiee);
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
