//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionPhotoImmobilier.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Seance
    {
        public int SeanceId { get; set; }
        public Nullable<System.DateTime> DateSeance { get; set; }
        public string Agent { get; set; }
        public string Photographe { get; set; }
        public string Client { get; set; }
        public string Forfait { get; set; }
        public string Commentaire { get; set; }
        public string Statut { get; set; }
    
        public virtual Rdv Rdv { get; set; }
    }
}
