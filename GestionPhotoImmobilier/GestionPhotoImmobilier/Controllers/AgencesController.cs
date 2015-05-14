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
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

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

            IEnumerable<Agent> lstAgents = unitOfWork.AgentRepository.ObtenirAgentParAgence(agence.AgenceId);

            foreach (Agent ag in lstAgents)
            {
                ag.AgenceId = null;
                ag.Agence = null;

                unitOfWork.AgentRepository.Update(ag);
            }

            unitOfWork.AgenceRepository.DeleteAgence(agence);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Rapport(int? id)
        {
            H15_PROJET_E03Entities context = new H15_PROJET_E03Entities();

            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Agence age = unitOfWork.AgenceRepository.ObtenirAgenceParID(id);

            if(age == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            List<usp_ProduireRapportAgence_Result> lstResults = new List<usp_ProduireRapportAgence_Result>();

            ViewBag.nomAgence = unitOfWork.AgenceRepository.GetByID(id).Nom;
            ViewBag.agenceId = id;

            return View(lstResults);
        }

       [HttpPost, ActionName("Rapport")]
       public ActionResult Rapport (int id, FormCollection formCollection)
       {
           H15_PROJET_E03Entities context = new H15_PROJET_E03Entities();
           
           List<usp_ProduireRapportAgence_Result> lstResults = new List<usp_ProduireRapportAgence_Result>();

           string anneeString = formCollection["Année"];
           string moisString = formCollection["Mois"];

           int? annee;
           int? mois;

           if (anneeString == null || anneeString == "")
               annee = null;
           else
           {
               try
               {
                   annee = int.Parse(anneeString);
               }
               catch
               {
                   annee = null;
               }
           }

           if (moisString == null || moisString == "")
               mois = null;
           else
           {
               try
               {
                   mois = int.Parse(moisString);
               }
               catch
               {
                   mois = null;
               }
           }

           if (annee != null && mois != null)
           {
               using (context)
               {
                   var result = context.usp_ProduireRapportAgence(id, annee, mois);

                   foreach (var item in result)
                   {
                       lstResults.Add(item);
                   }

                   DateTime dateDemandee = new DateTime(annee.Value, mois.Value, 1);

                   ViewBag.date = dateDemandee;

                   ViewBag.agenceId = id;
                   ViewBag.annee = annee;
                   ViewBag.mois = mois;
                   ViewBag.nomAgence = unitOfWork.AgenceRepository.GetByID(id).Nom;

                   return View(lstResults);
               }
           }

           return RedirectToAction("Rapport");
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
