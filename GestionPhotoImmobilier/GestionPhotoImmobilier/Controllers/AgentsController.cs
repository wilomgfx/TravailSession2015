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
    public class AgentsController : Controller
    {
       // private H15_PROJET_E03Entities db = new H15_PROJET_E03Entities();
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Agents
        public ActionResult Index()
        {
            return View(unitOfWork.AgentRepository.ObtenirAgent().ToList());
        }

        // GET: Agents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = unitOfWork.AgentRepository.ObtenirAgentParID(id);
            if (agent == null)
            {
                return HttpNotFound();
            }

            ViewModels.AgentRapport rapport = new ViewModels.AgentRapport();

            List<usp_ProduireRapportAgent_Result> lstRapport = new List<usp_ProduireRapportAgent_Result>();

            H15_PROJET_E03Entities context = new H15_PROJET_E03Entities();

            using (context)
            {
                var result = context.usp_ProduireRapportAgent(id, DateTime.Now.Year);

                foreach (var item in result)
                {
                    lstRapport.Add(item);
                }

                rapport.RapportAgent = lstRapport;
                rapport.Agent = agent;
            }

            return View(rapport);
        }

        // GET: Agents/Create
        public ActionResult Create()
        {
            SelectList AgenceId = new SelectList(unitOfWork.AgenceRepository.ObtenirAgence(), "AgenceId", "Nom");
            ViewBag.AgenceId = AgenceId;

            return View();
        }

        // POST: Agents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgentId,Nom,AgenceId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                Agence agence = unitOfWork.AgenceRepository.ObtenirAgenceParID(agent.AgenceId);
                agent.Agence = agence;

                unitOfWork.AgentRepository.InsertAgent(agent);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            SelectList AgenceId = new SelectList(unitOfWork.AgenceRepository.ObtenirAgence(), "AgenceId", "Nom", agent.AgenceId);
            ViewBag.AgenceId = AgenceId;

            return View(agent);
        }

        // GET: Agents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = unitOfWork.AgentRepository.ObtenirAgentParID(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            SelectList AgenceId = new SelectList(unitOfWork.AgenceRepository.ObtenirAgence(), "AgenceId", "Nom", agent.AgenceId);
            ViewBag.AgenceId = AgenceId;

            return View(agent);
        }

        // POST: Agents/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgentId,Nom,AgenceId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                Agence age = unitOfWork.AgenceRepository.ObtenirAgenceParID(agent.AgenceId);

                Agent vraiAgent = unitOfWork.AgentRepository.ObtenirAgentParID(agent.AgentId);

                vraiAgent.Agence = age;
                vraiAgent.AgenceId = age.AgenceId;
                vraiAgent.Nom = agent.Nom;
                vraiAgent.Seances = agent.Seances;

                unitOfWork.AgentRepository.UpdateAgent(vraiAgent);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            SelectList AgenceId = new SelectList(unitOfWork.AgenceRepository.ObtenirAgence(), "AgenceId", "Nom", agent.AgenceId);
            ViewBag.AgenceId = AgenceId;

            return View(agent);
        }

        // GET: Agents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = unitOfWork.AgentRepository.ObtenirAgentParID(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agent agent = unitOfWork.AgentRepository.ObtenirAgentParID(id);

            Agence age = unitOfWork.AgenceRepository.ObtenirAgenceParID(agent.AgenceId);

            if (age != null)
            {
                age.Agents.Remove(agent);
                unitOfWork.AgenceRepository.Update(age);
            }

            IEnumerable<Seance> seancesDagent = unitOfWork.SeanceRepository.ObtenirSeanceParAgent(agent.AgentId);

            foreach (Seance sea in seancesDagent)
            {
                sea.AgentId = null;
                sea.Agent = null;

                unitOfWork.SeanceRepository.Update(sea);
            }

            unitOfWork.AgentRepository.DeleteAgent(agent);
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
