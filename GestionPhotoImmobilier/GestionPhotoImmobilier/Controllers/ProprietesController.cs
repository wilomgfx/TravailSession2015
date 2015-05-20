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
    public class ProprietesController : Controller
    {
       // private H15_PROJET_E03Entities db = new H15_PROJET_E03Entities();
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Proprietes
        public ActionResult Index()
        {
            return View(unitOfWork.ProprieteRepository.ObtenirPropriete().ToList());
        }

        // GET: Proprietes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propriete propriete = unitOfWork.ProprieteRepository.ObtenirProprieteParID(id);
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
                unitOfWork.ProprieteRepository.InsertPropriete(propriete);
                unitOfWork.Save();
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
            Propriete propriete = unitOfWork.ProprieteRepository.ObtenirProprieteParID(id);
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
                unitOfWork.ProprieteRepository.UpdatePropriete(propriete);

                unitOfWork.Save();
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
            Propriete propriete = unitOfWork.ProprieteRepository.ObtenirProprieteParID(id);
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
            Propriete propriete = unitOfWork.ProprieteRepository.ObtenirProprieteParID(id);

            List<Seance> seaList = new List<Seance>(propriete.Seances);

            foreach (Seance sea in seaList)
            {
                Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(sea.SeanceId);

                if (seance == null)
                {
                    continue;
                }
                IEnumerable<Rdv> rdvsSeance = unitOfWork.RdvRepository.ObtenirRdvDeLaSeance(seance.SeanceId);

                foreach (Rdv rdv in rdvsSeance)
                {
                    unitOfWork.RdvRepository.Delete(rdv);
                }

                Forfait forf = unitOfWork.ForfaitRepository.GetByID(seance.ForfaitId);

                if (forf != null)
                    forf.Seances.Remove(seance);

                unitOfWork.SeanceRepository.DeleteSeance(seance);
            }
            unitOfWork.Save();

            //unitOfWork.ProprieteRepository.DeletePropriete(propriete);
            unitOfWork.ProprieteRepository.DeleteProprieteEtPhoto(propriete);
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
