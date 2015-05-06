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
using GestionPhotoImmobilier.ViewModels;

namespace GestionPhotoImmobilier.Controllers
{
    public class SeancesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Seances
        public ActionResult Index()
        {
            List<SeanceRdv> lstSeanceRdv = new List<SeanceRdv>();

            var colSeances = unitOfWork.SeanceRepository.ObtenirSeance();
            var colRdv = unitOfWork.RdvRepository.ObtenirRdvsComplets();

            foreach (var sea in colSeances)
            {
                SeanceRdv sRdv = new SeanceRdv();
                bool aUnRDV = false;

                sRdv.SeanceId = sea.SeanceId;
                sRdv.Agent = sea.Agent.Nom;
                sRdv.Client = sea.Client;
                sRdv.Commentaire = sea.Commentaire;
                sRdv.DateSeance = sea.DateSeance;
                sRdv.Forfait = sea.Forfait;
                sRdv.Statut = sea.Statut;
                sRdv.Photographe = sea.Photographe;

                foreach (var rdv in colRdv)
                {
                    if(rdv.Seance.SeanceId == sea.SeanceId)
                    {
                        sRdv.Confirmer = rdv.Confirmer;
                        sRdv.PhotographeRDV = rdv.Photographe;

                        lstSeanceRdv.Add(sRdv);
                        aUnRDV = true;
                        break;
                    }
                }

                if(!aUnRDV)
                {
                    sRdv.Confirmer = null;
                    sRdv.PhotographeRDV = null;

                    lstSeanceRdv.Add(sRdv);
                }
            }
            
            return View(lstSeanceRdv.OrderByDescending(s => s.DateSeance).ToList());
        }

        public ActionResult SeanceFuture()
        {
            List<SeanceRdv> lstSeanceRdv = new List<SeanceRdv>();

            var colSeances = unitOfWork.SeanceRepository.ObtenirSeance();
            var colRdv = unitOfWork.RdvRepository.ObtenirRdvsComplets();

            foreach (var sea in colSeances)
            {
                if (sea.DateSeance > DateTime.Now)
                {
                    SeanceRdv sRdv = new SeanceRdv();
                    bool aUnRDV = false;

                    sRdv.SeanceId = sea.SeanceId;
                    sRdv.Agent = sea.Agent.Nom;
                    sRdv.Client = sea.Client;
                    sRdv.Commentaire = sea.Commentaire;
                    sRdv.DateSeance = sea.DateSeance;
                    sRdv.Forfait = sea.Forfait;
                    sRdv.Statut = sea.Statut;
                    sRdv.Photographe = sea.Photographe;

                    foreach (var rdv in colRdv)
                    {
                        if (rdv.Seance.SeanceId == sea.SeanceId)
                        {
                            sRdv.Confirmer = rdv.Confirmer;
                            sRdv.PhotographeRDV = rdv.Photographe;

                            lstSeanceRdv.Add(sRdv);
                            aUnRDV = true;
                            break;
                        }
                    }

                    if (!aUnRDV)
                    {
                        sRdv.Confirmer = null;
                        sRdv.PhotographeRDV = null;

                        lstSeanceRdv.Add(sRdv);
                    }
                }
            }

            return View(lstSeanceRdv.OrderByDescending(s => s.DateSeance).ToList());
        }

        // GET: Seances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // GET: Seances/Create
        public ActionResult Create()
        {
            SelectList Agent = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom");
            ViewBag.Agent = Agent;
            return View();
        }

        // POST: Seances/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeanceId,DateSeance,Agent,Photographe,Client,Forfait,Commentaire,Statut")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SeanceRepository.InsertSeance(seance);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(seance);
        }

        // GET: Seances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // POST: Seances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeanceId,DateSeance,Agent,Photographe,Client,Forfait,Commentaire,Statut")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SeanceRepository.UpdateSeance(seance);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(seance);
        }

        // GET: Seances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // POST: Seances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);
            unitOfWork.SeanceRepository.DeleteSeance(seance);
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
