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
    
    public partial class Propriete
    {
        public Propriete()
        {
            this.Photos = new HashSet<Photo>();
            this.Seances = new HashSet<Seance>();
        }
    
        public int ProprieteId { get; set; }
        public string Client { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
    
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Seance> Seances { get; set; }
    }
}