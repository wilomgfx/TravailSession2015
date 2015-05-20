using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace ZipManager.Controllers
{
    public class HomeController : Controller
    {
        public static List<string> lstPhotoPath = new List<string>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> imageFile)
        {
            var cheminBase = AppDomain.CurrentDomain.BaseDirectory + @"Images\";
            //définir les droits d’accès
            
            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(ConfigurationManager.AppSettings["WillUserName"], FileSystemRights.FullControl, AccessControlType.Allow));
            var baserepertoire = Directory.CreateDirectory(cheminBase + 1, securityRules);


            //Soit file le fichier uploadé. la sauvegarde dans le répertoire de ce fichier:
            foreach (var file in imageFile)
            {
                //sauvegarde dans le local le dossier
                var chemin = cheminBase + baserepertoire + @"/" + Path.GetFileName(file.FileName);
                var cheminReplaced = chemin.Replace('\\', '/');
                file.SaveAs(chemin);
                lstPhotoPath.Add(chemin);
            }
            return View();
        }
        public ActionResult ZipIt()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ZipDat()
        {
            var cheminBase = AppDomain.CurrentDomain.BaseDirectory + @"Images\";
            //définir les droits d’accès
            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(ConfigurationManager.AppSettings["WillUserName"], FileSystemRights.FullControl, AccessControlType.Allow));
            string un = "1";
            string path = Path.Combine(cheminBase, un);
            ZipFile zip = new ZipFile();
            zip.AddDirectory(path);
            zip.Save(cheminBase+ "zips" +".zip");

            //download
            Response.ContentType = "application/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=photos.zip");
            //hard coder... but ez peasy la.
            Response.TransmitFile(cheminBase + "zips" + ".zip");
            Response.End();
            return View("Index");
        }
    }
}