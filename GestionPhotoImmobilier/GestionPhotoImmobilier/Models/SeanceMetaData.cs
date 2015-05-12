using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    [MetadataType(typeof(SeanceMetaData))]
    public partial class Seance { }

    public partial class SeanceMetaData
    {
        public int SeanceId { get; set; }
        [DisplayName("Date de la séance")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> DateSeance { get; set; }
        public string Agent { get; set; }
        public string Photographe { get; set; }
        public string Client { get; set; }
        public Forfait Forfait { get; set; }
        public string Commentaire { get; set; }
        [DisplayName("Statut de la séance")]
        public string Statut { get; set; }
    }
}