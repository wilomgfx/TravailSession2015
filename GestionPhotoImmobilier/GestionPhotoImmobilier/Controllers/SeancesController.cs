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
using PagedList;

namespace GestionPhotoImmobilier.Controllers
{
    public class SeancesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Seances
        public ActionResult Index(string currentFilter, string search,int? page, Nullable<bool> showFuture, int? pageFuture)
        {
            var colSeances = unitOfWork.SeanceRepository.ObtenirSeance();
            var colRdv = unitOfWork.RdvRepository.ObtenirRdvsComplets();

            List<SeanceRdv> lstSeanceRdv = GenererSeancesRdvs(colSeances, colRdv);

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            if (showFuture == null)
                showFuture = false;

            if(showFuture.Value)
            {
                IEnumerable<Seance> lstSeancesFutures = unitOfWork.SeanceRepository.ObtenirSeanceFutures(DateTime.Now);

                List<SeanceRdv> lstSeancesRdvsFutures = GenererSeancesRdvs(lstSeancesFutures, colRdv);

                int pageNumberFuture = (pageFuture ?? 1);
                ViewBag.seancesFutures = lstSeancesRdvsFutures.OrderByDescending(s => s.DateSeance).ToPagedList(pageNumberFuture, pageSize);
            }
            ViewBag.showFuture = showFuture;

            return View(lstSeanceRdv.OrderByDescending(s => s.DateSeance).ToPagedList(pageNumber,pageSize));
        }

        private List<SeanceRdv> GenererSeancesRdvs(IEnumerable<Seance> pSeances,IEnumerable<Rdv> pRdvs)
        {
            List<SeanceRdv> lstSeanceRdv = new List<SeanceRdv>();

            foreach (var sea in pSeances)
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

                foreach (var rdv in pRdvs)
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

            return lstSeanceRdv;
        }

        public ActionResult Sommaire(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceComplete(id);
            IEnumerable<Rdv> rdvs = unitOfWork.RdvRepository.ObtenirRdvDeLaSeance(seance.SeanceId);

            List<Seance> seanceToList = new List<Seance>();
            seanceToList.Add(seance);

            List<SeanceRdv> seanceRdv = GenererSeancesRdvs(seanceToList, rdvs);

            SommaireSeance sommaire = new SommaireSeance();

            sommaire.SeanceRdv = seanceRdv.First();
            sommaire.Agent = seance.Agent;
            sommaire.Propriete = seance.Propriete;

            return View(sommaire);
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
            SelectList AgentId = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom");
            ViewBag.AgentId = AgentId;

            SelectList ProprieteId = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse");
            ViewBag.ProprieteId = ProprieteId;
            return View();
        }

        // POST: Seances/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeanceId,DateSeance,AgentId,Photographe,Client,Forfait,Commentaire,Statut,ProprieteId")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                Agent ag = unitOfWork.AgentRepository.ObtenirAgentParID(seance.AgentId);

                seance.Agent = ag;
                seance.Propriete = unitOfWork.ProprieteRepository.ObtenirProprieteParID(seance.ProprieteId);

                unitOfWork.SeanceRepository.InsertSeance(seance);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            SelectList AgentId = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom");
            ViewBag.AgentId = AgentId;

            SelectList ProprieteId = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse");
            ViewBag.ProprieteId = ProprieteId;

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

            SelectList AgentId = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom", seance.AgentId);
            ViewBag.AgentId = AgentId;

            SelectList ProprieteId = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse");
            ViewBag.ProprieteId = ProprieteId;
            return View(seance);
        }

        // POST: Seances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeanceId,DateSeance,AgentId,Photographe,Client,Forfait,Commentaire,Statut,ProprieteId")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                Agent ag = unitOfWork.AgentRepository.ObtenirAgentParID(seance.AgentId);
                Propriete pro = unitOfWork.ProprieteRepository.ObtenirProprieteParID(seance.ProprieteId);

                Seance vraiSeance = unitOfWork.SeanceRepository.ObtenirSeanceParID(seance.SeanceId);

                vraiSeance.Agent = ag;
                vraiSeance.AgentId = seance.AgentId;
                vraiSeance.Client = seance.Client;
                vraiSeance.Commentaire = seance.Commentaire;
                vraiSeance.DateSeance = seance.DateSeance;
                vraiSeance.Forfait = seance.Forfait;
                vraiSeance.Photographe = seance.Photographe;
                vraiSeance.Propriete = seance.Propriete;
                vraiSeance.ProprieteId = seance.ProprieteId;
                vraiSeance.Rdvs = seance.Rdvs;
                vraiSeance.Statut = seance.Statut;
                vraiSeance.ProprieteId = seance.ProprieteId;
                vraiSeance.Propriete = pro;

                unitOfWork.SeanceRepository.UpdateSeance(vraiSeance);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            SelectList AgentId = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom");
            ViewBag.AgentId = AgentId;

            SelectList ProprieteId = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse");
            ViewBag.ProprieteId = ProprieteId;

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
