﻿using System;
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
        [Required]
        public Nullable<System.DateTime> DateSeance { get; set; }
        public string Agent { get; set; }
        [Required]
        public string Photographe { get; set; }
        [Required]
        public string Client { get; set; }
        public Forfait Forfait { get; set; }
        public string Commentaire { get; set; }
        [DisplayName("Statut de la séance")]
        public string Statut { get; set; }
        public string Extras { get; set; }
        public bool Facture { get; set; }
        [Timestamp]
        public byte[] RVersion { get; set; }
    }
}