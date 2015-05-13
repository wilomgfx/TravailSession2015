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
using System.IO;
using System.Security.AccessControl;
using System.Configuration;

namespace GestionPhotoImmobilier.Controllers
{
    public class PhotosController : Controller
    {
        //private H15_PROJET_E03Entities db = new H15_PROJET_E03Entities();
        private UnitOfWork unitofwork = new UnitOfWork();

        // GET: Photos
        public ActionResult Index()
        {
            //var photos = db.Photos.Include(p => p.Propriete);
            var photos = unitofwork.PhotoRepository.ObtenirPhotosComplets();
            return View(photos.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Photo photo = db.Photos.Find(id);
            var cheminBase = AppDomain.CurrentDomain.BaseDirectory + @"Images\Photos\";

            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(ConfigurationManager.AppSettings["monUserName"], FileSystemRights.FullControl, AccessControlType.Allow));

            var photo = unitofwork.PhotoRepository.ObtenirPhotoParID(id);
            ViewBag.cheminBase = "\\Images\\Photos\\"+ photo.Chemin;
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            //ViewBag.ProprieteId = new SelectList(db.Proprietes, "ProprieteId", "Client");
            ViewBag.ProprieteId = new SelectList(unitofwork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Client");
            return View();
        }

        // POST: Photos/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoId,TypeFichier,Chemin,ProprieteId")] Photo photo, IEnumerable<HttpPostedFileBase> imageFile)
        {
            if (ModelState.IsValid)
            {
                
                var cheminBase = AppDomain.CurrentDomain.BaseDirectory + @"Images\Photos\";
                //définir les droits d’accès
                DirectorySecurity securityRules = new DirectorySecurity();
                securityRules.AddAccessRule(new FileSystemAccessRule(ConfigurationManager.AppSettings["monUserName"], FileSystemRights.FullControl, AccessControlType.Allow));
                var baserepertoire = Directory.CreateDirectory(cheminBase + photo.ProprieteId, securityRules);


                //Soit file le fichier uploadé. la sauvegarde dans le répertoire de ce fichier:
                foreach (var file in imageFile)
                {
                    //sauvegarde dans le local le dossier
                    var chemin = cheminBase + baserepertoire + @"/" + Path.GetFileName(file.FileName);
                    var cheminReplaced = chemin.Replace('\\','/');
                    file.SaveAs(chemin);
                    photo.TypeFichier = file.FileName.Substring(file.FileName.LastIndexOf('.')+1);
                    photo.Chemin = photo.ProprieteId +"/" +  file.FileName;
                }

                unitofwork.PhotoRepository.InsertPhoto(photo);
                unitofwork.Save();
                return RedirectToAction("Index");
            }

            //ViewBag.ProprieteId = new SelectList(db.Proprietes, "ProprieteId", "Client", photo.ProprieteId);
            ViewBag.ProprieteId = new SelectList(unitofwork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Client", photo.ProprieteId);
            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = unitofwork.PhotoRepository.ObtenirPhotoParID(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProprieteId = new SelectList(db.Proprietes, "ProprieteId", "Client", photo.ProprieteId);
            ViewBag.ProprieteId = new SelectList(unitofwork.PhotoRepository.ObtenirPhoto(), "ProprieteId", "Client", photo.ProprieteId);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoId,TypeFichier,Chemin,ProprieteId")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(photo).State = EntityState.Modified;
                //db.SaveChanges();
                unitofwork.PhotoRepository.UpdatePhoto(photo);
                unitofwork.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.ProprieteId = new SelectList(db.Proprietes, "ProprieteId", "Client", photo.ProprieteId);
            ViewBag.ProprieteId = new SelectList(unitofwork.ProprieteRepository.ObtenirPropriete(), "ProprieteId", "Client", photo.ProprieteId);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Photo photo = db.Photos.Find(id);
            Photo photo = unitofwork.PhotoRepository.ObtenirPhotoParID(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Photo photo = db.Photos.Find(id);
            Photo photo = unitofwork.PhotoRepository.ObtenirPhotoParID(id);
            //db.Photos.Remove(photo);
            //db.SaveChanges();
            unitofwork.PhotoRepository.DeletePhoto(photo);
            unitofwork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unitofwork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
