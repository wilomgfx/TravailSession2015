using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.ViewModels
{
    public class SeanceRdv
    {
        //seance
        public int SeanceId { get; set; }
        [DisplayName("Date Séance")]
        public Nullable<System.DateTime> DateSeance { get; set; }
        public string Agent { get; set; }
        [DisplayName("Photographe Séance")]
        public string Photographe { get; set; }
        public string Client { get; set; }
        public Forfait Forfait { get; set; }
        public string Commentaire { get; set; }
        public string Statut { get; set; }
        //rdv
        [DisplayName("RDV Confirmé")]
        public Nullable<bool> Confirmer { get; set; }
        [DisplayName("Photographe RDV")]
        public string PhotographeRDV { get; set; }

        public string Extras { get; set; }
        public Nullable<bool> Facturer { get; set; }
    }
}