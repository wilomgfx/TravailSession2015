using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    public partial class SeanceMetaData
    {
        public int SeanceId { get; set; }
        public Nullable<System.DateTime> DateSeance { get; set; }
        public string Agent { get; set; }
        public string Photographe { get; set; }
        public string Client { get; set; }
        public string Forfait { get; set; }
        public string Commentaire { get; set; }
        public string Statut { get; set; }
    }
}