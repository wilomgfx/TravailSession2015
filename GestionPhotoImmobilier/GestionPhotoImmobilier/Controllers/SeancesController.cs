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
using GestionPhotoImmobilier.RegleDaffaire;
using PagedList;
using Ionic.Zip;
using System.Security.AccessControl;
using System.IO;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace GestionPhotoImmobilier.Controllers
{
    public class SeancesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Seances
        public ActionResult Index(string currentFilter, string searchPhotographe, string searchStatut, int? page, Nullable<bool> showFuture, int? pageFuture)
        {
            var colSeances = unitOfWork.SeanceRepository.ObtenirSeance();
            var colRdv = unitOfWork.RdvRepository.ObtenirRdvsComplets();

            List<SeanceRdv> lstSeanceRdv = GenererSeancesRdvs(colSeances, colRdv);
            if (searchPhotographe != null || searchStatut != null)
            {
                List<SeanceRdv> lstSeanceRdvSelonRecherche = new List<SeanceRdv>();
                foreach(SeanceRdv item in lstSeanceRdv)
               {
                   if (searchPhotographe == "" && searchStatut == "")
                    {
                        lstSeanceRdvSelonRecherche.Add(item);
                    }
                   else if (searchPhotographe != "" && searchStatut != "")
                      {
                          if(item.Statut.Equals(searchStatut) && item.Photographe.ToUpper().Contains(searchPhotographe.ToUpper()))
                          {
                              lstSeanceRdvSelonRecherche.Add(item);
                          }
                      }
                   else if(item.Photographe.ToUpper().Contains(searchPhotographe.ToUpper()))
                   {
                       lstSeanceRdvSelonRecherche.Add(item);
                   }
                    else if(searchStatut != "")
                    {
                        if (item.Statut.Equals(searchStatut))
                        {
                            lstSeanceRdvSelonRecherche.Add(item);
                        }
                    }
                    else
                    {
                        if (item.Photographe.Equals(searchPhotographe))
                        {
                            lstSeanceRdvSelonRecherche.Add(item);
                        }
                    }
               }
                lstSeanceRdv = lstSeanceRdvSelonRecherche;
            }


            /*
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;
            */

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            if (showFuture == null)
                showFuture = false;

            if(showFuture.Value)
            {
                IEnumerable<Seance> lstSeancesFutures = unitOfWork.SeanceRepository.ObtenirSeanceFutures(DateTime.Now);

                List<SeanceRdv> lstSeancesRdvsFutures = GenererSeancesRdvs(lstSeancesFutures, colRdv);

                int pageNumberFuture = (pageFuture ?? 1);
                ViewBag.seancesFutures = lstSeancesRdvsFutures.OrderBy(s => s.DateSeance).ToPagedList(pageNumberFuture, pageSize);
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

                if (sea.Agent != null)
                    sRdv.Agent = sea.Agent.Nom;
                else
                    sRdv.Agent = null;

                sRdv.Client = sea.Client;
                sRdv.Commentaire = sea.Commentaire;
                sRdv.DateSeance = sea.DateSeance;
                sRdv.Facturer = sea.Facture;

                Forfait forfait = unitOfWork.ForfaitRepository.GetByID(sea.ForfaitId);

                if (forfait != null)
                    sRdv.Forfait = forfait;
                else
                    sRdv.Forfait = null;

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



            double? PrixSeance = unitOfWork.SeanceRepository.ObtenirPrixSeance(id);
            double? PrixTpsDeSeance = unitOfWork.SeanceRepository.ObtenirPrixCoutTps(id);
            double? PrixTvqDeSeance = unitOfWork.SeanceRepository.ObtenirPrixCoutTvq(id);
            double? PrixTotalSeance = unitOfWork.SeanceRepository.ObtenirCoutTotal(id);
            double? PrixExtra = unitOfWork.SeanceRepository.ObtenirCoutExtra(id);


            sommaire.PrixSeance = PrixSeance;
            sommaire.PrixTotalSeance = PrixTotalSeance;
            sommaire.PrixTpsSeance = PrixTpsDeSeance;
            sommaire.PrixTvqSeance = PrixTvqDeSeance;
            sommaire.PrixExtra = PrixExtra;


            return View(sommaire);
        }
        [HttpPost, ActionName("Sommaire")]
        public ActionResult Sommaire(int id)
        {
            

            var cheminBase = AppDomain.CurrentDomain.BaseDirectory + @"Images\Photos\";
            //définir les droits d’accès
            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(ConfigurationManager.AppSettings["WillUserName"], FileSystemRights.FullControl, AccessControlType.Allow));

            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceComplete(id);
            //string directory = "\\images\\photos\\";
            string directory = cheminBase;
            ICollection<Photo> lstPhoto = seance.Propriete.Photos;

            //string path = cheminBase + seance.ProprieteId + "\\" + "SeanceDu" + seance.DateSeance.Value.ToString("yyyy-MM-dd HH-mm-ss") + ".zip";
            string path = Path.Combine(cheminBase, seance.ProprieteId.ToString());
            string fileName = cheminBase + "SeanceDu" + seance.DateSeance.Value.ToString("yyyy-MM-dd HH-mm-ss") + ".zip";
            string fileNameDownload = "SeanceDu" + seance.DateSeance.Value.ToString("yyyy-MM-dd HH-mm-ss") + ".zip";

            ZipFile zip = new ZipFile();
            zip.AddDirectory(path);
            zip.Save(cheminBase + "SeanceDu" + seance.DateSeance.Value.ToString("yyyy-MM-dd HH-mm-ss") + ".zip");

            //download
            Response.ContentType = "application/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileNameDownload);
            //hard coder... but ez peasy la.
            Response.TransmitFile(fileName);
            Response.End();

            return RedirectToAction("Index");
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

            SelectList ForfaitId = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfait(), "ForfaitId", "Nom");
            ViewBag.ForfaitId = ForfaitId;
            return View();
        }

        // POST: Seances/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeanceId,DateSeance,AgentId,Photographe,Client,ForfaitId,Commentaire,Statut,ProprieteId")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                Agent ag = unitOfWork.AgentRepository.ObtenirAgentParID(seance.AgentId);
                Forfait forf = unitOfWork.ForfaitRepository.GetByID(seance.ForfaitId);

                seance.Agent = ag;
                seance.Propriete = unitOfWork.ProprieteRepository.ObtenirProprieteParID(seance.ProprieteId);
                seance.Forfait = forf;

                unitOfWork.SeanceRepository.InsertSeance(seance);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            SelectList AgentId = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom", seance.AgentId);
            ViewBag.AgentId = AgentId;

            SelectList ProprieteId = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse", seance.ProprieteId);
            ViewBag.ProprieteId = ProprieteId;

            SelectList ForfaitId = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfait(), "ForfaitId", "Nom", seance.ForfaitId);
            ViewBag.ForfaitId = ForfaitId;

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

            SelectList ProprieteId = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse", seance.ProprieteId);
            ViewBag.ProprieteId = ProprieteId;

            SelectList ForfaitId = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfait(), "ForfaitId", "Nom", seance.ForfaitId);
            ViewBag.ForfaitId = ForfaitId;
            return View(seance);
        }

        // POST: Seances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeanceId,DateSeance,AgentId,Photographe,Client,ForfaitId,Commentaire,Statut,ProprieteId,RVersion")] Seance seance)
        {
            if (ModelState.IsValid)
            {
                Agent ag = unitOfWork.AgentRepository.ObtenirAgentParID(seance.AgentId);
                Propriete pro = unitOfWork.ProprieteRepository.ObtenirProprieteParID(seance.ProprieteId);
                Forfait forf = unitOfWork.ForfaitRepository.GetByID(seance.ForfaitId);

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
                vraiSeance.Forfait = forf;
                vraiSeance.ForfaitId = forf.ForfaitId;



                // section pour la validation de concurrence entre deux éléments
                Seance seanceModif = unitOfWork.SeanceRepository.ObtenirSeanceParID(seance.SeanceId);
                if (TryUpdateModel(seanceModif, new string[] { "SeanceId", "DateSeance", "AgentId", "Photographe", "Client", "ForfaitId", "Commentaire", "Statut", "ProprieteId", "RVersion" }))
                {
                    unitOfWork.SeanceRepository.UpdateSeance(vraiSeance);
                    try
                    {
                        unitOfWork.Save();
                        return RedirectToAction("Index");
                    }
                    catch (DbEntityValidationException ex)
                    {

                        RecupereErreurValidation(ex);

                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        var entry = ex.Entries.Single();

                        //Si vous voulez voir les différentes valeurs
                        // Response.Write("CurrentValues:" + entry.CurrentValues["FirstName"] +"<br/>");
                        //Response.Write("Original:" + entry.OriginalValues["FirstName"] + "<br/>");
                        //Response.Write("DatabaseValues:" + entry.GetDatabaseValues()["FirstName"] + "<br/>");                          

                        RecupererErreurUpdate(ex);
                    }
                    catch (DbUpdateException ex)
                    {
                        //erreur lors de la modification de la BD
                        ModelState.AddModelError("DbUpdateException", ex.Message);

                    }

                }
                SelectList AgentId = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom", seance.AgentId);
                ViewBag.AgentId = AgentId;

                SelectList ProprieteId = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse", seance.ProprieteId);
                ViewBag.ProprieteId = ProprieteId;

                SelectList ForfaitId = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfait(), "ForfaitId", "Nom", seance.ForfaitId);
                ViewBag.ForfaitId = ForfaitId;

                return View(seanceModif);
            }

            SelectList AgentId2 = new SelectList(unitOfWork.AgentRepository.ObtenirAgent(), "AgentId", "Nom", seance.AgentId);
            ViewBag.AgentId = AgentId2;

            SelectList ProprieteId2 = new SelectList(unitOfWork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Adresse", seance.ProprieteId);
            ViewBag.ProprieteId = ProprieteId2;

            SelectList ForfaitId2 = new SelectList(unitOfWork.ForfaitRepository.ObtenirForfait(), "ForfaitId", "Nom", seance.ForfaitId);
            ViewBag.ForfaitId = ForfaitId2;

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
            if(seance == null)
            {
                return RedirectToAction("Index");
            }
            IEnumerable<Rdv> rdvsSeance = unitOfWork.RdvRepository.ObtenirRdvDeLaSeance(seance.SeanceId);

            foreach (Rdv rdv in rdvsSeance)
            {
                unitOfWork.RdvRepository.Delete(rdv);
            }

            Forfait forf = unitOfWork.ForfaitRepository.GetByID(seance.ForfaitId);

            if(forf != null)
                forf.Seances.Remove(seance);

            unitOfWork.SeanceRepository.DeleteSeance(seance);
            try 
            {
                unitOfWork.Save();
                RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {

                RecupereErreurValidation(ex);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();

                //Si vous voulez voir les différentes valeurs
                // Response.Write("CurrentValues:" + entry.CurrentValues["FirstName"] +"<br/>");
                //Response.Write("Original:" + entry.OriginalValues["FirstName"] + "<br/>");
                //Response.Write("DatabaseValues:" + entry.GetDatabaseValues()["FirstName"] + "<br/>");                          

                RecupererErreurUpdate(ex);
            }
            catch (DbUpdateException ex)
            {
                //erreur lors de la modification de la BD
                ModelState.AddModelError("DbUpdateException", ex.Message);

            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Facturer(int? id)
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

        [HttpPost, ActionName("Facturer")]
        public ActionResult Facturer(int id)
        {
            Seance sea = unitOfWork.SeanceRepository.ObtenirSeanceComplete(id);

            sea.Facture = true;

            unitOfWork.SeanceRepository.Update(sea);

            unitOfWork.Save();

            return RedirectToAction("Index");
        }
        public ActionResult Confirmer(int? id)
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

        [HttpPost, ActionName("Confirmer")]
        public ActionResult Confirmer(int id)
        {
            Seance sea = unitOfWork.SeanceRepository.ObtenirSeanceComplete(id);

            Rdv rd = sea.Rdvs.First();

            rd.Confirmer = true;

            List<Rdv> rdvs = new List<Rdv>();

            rdvs.Add(rd);

            sea.Rdvs = rdvs;

            unitOfWork.SeanceRepository.Update(sea);
            unitOfWork.RdvRepository.Update(rd);

            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Extra(int? id)
        {
            Seance seance = unitOfWork.SeanceRepository.ObtenirSeanceComplete(id);

            if(seance == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Forfait forfait = unitOfWork.ForfaitRepository.ObtenirForfaitParID(seance.ForfaitId);

            SeanceForfait scf = new SeanceForfait();

            scf.Forfait = forfait;
            scf.Seance = seance;

            ViewBag.extraExistant = seance.Extras;

            return View(scf);
        }

        [HttpPost, ActionName("Extra")]
        public ActionResult Extra(int id, FormCollection formCollection, bool? delete)
        {
            if(delete != null && delete == true)
            {
                Seance seaDelete = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);

                seaDelete.Extras = null;

                unitOfWork.SeanceRepository.Update(seaDelete);
                unitOfWork.Save();

                return RedirectToAction("Extra");
            }

            string extraExistants = unitOfWork.SeanceRepository.ObtenirSeanceParID(id).Extras;
            string nomExtra = formCollection["nomExtra"];
            string prixExtra = formCollection["prixExtra"];

            string nouvelEnsembleExtra = "";
            
            if(extraExistants != null && extraExistants != "")
            {
                nouvelEnsembleExtra = extraExistants;
                nouvelEnsembleExtra += '|' + nomExtra + '$' + prixExtra;
            }
            else
                nouvelEnsembleExtra += nomExtra + '$' + prixExtra;

            Seance sea = unitOfWork.SeanceRepository.ObtenirSeanceParID(id);

            sea.Extras = nouvelEnsembleExtra;

            unitOfWork.SeanceRepository.Update(sea);
            unitOfWork.Save();

            return RedirectToAction("Extra");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
        // pour mettre les messages d'erreur dans le modelState
        private void RecupereErreurValidation(DbEntityValidationException ex)
        {
            foreach (var erreur in ex.EntityValidationErrors)
            {
                //var message = erreur.Entry.Entity.GetType();
                foreach (var validationErreur in erreur.ValidationErrors)
                {
                    ModelState.AddModelError("", validationErreur.ErrorMessage);
                }
            }

        }

        //creation message en cas d'accès concurrents
        private void RecupererErreurUpdate(DbUpdateConcurrencyException ex)
        {
            ModelState.AddModelError("", ex.Message);
            var entry = ex.Entries.Single();
            var clientValues = (Seance)entry.Entity;
            var databaseValues = (Seance)entry.GetDatabaseValues().ToObject();

            if (databaseValues == null || clientValues == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The department was deleted by another user.");
            }
            else
            {

                if (databaseValues.Agent != clientValues.Agent)
                    ModelState.AddModelError("Agent", "Current value: " + databaseValues.Agent);
                if (databaseValues.Client != clientValues.Client)
                    ModelState.AddModelError("Client", "Current value: " + databaseValues.Client);
                if (databaseValues.Commentaire != clientValues.Commentaire)
                    ModelState.AddModelError("Commentaire", "Current value: " + databaseValues.Commentaire);
                if (databaseValues.DateSeance != clientValues.DateSeance)
                    ModelState.AddModelError("DateSeance", "Current value: " + databaseValues.DateSeance);
                if (databaseValues.Extras != clientValues.Extras)
                    ModelState.AddModelError("Extras", "Current value: " + databaseValues.Extras);
                if (databaseValues.Facture != clientValues.Facture)
                    ModelState.AddModelError("Facture", "Current value: " + databaseValues.Facture);
                if (databaseValues.Forfait != clientValues.Forfait)
                    ModelState.AddModelError("Forfait", "Current value: " + databaseValues.Forfait);
                if (databaseValues.ForfaitId != clientValues.ForfaitId)
                    ModelState.AddModelError("ForfaitId", "Current value: " + databaseValues.ForfaitId);
                if (databaseValues.photoDisponible != clientValues.photoDisponible)
                    ModelState.AddModelError("photoDisponible", "Current value: " + databaseValues.photoDisponible);
                if (databaseValues.Photographe != clientValues.Photographe)
                    ModelState.AddModelError("Photographe", "Current value: " + databaseValues.Photographe);
                if (databaseValues.Propriete != clientValues.Propriete)
                    ModelState.AddModelError("Propriete", "Current value: " + databaseValues.Propriete);
                if (databaseValues.Rdvs != clientValues.Rdvs)
                    ModelState.AddModelError("Rdvs", "Current value: " + databaseValues.Rdvs);
                if (databaseValues.RVersion != clientValues.RVersion)
                    ModelState.AddModelError("RVersion", "Current value: " + databaseValues.RVersion);
                if (databaseValues.Statut != clientValues.Statut)
                    ModelState.AddModelError("Statut", "Current value: " + databaseValues.Statut);

                
            }
        }
    }
}
